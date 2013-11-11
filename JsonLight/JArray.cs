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
using System.Text;

namespace JsonLight
{
  public class JArray : List<JValue>, JValue
  {
    #region Decode section

    /**
     * Decode json string to JArray or JObject
     * @param content string
     * @return JValue
     */
    public static JValue Decode(string content)
    {
      if ('{' == content [0]) {
        return JObject.DecodeObject (content);
      }
      return DecodeArray (content);
    }

    /**
     * Decode json string to JArray
     * @param content string
     * @return new JArray instance
     */
    public static JArray DecodeArray(string content)
    {
      int index = 0, line = 0, simbol = 0;
      return DecodeArray (content, ref index, ref line, ref simbol);
    }

    /**
     * Decode json string to JArray
     * @param content string
     * @param ref index int
     * @param red line int
     * @param ref simbol int
     * @return new JArray instance
     */
    public static JArray DecodeArray(string content, ref int index, ref int line, ref int simbol)
    {
      if ('[' != content [index]) {
        throw new ExceptionSyntaxError (line, simbol);
      }

      JArray arr = new JArray ();
      bool comma = true;
      index ++;
      simbol ++;

      for (; index<content.Length; index++, simbol++) {
        char c = content [index];
        if (']' == c) {
          return arr;
        }
        if (' ' == c || '\n' == c || '\t' == c) {
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
          if ('"' == c || '\'' == c) {
            // Decode string value "String" or 'String'
            try {
              var pIndex = index;
              index++;
              arr.Add (JString.ValueOf (JUtils.DecodeEscapeString (content, ref index, c)));
              simbol += index - pIndex;
            } catch (FormatException e) {
              throw new ExceptionSyntaxError (line, simbol);
            }
          } else if ('[' == c) {
            // Parse Array
            arr.Add (JArray.DecodeArray (content, ref index, ref line, ref simbol));
          } else if ('{' == c) {
            // Parse Dictionary
            arr.Add (JObject.DecodeObject (content, ref index, ref line, ref simbol));
          } else {
            // Decode word value WORD or W0r6 ...
            var pIndex = index;
            var val = JUtils.DecodeWord (content, ref index);
            if (null == val || val.Length < 1) {
              throw new ExceptionSyntaxError (line, simbol);
            }
            if ("null" == val) {
              arr.Add (null);
            } else {
              arr.Add (JUtils.ValueOfString(val));
            }
            simbol += index - pIndex;
          }
          comma = false;
        }
      }
      throw new ExceptionSyntaxError (line, simbol);
    }

    #endregion // Decode section

    #region Value of

    /**
     * Decode json string to JArray
     * @param content string
     * @return new JArray instance
     */
    public static JArray ValueOf (string content)
    {
      return DecodeArray (content);
    }

    /**
     * JArray object from list
     * @param list [...]
     * @param new JArray instance
     */
    public static JArray ValueOf (IList<object> list)
    {
      return (new JArray ()).AddList (list);
    }
    
    #endregion // Value of

    /**
     * Add values from list
     * @param list
     * @return self
     */
    public JArray AddList (IList<object> list)
    {
      foreach (var it in list) {
        Add (JUtils.ValueOf (it));
      }
      return this;
    }

    /**
     * Get/set list value
     */
    public virtual object Value
    {
      get { return this; }
      set {
        Clear ();
        if (null != value && value is IList<object>) {
          AddList (value as IList<object>);
        } else {
          throw new ArgumentException ();
        }
      }
    }

    /**
     * Convert to List object
     * @return [...]
     */
    public List<object> ToList ()
    {
      List<object> list = new List<object> ();
      foreach (var it in this) {
        if (it.Value is JObject)
        {
          list.Add((it.Value as JObject).ToDictionary());
        }
        else if (it.Value is JArray)
        {
          list.Add((it.Value as JArray).ToList());
        }
        else
        {
          list.Add(it.Value);
        }
      }
      return list;
    }

    #region Value convert to
    
    /**
     * Convert to JSON string
     * @return [string]
     */
    public override string ToString ()
    {
      StringBuilder sb = new StringBuilder ();
      bool started = false;
      foreach (var it in this) {
        if (started) {
          sb.Append (",");
        } else {
          started = true;
        }
        sb.Append (it.ToJsonString ());
      }
      return String.Format ("[{0}]", sb.ToString ());
    }

    /**
     * Convert to JSON string
     * @return [string]
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