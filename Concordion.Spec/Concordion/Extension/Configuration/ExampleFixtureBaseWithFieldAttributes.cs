using Concordion.NET.Api.Extension;

namespace Concordion.Spec.Concordion.Extension.Configuration
{
    public class ExampleFixtureBaseWithFieldAttributes
    {
        [Extension]
        public FakeExtension2 extension2 = new FakeExtension2("SuperExtension");
    }
}
