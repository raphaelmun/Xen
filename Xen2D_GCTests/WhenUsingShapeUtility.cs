using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects_UnitTests;

namespace Xen2D_GCTests
{
    [TestClass]
    public class WhenUsingShapeUtility
    {
        [TestMethod]
        public void FindClosestPointOnCircleDoesNotGenerateGarbage()
        {
            Vector2 point = new Vector2( 100, 100 );
            ICircle2D circle = ShapeCircle.Acquire( new Vector2( 30, 30 ), 40 );

            HeapUtility.GetHeapStateBefore();
            ShapeUtility.FindClosestPointOnCircle( point, circle );
            HeapState delta = HeapUtility.GetHeapStateDelta();
            Assert.AreEqual( HeapState.Neutral, delta );
        }
    }
}
