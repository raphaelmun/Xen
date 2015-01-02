using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingDrawable2DComparers_DecrementingDrawable2DComparer
    {
        class MockDrawable : Renderable2DBase<MockDrawable>
        {
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void CompareReturnsZeroIfDrawOrdersAreEqual()
        {
            var left = new MockDrawable();
            left.DrawOrder = 5;
            var right = new MockDrawable();
            right.DrawOrder = 5;

            Assert.AreEqual( 0, DecrementingDrawable2DComparer.Instance.Compare( left, right ) );
        }


        [TestMethod]
        public void CompareReturnsPositiveValueIfLeftDrawOrderIsLess()
        {
            var left = new MockDrawable();
            left.DrawOrder = 5;
            var right = new MockDrawable();
            right.DrawOrder = 10;

            Assert.IsTrue( DecrementingDrawable2DComparer.Instance.Compare( left, right ) > 0 );
        }

        [TestMethod]
        public void CompareReturnsNegativeValueIfLeftDrawOrderIsGreater()
        {
            var left = new MockDrawable();
            left.DrawOrder = 15;
            var right = new MockDrawable();
            right.DrawOrder = 10;

            Assert.IsTrue( DecrementingDrawable2DComparer.Instance.Compare( left, right ) < 0 );
        }

        [TestMethod]
        public void SortingSimpleCollectionYieldsExpectedResult()
        {
            // Unsorted DrawOrder Collection: {7, 2, 3, 1}
            // Sorted DrawOrder Collection: {7, 3, 2, 1}
            var element1 = new MockDrawable();
            element1.DrawOrder = 1;
            var element2 = new MockDrawable();
            element2.DrawOrder = 2;
            var element3 = new MockDrawable();
            element3.DrawOrder = 3;
            var element7 = new MockDrawable();
            element7.DrawOrder = 7;

            List<IDrawable2D> elements = new List<IDrawable2D>();
            elements.Add( element7 );
            elements.Add( element2 );
            elements.Add( element3 );
            elements.Add( element1 );

            elements.Sort( DecrementingDrawable2DComparer.Instance );

            Assert.AreEqual( element7, elements[ 0 ] );
            Assert.AreEqual( element3, elements[ 1 ] );
            Assert.AreEqual( element2, elements[ 2 ] );
            Assert.AreEqual( element1, elements[ 3 ] );
        }
    }

    [TestClass]
    public class WhenUsingDrawable2DComparers_IncrementingDrawable2DComparer
    {
        class MockDrawable : Renderable2DBase<MockDrawable>
        {
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void CompareReturnsZeroIfDrawOrdersAreEqual()
        {
            var left = new MockDrawable();
            left.DrawOrder = 5;
            var right = new MockDrawable();
            right.DrawOrder = 5;

            Assert.AreEqual( 0, IncrementingDrawable2DComparer.Instance.Compare( left, right ) );
        }


        [TestMethod]
        public void CompareReturnsNegativeValueIfLeftDrawOrderIsLess()
        {
            var left = new MockDrawable();
            left.DrawOrder = 5;
            var right = new MockDrawable();
            right.DrawOrder = 10;

            Assert.IsTrue( IncrementingDrawable2DComparer.Instance.Compare( left, right ) < 0 );
        }

        [TestMethod]
        public void CompareReturnsPositiveValueIfLeftDrawOrderIsGreater()
        {
            var left = new MockDrawable();
            left.DrawOrder = 15;
            var right = new MockDrawable();
            right.DrawOrder = 10;

            Assert.IsTrue( IncrementingDrawable2DComparer.Instance.Compare( left, right ) > 0 );
        }

        [TestMethod]
        public void SortingSimpleCollectionYieldsExpectedResult()
        {
            // Unsorted DrawOrder Collection: {7, 2, 3, 1}
            // Sorted DrawOrder Collection: {1, 2, 3, 7}
            var element1 = new MockDrawable();
            element1.DrawOrder = 1;
            var element2 = new MockDrawable();
            element2.DrawOrder = 2;
            var element3 = new MockDrawable();
            element3.DrawOrder = 3;
            var element7 = new MockDrawable();
            element7.DrawOrder = 7;

            List<IDrawable2D> elements = new List<IDrawable2D>();
            elements.Add( element7 );
            elements.Add( element2 );
            elements.Add( element3 );
            elements.Add( element1 );

            elements.Sort( IncrementingDrawable2DComparer.Instance );

            Assert.AreEqual( element1, elements[ 0 ] );
            Assert.AreEqual( element2, elements[ 1 ] );
            Assert.AreEqual( element3, elements[ 2 ] );
            Assert.AreEqual( element7, elements[ 3 ] );
        }
    }
}
