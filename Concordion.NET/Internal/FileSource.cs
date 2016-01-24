using System.IO;
using System.Reflection;
using System.Text;
using org.concordion.api;
using InputStream = java.io.InputStream;
using ikvm.io;

namespace Concordion.NET.Internal
{
    public class FileSource : Source
    {
        #region Constants

        private const char PathSeparator = '\\';

        #endregion

        #region Properties

        private string BaseDirectory
        {
            get;
            set;
        }

        private Assembly FixtureAssembly
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public FileSource(Assembly fixtureAssembly, string baseDirectory)
        {
            this.FixtureAssembly = fixtureAssembly;
            this.BaseDirectory = Path.GetFullPath(baseDirectory);
        }

        #endregion

        #region Source Members

        public InputStream createInputStream(Resource resource)
        {
            return new InputStreamWrapper(new FileStream(this.ExistingFilePath(resource), FileMode.Open));
        }

        public bool canFind(Resource resource)
        {
            return ExistingFilePath(resource) != null;
        }

        #endregion

        #region private methods

        private string ExistingFilePath(Resource resource)
        {
            var resourcePath = resource.getPath().Replace("/", "\\");
            if (resourcePath.StartsWith("\\"))
            {
                resourcePath = resourcePath.Substring(1);
            }
            var filePath = Path.Combine(BaseDirectory, resourcePath);
            if (File.Exists(filePath))
            {
                return filePath;
            }
            filePath = Path.Combine(BaseDirectory, 
                this.RemoveFirst(resourcePath, FixtureAssembly.GetName().Name.Replace('.', PathSeparator) + PathSeparator));
            if (File.Exists(filePath))
            {
                return filePath;
            }
            return null;
        }

        private string RemoveFirst(string str, string toRemove)
        {
            if (string.IsNullOrEmpty(toRemove)) return string.Empty;
            var index = str.IndexOf(toRemove);
            var builder = new StringBuilder();

            if (index != -1)
            {
                builder.Append(str.Substring(0, index));
                builder.Append(str.Substring(index + toRemove.Length));
                return builder.ToString();
            }

            return str;
        }

        #endregion
    }
}
