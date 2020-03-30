using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncopq.Parsing;
using Sprache;
using Syncopq.Reader;
using System.IO;

namespace Syncopq.Testing.Reader
{
    public class FormulasReaderTest
    {
        [Test]
        public void Extract_MiniPowerBIFile_NonEmpty()
        {
            var fullpath = FileOnDisk.CreatePhysicalFile("Myfile.pbix", $"{this.GetType().Namespace}.Resources.Myfile.pbix");
            using var reader = new FormulasReader(fullpath);
            var content = reader.ReadToEnd();
            Assert.That(content, Does.StartWith("section"));
            Assert.That(content, Does.EndWith(";"));
        }
    }
}
