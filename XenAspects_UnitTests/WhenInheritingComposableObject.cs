using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenInheritingComposableObject
    {
        class MockComposable : ComposableObject<MockComposable>
        {
            public bool ResetDirectStateCalled = false;

            protected override void ResetDirectState()
            {
                base.ResetDirectState();
                ResetDirectStateCalled = true;
            }
        }

        //[TestMethod]
        //public void DerivedInstanceCallsResetWhenConstructed()
        //{
        //    var mock = new MockComposable();
        //    Assert.IsTrue( mock.ResetDirectStateCalled );
        //}
    }
}
