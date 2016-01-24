using java.lang;

namespace nu.xom
{
    public class ParsingException : Exception
    {
        public ParsingException()
        { }

        public ParsingException(System.Exception exception) 
            : base(exception) 
        { }
    }
}
