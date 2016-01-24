using org.concordion.api;

namespace Concordion.NET.Internal
{
    public class ClassNameBasedSpecificationLocator : SpecificationLocator
    {
        #region ISpecificationLocator Members

        private string m_SpecificationSuffix;

        public ClassNameBasedSpecificationLocator() : this("html") { }

        public ClassNameBasedSpecificationLocator(string mSpecificationSuffix)
        {
            this.m_SpecificationSuffix = mSpecificationSuffix;
        }

        public Resource locateSpecification(object fixture)
        {
            var fixtureName = fixture.GetType().ToString();
            fixtureName = fixtureName.Replace(".", "\\");
            if (fixtureName.EndsWith("Test"))
            {
                fixtureName = fixtureName.Substring(0, fixtureName.Length - 4);
            }
            else if (fixtureName.EndsWith("Fixture"))
            {
                fixtureName = fixtureName.Substring(0, fixtureName.Length - 7);
            }
            
            var path = "\\" + fixtureName + "." + m_SpecificationSuffix;
            return new Resource(path);
        }

        #endregion
    }
}
