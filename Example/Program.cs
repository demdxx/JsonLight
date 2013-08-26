using System;
using JsonLight;

namespace Example
{
  class MainClass
  {
    public static void Main (string[] args)
    {
      string json = "{'k1': '100', \"k2\": 898.3, k3: [{1: 988, 2: false}, 1, 1.3, true, \"DDDD DSDAS ASDAS\"]}";
      JObject j = JObject.DecodeObject (json);
      Console.WriteLine ("JSON Encode: " + j.ToString ());
    }
  }
}
