/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
 */
using System;

namespace JsonLight
{
  public class JBoolean : JValue
  {
    public bool _Value = false;

    public virtual object Value
    {
      get { return _Value; }
      set { _Value = Convert.ToBoolean (value); }
    }

    public JBoolean (object val)
    {
      if (val is string) {
        _Value = "true" == (string)val || "1" == (string)val;
      } else {
        _Value = Convert.ToBoolean (val);
      }
    }
    
    #region Value of

    public static JBoolean ValueOf (object val)
    {
      return new JBoolean (val);
    }

    #endregion // Value of

    public override string ToString ()
    {
      return _Value ? "true" : "false";
    }

    public virtual string ToJsonString ()
    {
      return ToString ();
    }
  }
}