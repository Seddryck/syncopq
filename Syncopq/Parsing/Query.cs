using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncopq.Parsing
{
    public static class Query
    {
        public readonly static Parser<string> Parser =
        (
                from load in Keyword.Shared
                from identifier in Grammar.Identifier
                from equal in Symbol.Equal
                from code in Grammar.Code
                from terminator in Symbol.SemiColumn
                select identifier
        );
    }
}
