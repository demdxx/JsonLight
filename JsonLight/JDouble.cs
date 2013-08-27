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

    #region Value convert to
    
    /**
     * Object to string
     * @return string
     */
    public override string ToString ()
    {
      return Convert.ToString (_Value);
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
        return Convert.ToInt32 (_Value);
      }
    }

    /**
     * Get long value
     * @return long
     */
    public long LongValue
    {
      get {
        return Convert.ToInt64 (_Value);
      }
    }

    /**
     * Get double value
     * @return double
     */
    public double DoubleValue
    {
      get {
        return _Value;
      }
    }

    /**
     * Get bool value
     * @return bool
     */
    public bool BoolValue
    {
      get {
        return Convert.ToBoolean (_Value);
      }
    }

    #endregion // Value convert to
  }
}