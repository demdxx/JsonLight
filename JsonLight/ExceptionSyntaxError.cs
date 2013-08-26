/**
 * @project JsonLight
 * @copyright Dmitry Ponomarev <demdxx@gmail.com> 2013
 * @license MIT
 */
using System;

namespace JsonLight
{
  public class ExceptionSyntaxError : Exception
  {
    public ExceptionSyntaxError(int line, int simbol)
      : base(String.Format ("Json syntax error at line {0} simbol {1}", line, simbol))
    {
      // ...
    }
  }
}

