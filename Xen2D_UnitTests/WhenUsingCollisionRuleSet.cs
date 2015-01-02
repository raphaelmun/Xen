using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCollisionRuleSet
    {
        [TestMethod]
        public void AddingRuleToCollisionRuleSetShouldContainRulesForBoth()
        {
            CollisionRuleSet rules = new CollisionRuleSet();

            Assert.IsFalse( rules.ContainsRulesFor( 1 ) );
            Assert.IsFalse( rules.ContainsRulesFor( 2 ) );

            rules.AddRule( 1, 2 );

            Assert.IsTrue( rules.ContainsRulesFor( 1 ) );
            Assert.IsTrue( rules.ContainsRulesFor( 2 ) );
        }

        [TestMethod]
        public void ClearingErasesAllRules()
        {
            CollisionRuleSet rules = new CollisionRuleSet();

            rules.AddRule( 1, 2 );

            Assert.IsTrue( rules.ContainsRulesFor( 1 ) );
            Assert.IsTrue( rules.ContainsRulesFor( 2 ) );

            rules.Clear();

            Assert.IsFalse( rules.ContainsRulesFor( 1 ) );
            Assert.IsFalse( rules.ContainsRulesFor( 2 ) );
        }

        [TestMethod]
        public void SpecifiedRuleShouldIndicateCollisionBetweenTwoClasses()
        {
            CollisionRuleSet rules = new CollisionRuleSet();

            Assert.IsFalse( rules.CanCollide( 1, 2 ) );

            rules.AddRule( 1, 2 );

            Assert.IsTrue( rules.CanCollide( 1, 2 ) );
        }

        [TestMethod]
        public void ComplexCollisionBetweenThreeClassesShouldWork()
        {
            CollisionRuleSet rules = new CollisionRuleSet();

            rules.AddRule( 1, 2 );
            rules.AddRule( 2, 3 );

            Assert.IsTrue( rules.CanCollide( 1, 2 ) );
            Assert.IsTrue( rules.CanCollide( 2, 3 ) );
            Assert.IsFalse( rules.CanCollide( 1, 3 ) );
        }

        [TestMethod]
        public void RemovingUnspecifiedRuleShouldReturnFalse()
        {
            CollisionRuleSet rules = new CollisionRuleSet();

            Assert.IsFalse( rules.RemoveRule( 1, 2 ) );
        }

        [TestMethod]
        public void RemovingSpecifiedRuleShouldReturnTrue()
        {
            CollisionRuleSet rules = new CollisionRuleSet();
            rules.AddRule( 1, 2 );

            Assert.IsTrue( rules.RemoveRule( 1, 2 ) );

            Assert.IsFalse( rules.ContainsRulesFor( 1 ) );
            Assert.IsFalse( rules.ContainsRulesFor( 2 ) );
        }
    }
}
