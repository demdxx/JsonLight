/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
 */
using System;

namespace JsonLight
{
  public class JDouble : JValue
  {
    public double _Value = 0.0;

    public virtual object Value
    {
      get { return _Value; }
      set { _Value = Convert.ToDouble (value); }
    }

    public JDouble (object val)
    {
      _Value = Convert.ToDouble (val);
    }

    #region Value of

    public static JDouble ValueOf (object val)
    {
      return new JDouble (val);
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