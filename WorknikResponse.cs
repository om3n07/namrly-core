using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace Namrly.WordnikApi
{
    public class WordnikResponse
    {
        public string RelationshipType { get; set; }
        public List<string> Words { get; set; } 
    }

    public class WordnikSingleResponse
    {
        public int id { get; set; }
        public string word { get; set; }
    }
}