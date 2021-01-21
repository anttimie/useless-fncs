using System;
using System.Collections.Generic;
using System.Text;

namespace UselessFacts
{
    public class Phrases
    {
        List<Phrase> phrases { get; set; }
    }
    public class Phrase
    {
        public long id { get; set; }
        public string phrase { get; set; }
        public DateTime added { get; set; }

    }
}
