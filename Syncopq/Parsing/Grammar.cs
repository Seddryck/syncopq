using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncopq.Parsing
{
    static class Grammar
    {
        private static readonly Parser<string> Textual = Parse.LetterOrDigit.AtLeastOnce().Text().Token();
        private static readonly Parser<string> QuotedTextual = Parse.CharExcept("\"").AtLeastOnce().Text().Contained(Parse.Char('\"'), Parse.Char('\"')).Token();
        private static readonly Parser<string> QuotedIdentifier =
        (
            from sharp in Parse.Char('#')
            from identifier in QuotedTextual
            select identifier
        );


        public static readonly Parser<string> Identifier = QuotedIdentifier.Or(Textual);
        public static readonly Parser<string> Code = Parse.CharExcept(';').AtLeastOnce().Text().Token();
    }
}
