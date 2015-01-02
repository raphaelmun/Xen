using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCollisionLifetimeTracker
    {
        [TestMethod]
        public void TrackerDoesNotContainsEntryForAnyCombinationOfNull()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            var entry = CollisionLifetimeEntry.Acquire( a, b, 0 );
            tracker.Add( entry );

            Assert.IsFalse( tracker.Contains( a, null ) );
            Assert.IsFalse( tracker.Contains( null, a ) );
            Assert.IsFalse( tracker.Contains( null, null ) );
        }

        [TestMethod]
        public void TrackerContainsEntryForAddedCollisionPair()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            tracker.Add( a, b, 0 );

            Assert.IsTrue( tracker.Contains( a, b ) );
            Assert.IsTrue( tracker.Contains( b, a ) );
        }

        [TestMethod]
        public void RemovingCollisionEntryWorks()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            var entry = CollisionLifetimeEntry.Acquire( a, b, 0 );
            tracker.Add( entry );

            tracker.Remove( entry );

            Assert.IsFalse( tracker.Contains( a, b ) );
            Assert.IsFalse( tracker.Contains( b, a ) );
        }

        [TestMethod]
        public void RemovingCollisionEntryOverloadWorks()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            var entry = CollisionLifetimeEntry.Acquire( a, b, 0 );
            tracker.Add( entry );

            tracker.Remove( a, b );

            Assert.IsFalse( tracker.Contains( a, b ) );
            Assert.IsFalse( tracker.Contains( b, a ) );
        }

        [TestMethod]
        public void RemovingCollisionEntryOverloadCommutativeWorks()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            var entry = CollisionLifetimeEntry.Acquire( a, b, 0 );
            tracker.Add( entry );

            tracker.Remove( b, a );

            Assert.IsFalse( tracker.Contains( a, b ) );
            Assert.IsFalse( tracker.Contains( b, a ) );
        }

        [TestMethod]
        public void AddingDuplicateCollisionEntryUpdatesOriginalCollisionTime()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            tracker.Add( a, b, 1 );

            Assert.IsTrue( tracker.Contains( a, b ) );

            tracker.Add( a, b, 2 );
            Assert.IsTrue( tracker.Contains( a, b ) );

            ICollisionLifetimeEntry returnEntry = null;
            Assert.IsTrue( tracker.Contains( a, b, out returnEntry ) );
            Assert.IsNotNull( returnEntry );
            Assert.AreEqual( 2, returnEntry.TimeOfCollision );
        }

        [TestMethod]
        public void ReleasingOneACollisionPartyCausesLifetimeEntryToBeRemoved()
        {
            var a = MockCollidableObject.Acquire();
            var b = MockCollidableObject.Acquire();
            var tracker = new CollisionLifetimeTracker();
            var entry = CollisionLifetimeEntry.Acquire( a, b, 0 );
            tracker.Add( entry );

            a.Release();
            Assert.IsFalse( tracker.Contains( a, b ) );
            Assert.IsFalse( tracker.Contains( b, a ) );
        }
    }
}