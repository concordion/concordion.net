using System;
using System.IO;
using System.Text;
using java.io;
using org.concordion.api;
using org.concordion.@internal.util;
using File = System.IO.File;
using nu.util.io;

namespace Concordion.NET.Internal
{
    public class FileTarget : Target
    {
        #region Fields

        private static readonly long FRESH_ENOUGH_MILLIS = 2000; // 2 secs

        #endregion

        #region Properties

        private string BaseDirectory
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public FileTarget(string baseDirectory)
        {
            if (baseDirectory.EndsWith("\\"))
            {
                this.BaseDirectory = baseDirectory;
            }
            else
            {
                this.BaseDirectory = baseDirectory + "\\";
            }
        }

        #endregion

        #region Methods

        private void MakeDirectories(Resource resource)
        {
            var parentPath = RelavitePath(resource.getParent().getPath());
            string path = Path.Combine(this.BaseDirectory, parentPath);
            Directory.CreateDirectory(path);
        }

        private static string RelavitePath(string path)
        {
            var strippedPath = path;
            if (strippedPath.StartsWith("/"))
            {
                strippedPath = strippedPath.Remove(0, 1);
            }
            return strippedPath.Replace("/", "\\");
        }

        private StreamWriter CreateWriter(Resource resource)
        {

            string path = this.GetTargetPath(resource);
            return new StreamWriter(path, false, Encoding.UTF8);
        }

        private bool IsFreshEnough(string source)
        {
            TimeSpan ageInMillis = DateTime.Now.Subtract(File.GetLastWriteTime(source));
            return ageInMillis.TotalMilliseconds < FRESH_ENOUGH_MILLIS;
        }

        private string GetTargetPath(Resource resource)
        {
            return Path.Combine(this.BaseDirectory, RelavitePath(resource.getPath()));
        }

        private static void Copy(TextReader inputReader, TextWriter outputWriter)
        {
            const int bufferSize = 4096;
            var buffer = new char[bufferSize];
            var len = 0;
            while ((len = inputReader.Read(buffer, 0, bufferSize)) != -1)
            {
                outputWriter.Write(buffer, 0, len);
            }
        }

        #endregion

        #region Target Members

        public void write(Resource resource, string content)
        {
            Check.notNull(resource, "resource is null");
            this.MakeDirectories(resource);
            using (StreamWriter writer = this.CreateWriter(resource))
            {
                try
                {
                    writer.Write(content);
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        public void copyTo(Resource resource, InputStream inputStream)
        {
            Check.notNull(resource, "resource is null");
            this.MakeDirectories(resource);
            var outputFile = this.GetTargetPath(resource);
            // Do not overwrite if a recent copy already exists
            if (File.Exists(outputFile) && this.IsFreshEnough(outputFile))
            {
                return;
            }
            var reader = new StreamReader(new StreamWrapper(inputStream));
            var writer = new StreamWriter(outputFile);
            Copy(reader, writer);
            writer.Close();
        }

        public void delete(Resource resource)
        {
            Check.notNull(resource, "resource is null");
            File.Delete(this.BaseDirectory + resource.getPath());
        }

        public bool exists(Resource resource)
        {
            return File.Exists(this.resolvedPathFor(resource));
        }

        public string resolvedPathFor(Resource resource)
        {
            return this.GetTargetPath(resource);
        }

        public OutputStream getOutputStream(Resource resource)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
