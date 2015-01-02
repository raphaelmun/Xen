using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingVectorRectangle
    {
        [TestMethod]
        public void VectorRectContainsAllCorners()
        {
            VectorRectangle vr = VectorRectangle.Acquire( 10, 20 );

            Assert.AreEqual( 10, vr.Width );
            Assert.AreEqual( 20, vr.Height );
            Assert.IsTrue( vr.Contains( Vector2.Zero ) );
            Assert.IsTrue( vr.Contains( new Vector2( 10, 0 ) ) );
            Assert.IsTrue( vr.Contains( new Vector2( 10, 20 ) ) );
            Assert.IsTrue( vr.Contains( new Vector2( 0, 20 ) ) );
            Assert.IsFalse( vr.Contains( new Vector2( -0.1f, -0.1f ) ) );
            Assert.IsFalse( vr.Contains( new Vector2( 10.1f, 0 ) ) );
            Assert.IsFalse( vr.Contains( new Vector2( 10.1f, 20.1f ) ) );
            Assert.IsFalse( vr.Contains( new Vector2( 0, 20.1f ) ) );
        }
    }
}
