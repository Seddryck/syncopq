﻿using NUnit.Framework;
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
            var result = Query.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<string>());
            Assert.That(result, Is.EqualTo("ApiKey"));
        }

        [Test]
        public void Parser_SimpleFunctionQuotedIdentifier_IdentifierRetrieved()
        {
            var input = "shared #\"ApiKey\" = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = Query.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<string>());
            Assert.That(result, Is.EqualTo("ApiKey"));
        }

        [Test]
        public void Parser_SimpleFunctionQuotedIdentifierWithSpace_IdentifierRetrieved()
        {
            var input = "shared #\"Api Key\" = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = Query.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<string>());
            Assert.That(result, Is.EqualTo("Api Key"));
        }
    }
}
