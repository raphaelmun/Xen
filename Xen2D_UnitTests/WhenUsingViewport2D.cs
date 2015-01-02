using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingViewport2D
    {
        [TestMethod]
        public void MoveByChangesAnchor()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.MoveBy( new Vector2( 5, 10 ) );
            Assert.AreEqual( new Vector2( 5, 10 ), viewport.Anchor );
            viewport.MoveBy( new Vector2( 2, 4 ) );
            Assert.AreEqual( new Vector2( 7, 14 ), viewport.Anchor );
        }

        [TestMethod]
        public void MoveToSetsAnchor()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.MoveTo( new Vector2( 5, 10 ) );
            Assert.AreEqual( new Vector2( 5, 10 ), viewport.Anchor );

            viewport.MoveTo( new Vector2( 25, 11 ) );
            Assert.AreEqual( new Vector2( 25, 11 ), viewport.Anchor );
        }

        [TestMethod]
        public void MoveHorizontalWithZeroAngleWorks()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.Anchor = new Vector2( 100, 100 );
            viewport.MoveHorizontal( 10 );
            Assert.AreEqual( new Vector2( 110, 100 ), viewport.Anchor );
        }

        [TestMethod]
        public void MoveHorizontalWithAngleWorks()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.Anchor = new Vector2( 100, 100 );
            viewport.Angle = MathHelper.PiOver2;
            viewport.MoveHorizontal( 10 );
            Assert.AreEqual( new Vector2( 100, 110 ), viewport.Anchor );
        }

        [TestMethod]
        public void MoveVerticalWithZeroAngleWorks()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.Anchor = new Vector2( 100, 100 );
            viewport.MoveVertical( 10 );
            Assert.AreEqual( new Vector2( 100, 110 ), viewport.Anchor );
        }

        [TestMethod]
        public void MoveVerticalWithAngleWorks()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.Anchor = new Vector2( 100, 100 );
            viewport.Angle = MathHelper.PiOver2;
            viewport.MoveVertical( 10 );
            Assert.AreEqual( new Vector2( 90, 100 ), viewport.Anchor );
        }

        [TestMethod]
        public void ResetWithScreenWidthSetsOrigin()
        {
            IViewport2D viewport = new Viewport2D();
            viewport.ResetWithScreenSize( 100, 100 );
            Assert.AreEqual( new Vector2( 50, 50 ), viewport.Origin );
        }
    }
}
