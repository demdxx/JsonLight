/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
 */
using System;

namespace JsonLight
{
  public class JInteger : JValue
  {
    public long _Value = 0;

    public virtual object Value
    {
      get { return _Value; }
      set { _Value = Convert.ToInt64 (value); }
    }

    public JInteger (object val)
    {
      _Value = Convert.ToInt64 (val);
    }

    #region Value of

    public static JInteger ValueOf (object val)
    {
      return new JInteger (val);
    }

    #endregion // Value of

    public override string ToString ()
    {
      return Convert.ToString (_Value);
    }

    public virtual string ToJsonString ()
    {
      return ToString ();
    }
  }
}