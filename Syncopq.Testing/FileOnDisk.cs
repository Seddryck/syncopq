using System.IO;
using System.Reflection;

namespace Syncopq.Testing
{
    public static class FileOnDisk
    {
        private static readonly object locker = new object();

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, len);
        }

        public static string CreatePhysicalFile(string filename, string resource)
        {
            //if filename starts by a directory separator remove it
            filename = filename.StartsWith(Path.DirectorySeparatorChar.ToString()) ? filename.Substring(1) : filename;

            //Build the fullpath for the file to read
            var fullpath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{Path.DirectorySeparatorChar}{filename}";

            //create the directory if needed
            if (!Directory.Exists(Path.GetDirectoryName(fullpath)))
                Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            lock (locker)
            {
                //delete it if already existing
                if (File.Exists(fullpath))
                    File.Delete(fullpath);

                // A Stream is needed to read the XLS document.
                using var stream = Assembly.GetExecutingAssembly()
                                               .GetManifestResourceStream(resource);
                if (stream == null)
                    throw new FileNotFoundException(resource);

                //Open another stream to persist the file on disk
                using Stream file = File.OpenWrite(fullpath);
                CopyStream(stream, file);
            }
            return fullpath;
        }
    }
}