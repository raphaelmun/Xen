using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using XenGameBase;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenUsingSubjectIsNullCondition
    {
        [TestMethod]
        public void NullSubjectEvaluatesToTrue()
        {
            object subj = null;
            ConditionSubjectIsNull condition = new ConditionSubjectIsNull( () =>
            { return subj; } );

            GameTime gt = GameTimeUtility.GameTimeZero;
            Assert.IsTrue( condition.Evaluate( ref gt ) );

            subj = "meow";

            Assert.IsFalse( condition.Evaluate( ref gt ) );
        }
    }

    [TestClass]
    public class WhenUsingSubjectIsNotNullCondition
    {
        [TestMethod]
        public void NullSubjectEvaluatesToFalse()
        {
            object subj = null;
            ConditionSubjectIsNotNull condition = new ConditionSubjectIsNotNull( () =>
            { return subj; } );

            GameTime gt = GameTimeUtility.GameTimeZero;
            Assert.IsFalse( condition.Evaluate( ref gt ) );

            subj = "meow";

            Assert.IsTrue( condition.Evaluate( ref gt ) );
        }
    }
}
