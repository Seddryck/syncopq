using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using Syncopq.Reader.Compression;

namespace Syncopq.Reader
{
    public class FormulasReader : IDisposable
    {
        private StreamReader Reader { get; set; }
        private IUnpack Unpacker { get;  }
        private string PbixPath { get; }

        public FormulasReader(string pbixPath, IUnpack unpacker)
            => (PbixPath, Unpacker) = (pbixPath, unpacker);

        public FormulasReader(string pbixPath) 
            : this(pbixPath, new SevenZipUnpack()) { }

        public string ReadToEnd()
        {
            var destination = Path.Combine(Path.GetTempPath(), "syncopq", Guid.NewGuid().ToString());
            Unpacker.Extract(PbixPath, destination);
            var source = Path.Combine(destination, "DataMashup");
            destination = Path.Combine(destination, "DataMashup~");
            Unpacker.Extract(source, destination);
            destination = Path.Combine(destination, "Formulas", "Section1.m");

            Reader = new StreamReader(destination);
            return Reader.ReadToEnd();
        }

        #region "disposed"

        private bool disposed = false;
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                handle.Dispose();

            disposed = true;
        }

        #endregion
    }
}
