using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingCompositeExtent
    {
        [TestMethod]
        public void ChangingCompositeExtentScaleDoesNotChangeComponentScale()
        {
            RectangularExtent extent = RectangularExtent.Acquire();
            extent.Reset( new Vector2( 100, 100 ) );
            extent.Scale = new Vector2( 2, 2 );
            CompositeExtent ce = new CompositeExtent();
            ce.Add( extent );
            Assert.AreEqual( ce.Scale, new Vector2( 1, 1 ) );

            ce.Scale = new Vector2( 3, 4 );

            Assert.AreEqual( ce.Scale, new Vector2( 3, 4 ) );
            Assert.AreEqual( extent.Scale, new Vector2( 2, 2 ) );
        }

        [TestMethod]
        public void ChangingCompositeExtentAngleDoesNotChangeComponentAngle()
        {
            RectangularExtent extent = RectangularExtent.Acquire();
            extent.Reset( new Vector2( 100, 100 ) );
            extent.Angle = 1.5f;
            CompositeExtent ce = new CompositeExtent();
            ce.Add( extent );
            ce.Angle = 1.1f;

            Assert.AreEqual( ce.Angle, 1.1f );
            Assert.AreEqual( extent.Angle, 1.5f );
        }

        [TestMethod]
        public void ChangingCompositeExtentAnchorDoesNotChangeComponentAnchor()
        {
            RectangularExtent extent = RectangularExtent.Acquire();
            extent.Reset( new Vector2( 100, 100 ) );

            CompositeExtent ce = new CompositeExtent();
            ce.Add( extent );

            Assert.AreEqual( extent.Anchor, Vector2.Zero );

            ce.Anchor = new Vector2( 50, 50 );

            Assert.AreEqual( ce.Anchor, new Vector2( 50, 50 ) );
            Assert.AreEqual( extent.Anchor, new Vector2( 0, 0 ) );
        }

        //[TestMethod]
        //public void CompositeExtentWithSingleExtentCollidesSimpleCase1()
        //{
        //    RectangularExtent extent = RectangularExtent.Acquire();
        //    extent.Reset( new Vector2( 100, 100 ) );

        //    RectangularExtent collideTarget = RectangularExtent.Acquire();
        //    extent.Reset( new Vector2( 1, 1 ) );
        //    extent.Anchor = new Vector2( 99, 99 );

        //    CompositeExtent ce = new CompositeExtent();
        //    ce.Add( extent );

        //    Assert.IsTrue( ce.Intersects( collideTarget ) );
        //}

        [TestMethod]
        public void CompositeExtentWithTwoExtentsBoundsUpdatesCorrectlyCase1()
        {
            RectangularExtent first = RectangularExtent.Acquire();
            first.Reset( new Vector2( 100, 100 ) );

            //The second rect completely encompasses the first rect.
            RectangularExtent second = RectangularExtent.Acquire();
            second.Reset( new Vector2( -100, -100 ), 300, 300 );
            CompositeExtent ce = new CompositeExtent();

            ce.Add( first );

            Assert.AreEqual( 100, ce.HighestX );
            Assert.AreEqual( 100, ce.HighestY);
            Assert.AreEqual( 0, ce.LowestX );
            Assert.AreEqual( 0, ce.LowestY);

            ce.Add( second );

            Assert.AreEqual( 200, ce.HighestX );
            Assert.AreEqual( 200, ce.HighestY );
            Assert.AreEqual( -100, ce.LowestX );
            Assert.AreEqual( -100, ce.LowestY );
        }

        [TestMethod]
        public void CompositeExtentWithTwoExtentsBoundsUpdatesCorrectlyCase2()
        {
            RectangularExtent first = RectangularExtent.Acquire();
            first.Reset( new Vector2( 100, 100 ) );
            RectangularExtent second = RectangularExtent.Acquire();
            second.Reset( new Vector2( -100, -100 ), 100, 100 );
            CompositeExtent ce = new CompositeExtent();

            ce.Add( first );

            Assert.AreEqual( 100, ce.HighestX );
            Assert.AreEqual( 100, ce.HighestY );
            Assert.AreEqual( 0, ce.LowestX );
            Assert.AreEqual( 0, ce.LowestY );

            ce.Add( second );

            Assert.AreEqual( 100, ce.HighestX );
            Assert.AreEqual( 100, ce.HighestY );
            Assert.AreEqual( -100, ce.LowestX );
            Assert.AreEqual( -100, ce.LowestY );
        }

        [TestMethod]
        public void NewOriginShouldUpdateAnchorCase1()
        {
            CompositeExtent extent = new CompositeExtent();

            Assert.AreEqual( extent.Anchor, Vector2.Zero );
            extent.Origin = new Vector2( 2, 2 );
            Assert.AreEqual( extent.Anchor, new Vector2( 2, 2 ) );
        }

        [TestMethod]
        public void NewOriginShouldUpdateAnchorCase2()
        {
            CompositeExtent extent = new CompositeExtent();
            extent.Anchor = new Vector2( 3, 4 );

            extent.Origin = new Vector2( 2, 2 );
            Assert.AreEqual( extent.Anchor, new Vector2( 5, 6 ) );
        }

        [TestMethod]
        public void AddingChildToCompositeExtentReflectsChangeInChildAnchor()
        {
            CompositeExtent ce = new CompositeExtent();
            ce.Anchor = new Vector2( 100, 100 );
            RectangularExtent extent = new RectangularExtent();
            extent.Reset( new Vector2( 10, 10 ) );
            extent.Anchor = new Vector2( 110, 110 );

            Assert.AreEqual( extent.Anchor, new Vector2( 110, 110 ) );
            Assert.AreEqual( extent.Origin, Vector2.Zero );
            Assert.AreEqual( ce.Anchor, new Vector2( 100, 100 ) );

            ce.Add( extent );

            Assert.AreEqual( extent.Anchor, new Vector2( 110, 110 ) );
            Assert.AreEqual( Vector2.Zero, extent.Origin );
        }

        [TestMethod]
        public void ExtentActualRectUpdatesWhenCompositeExtentIsRotated()
        {
            RectangularExtent extent = RectangularExtent.Acquire();
            extent.Reset( 0, 0, 10, 20 );
            CompositeExtent ce = new CompositeExtent();
            ce.Add( extent );

            Assert.AreEqual( 10, ce.HighestX );
            Assert.AreEqual( 0, ce.LowestX );
            Assert.AreEqual( 20, ce.HighestY );
            Assert.AreEqual( 0, ce.LowestY );

            ce.Angle = MathHelper.PiOver2;

            Assert.AreEqual( 0, ce.HighestX );
            Assert.AreEqual( -20, ce.LowestX );
            Assert.AreEqual( 10, ce.HighestY );
            AssertHelper.AreApproximatelyEqual( 0, ce.LowestY );
        }

        [TestMethod]
        public void ExtentActualRectUpdatesWhenCompositeExtentIsScaled()
        {
            RectangularExtent extent = RectangularExtent.Acquire();
            extent.Reset( 0, 0, 10, 20 );

            CompositeExtent ce = new CompositeExtent();
            ce.Add( extent );

            Assert.AreEqual( 10, ce.HighestX );
            Assert.AreEqual( 0, ce.LowestX );
            Assert.AreEqual( 20, ce.HighestY );
            Assert.AreEqual( 0, ce.LowestY );

            ce.Scale = new Vector2( 2, 2 );

            Assert.AreEqual( 20, ce.HighestX );
            Assert.AreEqual( 0, ce.LowestX );
            Assert.AreEqual( 40, ce.HighestY );
            Assert.AreEqual( 0, ce.LowestY );
        }

        [TestMethod]
        public void ExtentActualRectUpdatesWhenCompositeExtentIsMoved()
        {
            RectangularExtent extent = RectangularExtent.Acquire();
            extent.Reset( 0, 0, 10, 20 );
            
            CompositeExtent ce = new CompositeExtent();
            ce.Add( extent );
            Assert.AreEqual( 10, ce.HighestX );
            Assert.AreEqual( 0, ce.LowestX );
            Assert.AreEqual( 20, ce.HighestY );
            Assert.AreEqual( 0, ce.LowestY );

            ce.Anchor = new Vector2( 10, 10 );

            Assert.AreEqual( 20, ce.HighestX );
            Assert.AreEqual( 10, ce.LowestX );
            Assert.AreEqual( 30, ce.HighestY );
            Assert.AreEqual( 10, ce.LowestY );
        }

        [TestMethod]
        public void AddingChildToCompositeUpdatesParentSpace()
        {
            RectangularExtent child = new RectangularExtent();
            CompositeExtent parent = new CompositeExtent();

            Assert.AreEqual( null, child.ParentSpace );
            
            parent.Add( child );
            Assert.AreNotEqual( null, child.ParentSpace );
        }

        [TestMethod]
        public void RemovingChildFromParentSetsParentSpaceToNull()
        {
            RectangularExtent child = new RectangularExtent();
            CompositeExtent parent = new CompositeExtent();

            parent.Add( child );
            Assert.AreNotEqual( null, child.ParentSpace );

            parent.Remove( child );
            Assert.AreEqual( null, child.ParentSpace );
        }

        [TestMethod]
        public void ParentSubscribesToAddedChildChangeEvents()
        {
            RectangularExtent child = new RectangularExtent();
            CompositeExtent parent = new CompositeExtent();

            Assert.AreEqual( 0, child.OnChanged.NumSubscribers );
            parent.Add( child );
            Assert.AreEqual( 1, child.OnChanged.NumSubscribers );
        }

        [TestMethod]
        public void ParentUnsubscribesFromRemovedChildChangeEvents()
        {
            RectangularExtent child = new RectangularExtent();
            CompositeExtent parent = new CompositeExtent();

            parent.Add( child );
            parent.Remove( child );
            Assert.AreEqual( 0, child.OnChanged.NumSubscribers );
        }

        [TestMethod]
        public void ReleasingChildCausesItToBeRemovedFromParent()
        {
            RectangularExtent child = RectangularExtent.Acquire();
            CompositeExtent parent = CompositeExtent.Acquire();

            parent.Add( child );
            Assert.AreEqual( 1, parent.Items.Count );

            child.Release();

            Assert.AreEqual( 0, parent.Items.Count );
        }

        [TestMethod]
        public void ResettingChildCausesItToBeRemovedFromParent()
        {
            RectangularExtent child = RectangularExtent.Acquire();
            CompositeExtent parent = CompositeExtent.Acquire();

            parent.Add( child );
            Assert.AreEqual( 1, parent.Items.Count );

            child.Reset();

            Assert.AreEqual( 0, parent.Items.Count );
        }

        [TestMethod]
        public void NewCompositeExtentsHaveFourSides()
        {
            CompositeExtent ce = new CompositeExtent();

            Assert.AreEqual( 4, ce.NumSides );
        }
    }
}
