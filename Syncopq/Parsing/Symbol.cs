using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncopq.Parsing
{
    static class Symbol
    {
        public static readonly Parser<char> SemiColumn = Parse.Char(';').Token();
        public static readonly Parser<char> Equal = Parse.Char('=').Token();
    }
}
