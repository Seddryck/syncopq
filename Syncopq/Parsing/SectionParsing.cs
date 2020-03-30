using System;
using System.Collections.Generic;
using System.Text;
using Sprache;

namespace Syncopq.Parsing
{
    public class SectionParsing
    {
        public readonly static Parser<Section> Parser =
        (
                from section in Keyword.Section
                from identifier in Grammar.Identifier
                from terminator in Symbol.SemiColumn
                from queries in Parse.AtLeastOnce(queryParser)
                select new Section(identifier, queries)
        );

        private readonly static Parser<Query> queryParser =
        (
            from queries in QueryParsing.Parser
            from lineEnd in Parse.AtLeastOnce(Parse.LineTerminator.Or(Parse.WhiteSpace.Many().Text()))
            select queries
        );
    }
}
