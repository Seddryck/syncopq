using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncopq.Parsing
{
    static class Keyword
    {
        public static readonly Parser<string> Shared = Parse.IgnoreCase("Shared").Text().Token();
    }
}
