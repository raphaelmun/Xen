using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_GCTests
{
    [TestClass]
    public class WhenUsingEvents
    {
        public struct XenEventArgs { }

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

        [TestMethod]
        public void NormalEventsGeneratesGarbage()
        {
            Source src = new Source();

            HeapState hs = HeapTracker.Execute( () =>
            {
                for( int i = 0; i < 10000; i++ )
                {
                    src.IntEvent += src_IntEvent;
                    src.InvokeIntEvent();
                }
            } );

            Assert.IsTrue( hs.TotalMemoryAllocated > 380000 );
        }

        void src_IntEvent( int obj )
        {
        }

        [TestMethod]
        public void CachedDelegateAllocatesGarbage()
        {
            Source src = new Source();
            Action<int> cachedDelegate = new Action<int>( src_IntEvent );

            HeapUtility.GetHeapStateBefore();

            for( int i = 0; i < 10; i++ )
            {
                src.IntEvent += cachedDelegate;
            }

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreNotEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void AttachingCachedDelegatesToEventDoesNotGenerateGarbage()
        {
            Source src = new Source();
            Action<XenEventArgs> cachedDelegate = new Action<XenEventArgs>( t => { } );

            HeapUtility.GetHeapStateBefore();

            for( int i = 0; i < 30; i++ )
            {
                src.XenEvent.Add( cachedDelegate );
            }

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void RemovingDelegateFromEventDoesNotGenerateGarbage()
        {
            Source src = new Source();
            Action<XenEventArgs> cachedDelegate = new Action<XenEventArgs>( t => { } );

            src.XenEvent.Add( cachedDelegate );

            HeapUtility.GetHeapStateBefore();

            src.XenEvent.Remove( cachedDelegate );

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }

        [TestMethod]
        public void InvokeDoesNotGenerateGarbage()
        {
            Source src = new Source();
            Action<XenEventArgs> cachedDelegate = new Action<XenEventArgs>( t => { } );
            XenEventArgs args = new XenEventArgs();

            src.XenEvent.Add( cachedDelegate );

            HeapUtility.GetHeapStateBefore();

            src.XenEvent.Notify( args );

            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
