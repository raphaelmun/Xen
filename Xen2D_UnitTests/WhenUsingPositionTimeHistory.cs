using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingPositionTimeHistory2D
    {
        static TimeSpan Zero = new TimeSpan( 0, 0, 0 );
        static TimeSpan OneSecond = new TimeSpan( 0, 0, 1 );

        [TestMethod]
        public void OverloadedEqualityOperatorWorks()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 200 ) );
            PositionTimeEntry2D pte2 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 200 ) );
            PositionTimeEntry2D pte3 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 201 ) );
            
            Assert.AreEqual( pte1, pte2 );
            Assert.AreNotEqual( pte1, pte3 );
        }

        [TestMethod]
        public void InsertingTwoElementsWithIdenticalPositionAndAngleWorks()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 200 ) );
            PositionTimeEntry2D pte2 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 201 ) );

            PositionTimeHistory2D history = PositionTimeHistory2D.Acquire();
            history.Add( pte1 );
            history.Add( pte2 );

            Assert.AreEqual( pte1, history.SecondToLast );
            Assert.AreEqual( pte2, history.Last );
        }

        [TestMethod]
        public void PositionTimeEntry2DDisplacementYieldsExpected()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, Zero );
            PositionTimeEntry2D pte2 = new PositionTimeEntry2D( Vector2.UnitX, OneSecond );

            Assert.AreEqual( Vector2.UnitX, PositionTimeEntry2D.GetVelocity( pte1, pte2 ) );
        }

        [TestMethod]
        public void GetLastVelocityVectorYieldsZeroIfLessThanTwoEntries()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, Zero );

            PositionTimeHistory2D history = PositionTimeHistory2D.Acquire();
            history.Add( pte1 );

            Assert.AreEqual( history.LastVelocity, Vector2.Zero );
        }

        [TestMethod]
        public void AddingEntryWithSamePositionAgainsIncreasesCount()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, Zero );
            PositionTimeEntry2D pte2 = new PositionTimeEntry2D( Vector2.Zero, OneSecond );

            PositionTimeHistory2D history = PositionTimeHistory2D.Acquire();
            history.Add( pte1 );

            Assert.AreEqual( history.Count, 1 );
            history.Add( pte2 );
            Assert.AreEqual( history.Count, 2 );
        }

        [TestMethod]
        public void GetLastVelocityVectorWorksAsExpected()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, Zero );
            PositionTimeEntry2D pte2 = new PositionTimeEntry2D( Vector2.UnitX, OneSecond );

            PositionTimeHistory2D history = PositionTimeHistory2D.Acquire();
            history.Add( pte1 );
            history.Add( pte2 );

            Assert.AreEqual( history.LastVelocity, Vector2.UnitX );
        }

        [TestMethod]
        public void NewPositionTimeHistory2DReturnsNullForLast()
        {
            PositionTimeHistory2D history = PositionTimeHistory2D.Acquire();

            Assert.AreEqual( history.Last, PositionTimeEntry2D.Invalid );
        }

        [TestMethod]
        public void AddingOutOfOrderEntryThrowsException()
        {
            PositionTimeEntry2D pte1 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 200 ) );
            PositionTimeEntry2D pte2 = new PositionTimeEntry2D( Vector2.Zero, new TimeSpan( 201 ) );

            PositionTimeHistory2D history = PositionTimeHistory2D.Acquire();
            history.Add( pte2 );
            try
            {
                history.Add( pte1 );
                Assert.Fail();
            }
            catch( PositionHistoryOrderException )
            {
            }
        }
    }
}
