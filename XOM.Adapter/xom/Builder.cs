using System;
using System.IO;
using System.Xml.Linq;
using java.io;

namespace nu.xom
{
    public class Builder
    {
        public Document build(InputStream inputStream)
        {
            try
            {
                var streamWrapper = new util.io.StreamWrapper(inputStream);
                TextReader textReader = new StreamReader(streamWrapper);
                var xDocument = XDocument.Load(textReader);
                inputStream.close();
                return new Document(xDocument);
            }
            catch (Exception exception)
            {
                throw new ParsingException(exception);
            }
        }
    }
}
