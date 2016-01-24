using System;
using System.Reflection;
using System.IO;
using java.io;
using org.concordion.api;
using ikvm.io;

namespace Concordion.NET.Internal
{
    public class EmbeddedResourceSource : Source
    {
        #region Properties

        public Assembly FixtureAssembly
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public EmbeddedResourceSource(Assembly fixtureAssembly)
        {
            this.FixtureAssembly = fixtureAssembly;
        }

        #endregion

        #region Methods

        private string ConvertPathToNamespace(string path)
        {
            var dottedPath = path.Replace('\\', '.');
            dottedPath = dottedPath.Replace('/', '.');
            if (dottedPath[0] == '.')
            {
                dottedPath = dottedPath.Remove(0, 1);
            }
            return dottedPath;
        }

        #endregion

        #region Source Members

        public bool canFind(org.concordion.api.Resource resource)
        {
            var fullyQualifiedTypeName = ConvertPathToNamespace(resource.getPath());
            return FixtureAssembly.GetManifestResourceInfo(fullyQualifiedTypeName) != null;
        }

        public InputStream createInputStream(org.concordion.api.Resource resource)
        {
            var fullyQualifiedTypeName = ConvertPathToNamespace(resource.getPath());

            if (canFind(resource))
            {
                Stream manifestResourceStream = FixtureAssembly.GetManifestResourceStream(fullyQualifiedTypeName);
                return new InputStreamWrapper(manifestResourceStream);
            }

            throw new InvalidOperationException(String.Format("Cannot open the resource {0}", fullyQualifiedTypeName));
        }

        #endregion
    }
}
