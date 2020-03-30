using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncopq.Parsing;
using Sprache;

namespace Syncopq.Testing.Parsing
{
    public class QueryTest
    {
        [Test]
        public void Parser_SimpleFunction_IdentifierRetrieved()
        {
            var input = "shared ApiKey = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Name, Is.EqualTo("ApiKey"));
        }

        [Test]
        public void Parser_SimpleFunctionQuotedIdentifier_IdentifierRetrieved()
        {
            var input = "shared #\"ApiKey\" = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Name, Is.EqualTo("ApiKey"));
        }

        [Test]
        public void Parser_SimpleFunctionQuotedIdentifierWithSpace_IdentifierRetrieved()
        {
            var input = "shared #\"Api Key\" = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Name, Is.EqualTo("Api Key"));
        }

        [Test]
        public void Parser_SimpleCodeWithoutQuote_CodeRetrieved()
        {
            var input = "shared #\"Api Key\" = 125 meta [IsParameterQuery=true, IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Code, Is.EqualTo("125 meta [IsParameterQuery=true, IsParameterQueryRequired=true]"));
        }

        [Test]
        public void Parser_SimpleCode_CodeRetrieved()
        {
            var input = "shared #\"Api Key\" = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Code, Is.EqualTo("\"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true]"));
        }

        [Test]
        public void Parser_MultilineCode_CodeRetrieved()
        {
            var input = "shared #\"Api Key\" = \"X8sdc0uezeOKlm\" meta\r\n\t[IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Code, Is.EqualTo("\"X8sdc0uezeOKlm\" meta\r\n\t[IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true]"));
        }

        [Test]
        public void Parser_CodeIncludingSemiColumnBetweenQuotes_CodeRetrieved()
        {
            var input = "shared #\"Api Key\" = \"X8sdc0u;zeOKlm\" meta\r\n\t[IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = QueryParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Query>());
            Assert.That(result.Code, Is.EqualTo("\"X8sdc0u;zeOKlm\" meta\r\n\t[IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true]"));
        }
    }
}
