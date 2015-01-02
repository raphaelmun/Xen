using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    [TestClass]
    public class WhenUsingFireOnceEvent
    {
        [TestMethod]
        public void MultipleNotifiesOnlyCallsbackOnce()
        {
            FireOnceEvent<object> foe = new FireOnceEvent<object>();
            int timesCalled = 0;
            foe.Add( t => { timesCalled++; } );

            Assert.AreEqual( 0, timesCalled );
            foe.Notify( null );
            foe.Notify( null );

            Assert.AreEqual( 1, timesCalled );
        }
    }
}
