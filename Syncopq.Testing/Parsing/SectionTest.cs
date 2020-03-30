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
    public class SectionTest
    {
        [Test]
        public void Parser_OneFunction_Retrieved()
        {
            var input = "section Section1;\r\n\r\nshared ApiKey = \"X8sdc0uezeOKlm\" meta [IsParameterQuery=true, Type=\"Text\", IsParameterQueryRequired=true];";
            var result = SectionParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Section>());
            Assert.That(result.Name, Is.EqualTo("Section1"));
            Assert.That(result.Queries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Parser_TwoFunctions_Retrieved()
        {
            var input = @"section Section1;
                        shared ApiKey = ""X8sdc0uezeOKlm"" meta [IsParameterQuery=true, Type=""Text"", IsParameterQueryRequired=true];
                        shared Password = ""helloWorld"" meta [IsParameterQuery=true, Type=""Text"", IsParameterQueryRequired=true];
                        ";
            var result = SectionParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Section>());
            Assert.That(result.Name, Is.EqualTo("Section1"));
            Assert.That(result.Queries.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Parser_TwoFunctionsWithManyLineEnd_Retrieved()
        {
            var input = @"section Section1;
                        


                        shared ApiKey = ""X8sdc0uezeOKlm"" meta [IsParameterQuery=true, Type=""Text"", IsParameterQueryRequired=true];
                        

                        shared Password = ""helloWorld"" meta [IsParameterQuery=true, Type=""Text"", IsParameterQueryRequired=true];
                        

                        ";
            var result = SectionParsing.Parser.Parse(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Section>());
            Assert.That(result.Name, Is.EqualTo("Section1"));
            Assert.That(result.Queries.Count(), Is.EqualTo(2));
        }
    }
}
