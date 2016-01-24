using System;
using System.Reflection;
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

        public bool canFind(Resource resource)
        {
            var fullyQualifiedTypeName = this.ConvertPathToNamespace(resource.getPath());
            return this.FixtureAssembly.GetManifestResourceInfo(fullyQualifiedTypeName) != null;
        }

        public InputStream createInputStream(Resource resource)
        {
            var fullyQualifiedTypeName = this.ConvertPathToNamespace(resource.getPath());

            if (canFind(resource))
            {
                var manifestResourceStream = this.FixtureAssembly.GetManifestResourceStream(fullyQualifiedTypeName);
                return new InputStreamWrapper(manifestResourceStream);
            }

            throw new InvalidOperationException(string.Format("Cannot open the resource {0}", fullyQualifiedTypeName));
        }

        #endregion
    }
}
