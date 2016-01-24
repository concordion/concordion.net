using System;
using System.Resources;
using java.io;

namespace Concordion.NET.Internal.Util
{
    public class IOUtil : org.concordion.@internal.util.IOUtil
    {
        public override string readResourceAsString(string resourcePath)
        {
            var resourceManager = new ResourceManager("Concordion.NET.HtmlFramework", typeof(IOUtil).Assembly);
            var resourceName = resourcePath.Substring(resourcePath.LastIndexOf('/') + 1);
            var resourceAsString = resourceManager.GetString(resourceName);
            var result = resourceAsString.Replace("\r", "");
            return result;
        }

        public override string readResourceAsString(string resourcePath, string charsetName)
        {
            throw new NotSupportedException("not supported by Concordion.NET");
        }

        public override string readAsString(Reader reader)
        {
            throw new NotSupportedException("not supported by Concordion.NET");
        }

        public override InputStream getResourceAsStream(string resourcePath)
        {
            throw new NotSupportedException("not supported by Concordion.NET");
        }
    }
}
