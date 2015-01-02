using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCollisionRuleEntry
    {
        [TestMethod]
        public void SimpleInvolvesWorks()
        {
            var entry = new CollisionRuleEntry( 1, 2 );
            Assert.IsTrue( entry.Involves( 1, 2 ) );
        }

        [TestMethod]
        public void SwappedInvolvesWorks()
        {
            var entry = new CollisionRuleEntry( 1, 2 );
            Assert.IsTrue( entry.Involves( 2, 1 ) );
        }

        [TestMethod]
        public void SimpleInvolvesNegativeCase1Works()
        {
            var entry = new CollisionRuleEntry( 1, 2 );
            Assert.IsFalse( entry.Involves( 1, 3 ) );
        }

        [TestMethod]
        public void SimpleInvolvesNegativeCase2Works()
        {
            var entry = new CollisionRuleEntry( 1, 2 );
            Assert.IsFalse( entry.Involves( 3, 2 ) );
        }
    }
}
