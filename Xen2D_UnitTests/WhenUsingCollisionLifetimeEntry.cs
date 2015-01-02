using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;
using XenAspects;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCollisionLifetimeEntry
    {
        [TestMethod]
        public void AttachingToAnyCombinationOfNullThrowsException()
        {
            try
            {
                var cle = CollisionLifetimeEntry.Acquire( null, null, 0 );
                Assert.Fail( "should not reach here" );
            }
            catch( ArgumentNullException ) { }

            try
            {
                var cle = CollisionLifetimeEntry.Acquire( null, new MockCollidableObject(), 0 );
                Assert.Fail( "should not reach here" );
            }
            catch( ArgumentNullException ) { }
            
            try
            {
                var cle = CollisionLifetimeEntry.Acquire( new MockCollidableObject(), null, 0 );
                Assert.Fail( "should not reach here" );
            }
            catch( ArgumentNullException ) { }
        }

        [TestMethod]
        public void ResettingInstancesAttachesToSpecifiedCollidables()
        {
            var mco1 = new MockCollidableObject();
            var mco2 = new MockCollidableObject();

            Assert.AreEqual( 0, mco1.OnReleased.NumSubscribers );
            Assert.AreEqual( 0, mco2.OnReleased.NumSubscribers );

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            Assert.AreEqual( 1, mco1.OnReleased.NumSubscribers );
            Assert.AreEqual( 1, mco2.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void ReleasingFirstEntryObjectCausesBothToBeUnsubscribed()
        {
            var mco1 = MockCollidableObject.Acquire();
            var mco2 = MockCollidableObject.Acquire();

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            mco1.Release();

            Assert.AreEqual( 0, mco1.OnReleased.NumSubscribers );
            Assert.AreEqual( 0, mco2.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void ReleasingSecondEntryObjectCausesBothToBeUnsubscribed()
        {
            var mco1 = MockCollidableObject.Acquire();
            var mco2 = MockCollidableObject.Acquire();

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            mco2.Release();

            Assert.AreEqual( 0, mco1.OnReleased.NumSubscribers );
            Assert.AreEqual( 0, mco2.OnReleased.NumSubscribers );
        }

        [TestMethod]
        public void ReleasingCollisionParticipantCausesCollisionLifetimeEntryToRelease()
        {
            var mco1 = MockCollidableObject.Acquire();
            var mco2 = MockCollidableObject.Acquire();

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            bool releaseCalled = false;
            cle.OnReleased.Add( ( IPooledObject obj ) =>
                {
                    releaseCalled = true;
                } );

            Assert.IsFalse( releaseCalled );

            mco2.Release();

            Assert.IsTrue( releaseCalled );
        }

        [TestMethod]
        public void GetCombinedHashCodeReturnsXorOfCollidedObjectHashes()
        {
            var mco1 = MockCollidableObject.Acquire();
            var mco2 = MockCollidableObject.Acquire();

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            int expectedHashCode = mco1.GetHashCode() ^ mco2.GetHashCode();
            Assert.AreEqual( expectedHashCode, cle.GetCombinedHash() );
        }

        [TestMethod]
        public void CanCollideAgainReturnsTrueIfElapsedTimeIsGreatherThanCooldown()
        {
            var mco1 = MockCollidableObject.Acquire();
            var mco2 = MockCollidableObject.Acquire();

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            Assert.IsTrue( cle.CanCollideAgain( 10, 11 ) );
        }

        [TestMethod]
        public void CanCollideAgainReturnsFalseIfElapsedTimeIsLessThanCooldown()
        {
            var mco1 = MockCollidableObject.Acquire();
            var mco2 = MockCollidableObject.Acquire();

            var cle = CollisionLifetimeEntry.Acquire( mco1, mco2, 0 );

            Assert.IsFalse( cle.CanCollideAgain( 10, 9 ) );
        }

        [TestMethod]
        public void ParameterlessAcquireReturnsInvalidCombinedHash()
        {
            var cle = CollisionLifetimeEntry.Acquire();
            Assert.AreEqual( CollisionLifetimeEntry.InvalidCombinedHash, cle.GetCombinedHash() );
        }
    }
}
