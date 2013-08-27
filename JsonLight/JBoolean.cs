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

    #region Value convert to
    
    /**
     * Object to string
     * @return string
     */
    public override string ToString ()
    {
      return _Value ? "true" : "false";
    }
    
    /**
     * Object to JSON string
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
        return _Value ? 1 : 0;
      }
    }

    /**
     * Get long value
     * @return long
     */
    public long LongValue
    {
      get {
        return _Value ? 1 : 0;
      }
    }

    /**
     * Get double value
     * @return double
     */
    public double DoubleValue
    {
      get {
        return _Value ? 1.0 : 0.0;
      }
    }

    /**
     * Get bool value
     * @return bool
     */
    public bool BoolValue
    {
      get {
        return _Value;
      }
    }

    #endregion // Value convert to
  }
}