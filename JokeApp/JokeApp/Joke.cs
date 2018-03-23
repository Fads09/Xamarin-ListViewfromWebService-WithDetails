using System;
using System.Collections.Generic;

namespace JokeApp
{
    public class Joke
    {
        public string type { get; set; }
        public List<Value> value { get; set; }

        public Joke()
        {

        }
    }

    public class Value
    {
        public string joke { get; set; }
        public List<object> categories { get; set; }
    }
}