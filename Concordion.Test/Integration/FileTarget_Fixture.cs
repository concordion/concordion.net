using Concordion.NET.Internal.Util;
using java.io;
using NUnit.Framework;
using org.concordion.api;
using org.concordion.@internal;

namespace Concordion.Test.Integration
{
    [TestFixture]
    public class FileTarget_Fixture
    {
        [Test]
        public void Test_Can_Get_File_Path_Successfully()
        {
            var resource = new Resource("\\blah\\blah.txt");

            var target = new FileTarget(new File(@"c:\temp"), new IOUtil());

            Assert.AreEqual(@"c:\temp\blah\blah.txt", target.resolvedPathFor(resource));
        }
    }
}
