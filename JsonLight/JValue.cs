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
  public interface JValue
  {
    /**
     * Object to string
     * @return string
     */
    string ToString ();

    /**
     * Object to JSON string
     * @return JSON string
     */
    string ToJsonString ();

    /**
     * Get set raw value
     */
    object Value { get; set; }

    /**
     * Get int value
     * @return int
     */
    int IntValue { get; }

    /**
     * Get long value
     * @return long
     */
    long LongValue { get; }

    /**
     * Get double value
     * @return double
     */
    double DoubleValue { get; }

    /**
     * Get bool value
     * @return bool
     */
    bool BoolValue { get; }
  }
}