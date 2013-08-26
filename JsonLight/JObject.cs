/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
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

      for (; index<content.Length; index++, simbol++) {
        char c = content [index];
        if ('}' == c) {
          if (colon) {
            throw new ExceptionSyntaxError (line, simbol);
          }
          break;
        }
        else if (' ' == c || '\n' == c || '\t' == c) {
          if ('\n' == c) {
            line++;
            simbol = 0;
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
              index++;
              if (isValue) {
                obj.Add (key, JString.ValueOf (JUtils.DecodeEscapeString (content, ref index, c)));
              } else {
                key = JUtils.DecodeEscapeString (content, ref index, c);
              }
            } else if ('[' == c) {
              if (!isValue) {
                throw new ExceptionSyntaxError (line, simbol);
              }
              obj.Add (key, JArray.DecodeArray (content, ref index, ref line, ref simbol));
            } else if ('{' == c) {
              if (!isValue) {
                throw new ExceptionSyntaxError (line, simbol);
              }
              obj.Add (key, JObject.DecodeObject (content, ref index, ref line, ref simbol));
            } else {
              if (isValue) {
                obj.Add (key, JUtils.ValueOfString (JUtils.DecodeWord (content, ref index)));
              } else {
                key = JUtils.DecodeWord (content, ref index);
              }
            }
            if (isValue) {
              comma = false;
              colon = false;
            }
            isValue = !isValue;
          }
        }
      }
      return obj;
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
     */
    public virtual object Value
    {
      get { return this; }
      set {
        Clear ();
        if (null != value && value is IDictionary<string, object>) {
          AddFromDict (value as IDictionary<string, object>);
        } else {
          throw new ArgumentException ();
        }
      }
    }

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
  }
}