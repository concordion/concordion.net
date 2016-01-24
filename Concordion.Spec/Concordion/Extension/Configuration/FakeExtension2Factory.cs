using System;
using System.Collections.Generic;
using System.Linq;
using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Configuration
{
    public class FakeExtension2Factory : ConcordionExtensionFactory
    {
        public ConcordionExtension createExtension()
        {
            return new FakeExtension2("FakeExtension2FromFactory");
        }
    }
}
