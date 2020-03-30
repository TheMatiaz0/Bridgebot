using TrualityEngine.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest
{
    [TestClass]
    public class TagTest
    {
        [TestMethod]
        public void TagFinderTest()
        {
            new Entity() { Tags = new Tags("aa", "ddd", "cc") };
            Assert.IsNotNull(Entity.FindEntityThatHasTags("aa"));
            Assert.IsNotNull(Entity.FindEntityThatHasTags("ddd"));
            Assert.IsNull(Entity.FindIdenticalTags(new Tags( "ddd", "cc")));
            Assert.IsNotNull(Entity.FindIdenticalTags(new Tags("ddd", "cc", "aa")));
        }
        [TestMethod]
        public void TagTest2()
        {
            Assert.IsTrue(new Tags("ddd", "aaa", "bb").Has(new Tags("aaa", "bb")));
            Assert.AreEqual(new Tags("ddd", "aaa").GetHashCode(), new Tags("aaa", "ddd").GetHashCode());
            
        }
    }
}
