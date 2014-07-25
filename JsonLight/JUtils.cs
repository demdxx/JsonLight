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
using System.Text;
using System.Collections.Generic;

namespace JsonLight
{
  public static class JUtils
  {
    public static string DecodeEscapeUnicodeString(string content)
    {
      // Unescape unicode simbols
      return System.Text.RegularExpressions.Regex.Replace(content,
        @"\\[Uu]([0-9A-Fa-f]{4})",
        m => char.ToString(
          (char)ushort.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.AllowHexSpecifier)));
    }

    public static string DecodeEscapeString(string content, ref int index, char end)
    {
      StringBuilder result = new StringBuilder();
      bool esc = false;
      for (; index < content.Length; index++) {
        char c = content [index];
        if (esc) {
          esc = false;
          if ('\\' == c || '\'' == c || '"' == c || '/' == c) {
            result.Append (c);
            continue;
          } else if ('n' == c) {
            result.Append ('\n');
            continue;
          } else if ('t' == c) {
            result.Append('\t');
            continue;
          } else if ('r' == c) {
            result.Append('\r');
            continue;
          } else {
            result.Append ('\\');
          }
        }
        if (end == c) {
          if (esc) {
            result.Append ('\\');
          }
          return result.ToString ();
        } else if ('\\' == c) {
          esc = true;
        } else {
          result.Append (c);
        }
      }
      throw new FormatException ();
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
      if (obj is IList<Dictionary<string, object>>) {
        return JArray.ValueOf (ToObjectList (obj as IList<Dictionary<string, object>>));
      }
      if (obj is IDictionary<string, object>) {
        return JObject.ValueOf (obj as IDictionary<string, object>);
      }
      return null;
    }

    public static List<object> ToObjectList(IList<Dictionary<string, object>> list)
    {
      var result = new List<object> ();
      foreach (var v in list) {
        result.Add (v);
      }
      return result;
    }
  }
}