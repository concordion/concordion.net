using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Configuration
{
    class ExampleDerivedFixtureWithFieldAttributes : ExampleFixtureBaseWithFieldAttributes
    {
        [NET.Api.Extension.Extension]
        public ConcordionExtension extension = new FakeExtension1("ExampleExtension");
    }
}
