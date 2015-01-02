using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    public class Source
    {
        public event Action<int> IntEvent;

        private Event<XenEventArgs> _xenEvent = new Event<XenEventArgs>();
        public Event<XenEventArgs> XenEvent { get { return _xenEvent; } }

        public void InvokeXenEvent( XenEventArgs args )
        {
            XenEvent.Notify( args );
        }

        public void InvokeIntEvent()
        {
            if( null != IntEvent )
            {
                IntEvent( 5 );
            }
        }
    }

    public struct XenEventArgs
    {
    }

    [TestClass]
    public class WhenUsingXenEvents
    {
        [TestMethod]
        public void InvokingXenEventCausesCallbackToBeCalled()
        {
            Source src = new Source();
            bool callbackInvoked = false;

            Action<XenEventArgs> handler = new Action<XenEventArgs>( args => { callbackInvoked = true; } );
            src.XenEvent.Add( handler );

            Assert.IsFalse( callbackInvoked );
            src.InvokeXenEvent( new XenEventArgs() );

            Assert.IsTrue( callbackInvoked );
        }

        [TestMethod]
        public void AddingDelegateMultipleTimesToEventOnlyGetsCalledbackOnce()
        {
            Source src = new Source();
            int numTimesCalled = 0;

            Action<XenEventArgs> cachedDelegate = new Action<XenEventArgs>( t =>
            {
                numTimesCalled++;
            } );

            for( int i = 0; i < 30; i++ )
            {
                src.XenEvent.Add( cachedDelegate );
            }

            src.InvokeXenEvent( new XenEventArgs() );
            Assert.AreEqual( 1, numTimesCalled );
        }
    }
}
