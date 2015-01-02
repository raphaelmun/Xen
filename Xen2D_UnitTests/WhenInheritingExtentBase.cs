using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenInheritingExtentBase
    {
        class MockExtent : ExtentBase<MockExtent, ShapePolygon>
        {
            public override void RecalculateBounds() { }

            public override Vector2 FindClosestPoint( Vector2 point )
            {
                throw new NotImplementedException();
            }

            public override bool Contains( Vector2 point )
            {
                throw new NotImplementedException();
            }

            public override bool Intersects( ICompositeExtent other )
            {
                throw new NotImplementedException();
            }

            public override bool Intersects( IPolygonExtent other )
            {
                throw new NotImplementedException();
            }

            public override bool Intersects( ICircularExtent other )
            {
                throw new NotImplementedException();
            }

            public override bool Intersects( IPolygon2D other )
            {
                throw new NotImplementedException();
            }

            public override bool Intersects( ICircle2D other )
            {
                throw new NotImplementedException();
            }

            protected override bool IntersectsImpl( CollisionMode thisCollisionMode, IExtent otherExtent, CollisionMode otherCollisionMode )
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void DerivedInstanceHasExpectedDefaultValues()
        {
            MockExtent mock = new MockExtent();
            Assert.AreEqual( mock.Anchor, Vector2.Zero );
            Assert.AreEqual( mock.Angle, 0 );
            Assert.AreEqual( mock.Origin, Vector2.Zero );
            Assert.AreEqual( mock.Scale, new Vector2( 1, 1 ) );
        }

        [TestMethod]
        public void ResetRestoresDefaultValues()
        {
            MockExtent mock = new MockExtent();
            mock.Anchor = new Vector2( 100, 100 );
            mock.Angle = 5.0f;
            mock.Origin = new Vector2( 241, 124 );
            mock.Scale = new Vector2( 5, 10 );

            mock.Reset();

            Assert.AreEqual( mock.Anchor, Vector2.Zero );
            Assert.AreEqual( mock.Angle, 0 );
            Assert.AreEqual( mock.Origin, Vector2.Zero );
            Assert.AreEqual( mock.Scale, new Vector2( 1, 1 ) );
        }

        [TestMethod]
        public void ResetRestoresDefaultTransform()
        {
            MockExtent mock = new MockExtent();
            mock.Anchor = new Vector2( 100, 100 );
            mock.Angle = 5.0f;
            mock.Origin = new Vector2( 241, 124 );
            mock.Scale = new Vector2( 5, 10 );

            Assert.AreNotEqual( mock.Transform.TranslateFrom, Matrix.Identity );
            Assert.AreNotEqual( mock.Transform.TranslateTo, Matrix.Identity );

            mock.Reset();

            Assert.AreEqual( mock.Transform.TranslateFrom, Matrix.Identity );
            Assert.AreEqual( mock.Transform.TranslateTo, Matrix.Identity );
        }

        [TestMethod]
        public void SettingOriginUpdatesAnchor()
        {
            MockExtent extent = new MockExtent();

            extent.Origin = new Vector2( 50, 50 );
            Assert.AreEqual( new Vector2( 50, 50 ), extent.Anchor );
        }

        [TestMethod]
        public void MovingAnchorDoesNotAffectOrigin()
        {
            MockExtent extent = new MockExtent();

            extent.Anchor = new Vector2( 200, 200 );
            Assert.AreEqual( Vector2.Zero, extent.Origin );
        }

        [TestMethod]
        public void SimpleReAnchorWorksForZeroAnchorAndOrigin()
        {
            MockExtent extent = new MockExtent();

            extent.ReAnchor( new Vector2( 50, 50 ) );
            Assert.AreEqual( new Vector2( 50, 50 ), extent.Anchor );
            Assert.AreEqual( new Vector2( 50, 50 ), extent.Origin );
        }

        [TestMethod]
        public void SimpleReAnchorWorksForNonZeroAnchorAndOrigin()
        {
            MockExtent extent = new MockExtent();

            extent.Origin = new Vector2( 10, 10 );
            extent.Anchor = new Vector2( 20, 20 );
            Assert.AreEqual( new Vector2( 20, 20 ), extent.Anchor );
            extent.ReAnchor( new Vector2( 50, 50 ) );
            Assert.AreEqual( new Vector2( 40, 40 ), extent.Origin );
        }

        [TestMethod]
        public void ReAnchorWorksForScaledExtent()
        {
            MockExtent extent = new MockExtent();

            extent.Scale = new Vector2( 2, 2 );
            extent.ReAnchor( new Vector2( 16, 16 ) );
            Assert.AreEqual( new Vector2( 8, 8 ), extent.Origin );
        }

        [TestMethod]
        public void ReAnchorWorksForAngledExtent()
        {
            MockExtent extent = new MockExtent();

            extent.Angle = MathHelper.PiOver2;
            extent.ReAnchor( Vector2.UnitY );
            Assert.AreEqual( Vector2.UnitX, XenMath.RoundVector( extent.Origin ) );
        }

        [TestMethod]
        public void TranslateToWithParentWorks()
        {
            MockExtent parent = new MockExtent();
            parent.Anchor = new Vector2( 100, 100 );
            parent.Angle = MathHelper.PiOver2;
            parent.Scale = new Vector2( 2, 2 );

            MockExtent child = new MockExtent();
            child.Anchor = new Vector2( 200, 200 );
            child.Angle = MathHelper.PiOver4;
            child.Scale = new Vector2( 3, 3 );
            child.ParentSpace = parent;

            Assert.AreEqual( child.Transform.TranslateTo * parent.Transform.TranslateTo, child.TranslateTo );
        }

        [TestMethod]
        public void TranslateFromWithParentWorks()
        {
            MockExtent parent = new MockExtent();
            parent.Anchor = new Vector2( 100, 100 );
            parent.Angle = MathHelper.PiOver2;
            parent.Scale = new Vector2( 2, 2 );

            MockExtent child = new MockExtent();
            child.Anchor = new Vector2( 200, 200 );
            child.Angle = MathHelper.PiOver4;
            child.Scale = new Vector2( 3, 3 );
            child.ParentSpace = parent;

            Assert.AreEqual( child.Transform.TranslateFrom * parent.Transform.TranslateFrom, child.TranslateFrom );
        }

        [TestMethod]
        public void ModifyingAngleRaisesOnChanged()
        {
            MockExtent extent = new MockExtent();
            bool onChangedCalled = false;

            extent.OnChanged.Add( () =>
                {
                    onChangedCalled = true;
                } );

            extent.Angle = extent.Angle;
            Assert.IsFalse( onChangedCalled );

            extent.Angle += 0.1f;
            Assert.IsTrue( onChangedCalled );
        }

        [TestMethod]
        public void ModifyingScaleRaisesOnChanged()
        {
            MockExtent extent = new MockExtent();
            bool onChangedCalled = false;

            extent.OnChanged.Add( () =>
            {
                onChangedCalled = true;
            } );

            extent.Scale = extent.Scale;
            Assert.IsFalse( onChangedCalled );

            extent.Scale *= 0.1f;
            Assert.IsTrue( onChangedCalled );
        }

        [TestMethod]
        public void ModifyingAnchorRaisesOnChanged()
        {
            MockExtent extent = new MockExtent();
            bool onChangedCalled = false;

            extent.OnChanged.Add( () =>
            {
                onChangedCalled = true;
            } );

            extent.Anchor = extent.Anchor;
            Assert.IsFalse( onChangedCalled );

            extent.Anchor += new Vector2( 1, 2 );
            Assert.IsTrue( onChangedCalled );
        }
    }
}
