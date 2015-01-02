using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCollisionRuleSetAll
    {
        [TestMethod]
        public void InstanceDoesNotContainRulesForNotCollidable()
        {
            var ruleset = CollisionRuleSetAll.Instance;
            Assert.IsFalse( ruleset.ContainsRulesFor( CollisionClasses.DoesNotCollide ) );
        }

        [TestMethod]
        public void CannotAddRules()
        {
            try
            {
                CollisionRuleSetAll.Instance.AddRule( 1, 2 );
                Assert.Fail( "should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void CannotRemoveRules()
        {
            try
            {
                CollisionRuleSetAll.Instance.RemoveRule( 1, 2 );
                Assert.Fail( "should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void CanCollideReturnsTrueForAllNonZeroProducts()
        {
            Assert.IsTrue( CollisionRuleSetAll.Instance.CanCollide( 1, 2 ) );
            Assert.IsTrue( CollisionRuleSetAll.Instance.CanCollide( 3, 4 ) );
            Assert.IsTrue( CollisionRuleSetAll.Instance.CanCollide( 5, 6 ) );
        }

        [TestMethod]
        public void CanCollideReturnsFalseForAllZeroProducts()
        {
            Assert.IsFalse( CollisionRuleSetAll.Instance.CanCollide( 0, 2 ) );
            Assert.IsFalse( CollisionRuleSetAll.Instance.CanCollide( 3, 0 ) );
            Assert.IsFalse( CollisionRuleSetAll.Instance.CanCollide( 0, 6 ) );
        }

        [TestMethod]
        public void CanCollideReturnsTrueForAllNonZeroProductsWithNullReturnedRule()
        {
            CollisionRuleEntry rule;
            Assert.IsTrue( CollisionRuleSetAll.Instance.CanCollide( 1, 2, out rule ) );
            Assert.IsNull( rule );
        }
    }
}
