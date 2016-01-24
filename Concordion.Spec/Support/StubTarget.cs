using System;
using System.Collections.Generic;
using System.Linq;
using java.io;
using org.concordion.api;
using org.concordion.@internal.util;

namespace Concordion.Spec.Support
{
    class StubTarget : Target
    {
        private readonly Dictionary<Resource, String> writtenStrings;
        private readonly List<Resource> m_CopiedResources = new List<Resource>();

        public StubTarget()
        {
            this.writtenStrings = new Dictionary<Resource, string>();
        }

        public string GetWrittenString(Resource resource)
        {
            Check.isTrue(this.writtenStrings.ContainsKey(resource), "Expected resource '" + resource.getPath() + "' was not written to target");
            return this.writtenStrings[resource];
        }

        #region Target Members

        public void write(Resource resource, string image)
        {
            this.writtenStrings.Add(resource, image);
        }

        public void CopyTo(Resource resource, string destination)
        {
        }

        public void copyTo(Resource resource, InputStream inputStream)
        {
            this.m_CopiedResources.Add(resource);
        }

        public bool exists(Resource resource)
        {
            return this.HasCopiedResource(resource) || this.writtenStrings.ContainsKey(resource);
        }

        public void delete(Resource resource)
        {
        }

        public OutputStream getOutputStream(Resource r)
        {
            throw new NotImplementedException();
        }

        public string resolvedPathFor(Resource resource)
        {
            return "";
        }

        #endregion

        public bool HasCopiedResource(Resource resource)
        {
            return this.m_CopiedResources.Contains(resource);
        }
    }
}
