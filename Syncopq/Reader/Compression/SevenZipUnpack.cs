using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Syncopq.Reader.Compression
{
    public class SevenZipUnpack : IUnpack
    {
        private string SevenZipPath { get; }

        public SevenZipUnpack(string sevenZipPath)
            => SevenZipPath = sevenZipPath;

        public SevenZipUnpack() : this(@"C:\Program Files\7-Zip\7z.exe") { }

        public void Extract(string source, string destination)
        {
            var startInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = SevenZipPath,
                Arguments = $"x \"{source}\" -o{destination}",
            };

            var process = Process.Start(startInfo);
            process.WaitForExit();
        }
    }
}
