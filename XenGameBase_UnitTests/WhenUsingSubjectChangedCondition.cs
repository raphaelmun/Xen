using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using XenGameBase;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenUsingSubjectChangedCondition
    {
        [TestMethod]
        public void ConditionSubjectComparisonCase1()
        {
            float subject = 5.0f;
            ConditionSubjectChanged<float> condition = new ConditionSubjectChanged<float>( () => { return subject; } );
            GameTime gt = GameTimeUtility.GameTimeZero;
            Assert.IsTrue( condition.Evaluate( ref gt ) );
            Assert.IsFalse( condition.Evaluate( ref gt ) );
            subject = 6.0f;
            Assert.IsTrue( condition.Evaluate( ref gt ) );
        }
    }
}
