/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
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

      for (; index<content.Length; index++, simbol++) {
        char c = content [index];
        if (']' == c) {
          break;
        }
        if (' ' == c || '\n' == c || '\t' == c) {
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
          if ('"' == c || '\'' == c) {
            index++;
            arr.Add (JString.ValueOf (JUtils.DecodeEscapeString (content, ref index, c)));
          } else if ('[' == c) {
            arr.Add (JArray.DecodeArray (content, ref index, ref line, ref simbol));
          } else if ('{' == c) {
            arr.Add (JObject.DecodeObject (content, ref index, ref line, ref simbol));
          } else {
            arr.Add (JUtils.ValueOfString (JUtils.DecodeWord (content, ref index)));
          }
          comma = false;
        }
      }
      return arr;
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
        list.Add (it.Value);
      }
      return list;
    }
    
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
  }
}