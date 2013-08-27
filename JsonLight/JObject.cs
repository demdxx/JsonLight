/**
 * @project JsonLight
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2013 Dmitry Ponomarev <demdxx@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace JsonLight
{
  public class JObject : Dictionary<string, JValue>, JValue
  {
    #region Decode section

    /**
     * Decode json string to JArray or JObject
     * @param content string
     * @return JValue
     */
    public static JValue Decode(string content)
    {
      if ('[' == content [0]) {
        return JArray.Decode (content);
      }
      return DecodeObject (content);
    }

    /**
     * Decode json string to JObject
     * @param content string
     * @return new JObject instance
     */
    public static JObject DecodeObject (string content)
    {
      int index = 0, line = 0, simbol = 0;
      return DecodeObject (content, ref index, ref line, ref simbol);
    }
    

    /**
     * Decode json string to JObject
     * @param content string
     * @param ref index int
     * @param red line int
     * @param ref simbol int
     * @return new JObject instance
     */
    public static JObject DecodeObject (string content, ref int index, ref int line, ref int simbol)
    {
      if ('{' != content [index]) {
        throw new ExceptionSyntaxError (0, 0);
      }

      JObject obj = new JObject ();

      bool comma = true;
      bool colon = false;
      bool isValue = false;
      string key = "";
      index ++;
      simbol ++;

      for (; index<content.Length; index++, simbol++) {
        char c = content [index];
        if ('}' == c) {
          if (colon) {
            throw new ExceptionSyntaxError (line, simbol);
          }
          return obj;
        }
        else if (' ' == c || '\n' == c || '\t' == c) {
          if ('\n' == c) {
            line++;
            simbol = -1;
          }
          continue;
        } else if (!comma) {
          if (',' != c) {
            throw new ExceptionSyntaxError (line, simbol);
          }
          comma = true;
        } else {
          if (isValue && !colon) {
            if (':' != c) {
              throw new ExceptionSyntaxError (line, simbol);
            }
            colon = true;
          } else {
            if ('"' == c || '\'' == c) {
              // Decode string value "String" or 'String'
              try {
                var pIndex = index;
                index++;
                if (isValue) {
                  obj.Add (key, JString.ValueOf (JUtils.DecodeEscapeString (content, ref index, c)));
                } else {
                  key = JUtils.DecodeEscapeString (content, ref index, c);
                  if (null == key || key.Length < 1) {
                    throw new ExceptionSyntaxError (line, simbol);
                  }
                }
                simbol += index - pIndex;
              } catch (FormatException e) {
                throw new ExceptionSyntaxError (line, simbol);
              }
            } else if ('[' == c) {
              // Parse Array
              if (!isValue) {
                throw new ExceptionSyntaxError (line, simbol);
              }
              obj.Add (key, JArray.DecodeArray (content, ref index, ref line, ref simbol));
            } else if ('{' == c) {
              // Parse Dictionary
              if (!isValue) {
                throw new ExceptionSyntaxError (line, simbol);
              }
              obj.Add (key, JObject.DecodeObject (content, ref index, ref line, ref simbol));
            } else {
              // Decode word value WORD or W0r6 ...
              var pIndex = index;
              if (isValue) {
                var val = JUtils.DecodeWord (content, ref index);
                if (null == val || val.Length < 1) {
                  throw new ExceptionSyntaxError (line, simbol);
                }
                obj.Add (key, JUtils.ValueOfString (val));
              } else {
                key = JUtils.DecodeWord (content, ref index);
                if (null == key || key.Length < 1) {
                  throw new ExceptionSyntaxError (line, simbol);
                }
              }
              simbol += index - pIndex;
            }
            if (isValue) {
              comma = false;
              colon = false;
            }
            isValue = !isValue;
          }
        }
      }
      throw new ExceptionSyntaxError (line, simbol);
    }

    #endregion // Decode section
    
    #region Value of

    /**
     * Decode json string to JObject
     * @param content string
     * @return new JObject instance
     */
    public static JObject ValueOf (string content)
    {
      return DecodeObject (content);
    }

    /**
     * Dictionary to JObject
     * @param dict {key: value, ...}
     * @return new JObject instance
     */
    public static JObject ValueOf (IDictionary<string, object> dict)
    {
      return (new JObject ()).AddFromDict (dict);
    }

    #endregion // Value of

    /**
     * Add values from dict
     * @param dict {key: value, ...}
     * @return self
     */
    public JObject AddFromDict (IDictionary<string, object> dict)
    {
      foreach (var it in dict) {
        Add (it.Key, JUtils.ValueOf (it.Value));
      }
      return this;
    }

    /**
     * Get/set dictionary value
     * @return object
     */
    public virtual object Value
    {
      get {
        return this;
      }
      set {
        Clear ();
        if (null != value && value is IDictionary<string, object>) {
          AddFromDict (value as IDictionary<string, object>);
        } else {
          throw new ArgumentException ();
        }
      }
    }
    
    #region Value convert to

    /**
     * Convert to dictionary
     * @return dictionary
     */
    public Dictionary<string, object> ToDictionary()
    {
      var result = new Dictionary<string, object> ();
      foreach (var p in this) {
        if (null != p.Value) {
          result.Add (p.Key, p.Value.Value);
        }
      }
      return result;
    }

    /**
     * Convert to JSON string
     * @return JSON string
     */
    public override string ToString ()
    {
      var sb = new StringBuilder ();
      bool started = false;
      foreach (var p in this) {
        if (null != p.Value) {
          if (started) {
            sb.Append (",");
          } else {
            started = true;
          }
          sb.AppendFormat ("\"{0}\":{1}", p.Key, p.Value.ToJsonString ());
        }
      }
      return String.Format ("{{{0}}}", sb.ToString ());
    }

    /**
     * Convert to JSON string
     * @return JSON string
     */
    public virtual string ToJsonString ()
    {
      return ToString ();
    }

    /**
     * Get int value
     * @return int
     */
    public int IntValue
    {
      get {
        return Count > 0 ? 1 : 0;
      }
    }

    /**
     * Get long value
     * @return long
     */
    public long LongValue
    {
      get {
        return Count > 0 ? 1 : 0;
      }
    }

    /**
     * Get double value
     * @return double
     */
    public double DoubleValue
    {
      get {
        return Count > 0 ? 1.0 : 0.0;
      }
    }

    /**
     * Get bool value
     * @return bool
     */
    public bool BoolValue
    {
      get {
        return Count > 0;
      }
    }

    #endregion // Value convert to
  }
}