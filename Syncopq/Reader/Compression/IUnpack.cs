using System;
using System.Collections.Generic;
using System.Text;

namespace Syncopq.Reader.Compression
{
    public interface IUnpack
    {
        void Extract(string source, string destination);
    }
}
