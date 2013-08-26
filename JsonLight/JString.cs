/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
 */
using System;

namespace JsonLight
{
  public class JString : JValue
  {
    public string _Value = "";

    public virtual object Value
    {
      get { return _Value; }
      set { _Value = Convert.ToString (value); }
    }

    public JString (string val)
    {
      _Value = val;
    }

    #region Value of

    public static JString ValueOf (string val)
    {
      return new JString (val);
    }
    
    #endregion // Value of

    public override string ToString ()
    {
      return _Value;
    }

    public virtual string ToJsonString ()
    {
      return String.Format ("\"{0}\"", _Value.Replace ("\"", "\\\"").Replace ("\n", "\\n").Replace ("\t", "\\t"));
    }
  }
}