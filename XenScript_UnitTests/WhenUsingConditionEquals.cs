using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using XenAspects;
using XenGameBase;
using XenScript;

namespace XenScript_UnitTests
{
    [TestClass]
    public class WhenUsingConditionEquals
    {
        [TestMethod]
        public void ComparesToWorksAsExpected()
        {
            int i = 5, j = 5, k = 6;
            Assert.IsTrue( i.CompareTo( j ) == 0 );
            Assert.IsTrue( i.CompareTo( k ) == -1 );
            Assert.IsTrue( k.CompareTo( i ) == 1 );
        }

        [TestMethod]
        public void ConditionSubjectComparisonCase1()
        {
            int subject = 5;
            int target = 5;
            ConditionSubjectEqualsTarget<int> equals = new ConditionSubjectEqualsTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );
            ConditionSubjectGreaterThanTarget<int> greater = new ConditionSubjectGreaterThanTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );
            ConditionSubjectLessThanTarget<int> less = new ConditionSubjectLessThanTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );

            GameTime gt = GameTimeUtility.GameTimeZero;
            Assert.IsTrue( equals.Evaluate( ref gt ) );
            Assert.IsFalse( greater.Evaluate( ref gt ) );
            Assert.IsFalse( less.Evaluate( ref gt ) );
        }

        [TestMethod]
        public void ConditionSubjectComparisonCase2()
        {
            int subject = 4;
            int target = 5;
            ConditionSubjectEqualsTarget<int> equals = new ConditionSubjectEqualsTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );
            ConditionSubjectGreaterThanTarget<int> greater = new ConditionSubjectGreaterThanTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );
            ConditionSubjectLessThanTarget<int> less = new ConditionSubjectLessThanTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );

            GameTime gt = GameTimeUtility.GameTimeZero;
            Assert.IsFalse( equals.Evaluate( ref gt ) );
            Assert.IsFalse( greater.Evaluate( ref gt ) );
            Assert.IsTrue( less.Evaluate( ref gt ) );
        }

        [TestMethod]
        public void ConditionSubjectComparisonCase3()
        {
            int subject = 6;
            int target = 5;
            ConditionSubjectEqualsTarget<int> equals = new ConditionSubjectEqualsTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );
            ConditionSubjectGreaterThanTarget<int> greater = new ConditionSubjectGreaterThanTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );
            ConditionSubjectLessThanTarget<int> less = new ConditionSubjectLessThanTarget<int>( target, new Getter<int>( () =>
            { return subject; } ) );

            GameTime gt = GameTimeUtility.GameTimeZero;
            Assert.IsFalse( equals.Evaluate( ref gt ) );
            Assert.IsTrue( greater.Evaluate( ref gt ) );
            Assert.IsFalse( less.Evaluate( ref gt ) );
        }
    }
}
