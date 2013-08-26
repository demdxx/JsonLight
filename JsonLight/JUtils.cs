/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
 */
using System;
using System.Text;
using System.Collections.Generic;

namespace JsonLight
{
  public static class JUtils
  {
    public static string DecodeEscapeString(string content, ref int index, char end)
    {
      StringBuilder result = new StringBuilder();
      bool esc = false;
      for (; index < content.Length; index++) {
        char c = content [index];
        if (esc) {
          esc = false;
          if ('\\' == c || '\'' == c || '"' == c) {
            result.Append (c);
            continue;
          } else if ('n' == c) {
            result.Append ('\n');
            continue;
          } else if ('t' == c) {
            result.Append ('\t');
            continue;
          } else {
            result.Append ('\\');
          }
        }
        if (end == c) {
          if (esc) {
            result.Append ('\\');
          }
          break;
        } else if ('\\' == c) {
          esc = true;
        } else {
          result.Append (c);
        }
      }
      return result.ToString ();
    }

    private static string endSimbols = " \n\t,:;'\"!@#$%^&*(){}[]=?/";
    public static string DecodeWord(string content, ref int index)
    {
      StringBuilder result = new StringBuilder();
      bool esc = false;
      for (; index < content.Length; index++) {
        char c = content [index];
        if (esc) {
          esc = false;
          if ('\\' == c || '\'' == c || '"' == c) {
            result.Append (c);
            continue;
          } else {
            result.Append ('\\');
          }
        }
        if (endSimbols.IndexOf (c) >= 0) {
          if (esc) {
            result.Append ('\\');
          }
          index--;
          break;
        } else if ('\\' == c) {
          esc = true;
        } else {
          result.Append (c);
        }
      }
      return result.ToString ();
    }

    /**
     * Get JValue from string
     * @param value string
     * @return JValue
     */
    public static JValue ValueOfString(string val)
    {
      char c = val [0];
      if ('1' == c || '2' == c || '3' == c || '4' == c || '5' == c
          || '6' == c || '7' == c || '8' == c || '9' == c || '0' == c) {
        return val.IndexOf ('.') < 0 ? (JValue)JInteger.ValueOf (val) : (JValue)JDouble.ValueOf (val);
      } else if ("true" == val || "false" == val) {
        return JBoolean.ValueOf (val);
      }
      return JString.ValueOf (val);
    }

    public static JValue ValueOf(object obj)
    {
      if (obj is string) {
        return JString.ValueOf (obj as string);
      }
      if (obj is int || obj is long) {
        return JInteger.ValueOf (obj);
      }
      if (obj is float || obj is double) {
        return JDouble.ValueOf (obj);
      }
      if (obj is bool) {
        return JBoolean.ValueOf (obj);
      }
      if (obj is IList<object>) {
        return JArray.ValueOf (obj as IList<object>);
      }
      if (obj is IDictionary<string, object>) {
        return JObject.ValueOf (obj as IDictionary<string, object>);
      }
      return null;
    }
  }
}