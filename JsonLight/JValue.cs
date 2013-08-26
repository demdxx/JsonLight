/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
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
  }
}