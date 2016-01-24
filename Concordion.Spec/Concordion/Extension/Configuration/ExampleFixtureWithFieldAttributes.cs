using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Configuration
{
    public class ExampleFixtureWithFieldAttributes
    {
        [NET.Api.Extension.Extension]
        public ConcordionExtension extension = new FakeExtension1();

        [NET.Api.Extension.Extension]
        public FakeExtension2 extension2 = new FakeExtension2();
    }
}
