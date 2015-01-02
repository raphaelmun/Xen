using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;
using XenGameBase;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenUsingElementDecorator
    {
        class MockRenderable : Renderable2DBase<MockRenderable>
        {
            protected override void DrawInternal( Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
            }
        }

        [TestMethod]
        public void NewlyAcquiredInstanceHasDefaultValues()
        {
            var dec = ElementDecorator.Acquire();
            Assert.AreEqual( Vector2.Zero, dec.AnchorToParent );
            Assert.AreEqual( Vector2.Zero, dec.Offset );
        }

        [TestMethod]
        public void ResetRestoresDirectStateToDefaults()
        {
            var dec = ElementDecorator.Acquire();
            dec.AnchorToParent = 100 * Vector2.One;
            dec.Offset = 100 * Vector2.One;

            dec.Reset();
            Assert.AreEqual( Vector2.Zero, dec.AnchorToParent );
            Assert.AreEqual( Vector2.Zero, dec.Offset );
        }

        [TestMethod]
        public void ResettingCausesComponentToBeSet()
        {
            var dec = ElementDecorator.Acquire();
            var comp = MockRenderable.Acquire();

            Assert.AreEqual( null, dec.Component );
            dec.Reset( comp );
            Assert.AreEqual( comp, dec.Component );
        }

        [TestMethod]
        public void ReleasingDecoratorCausesComponentToRelease()
        {
            var dec = ElementDecorator.Acquire();
            var comp = MockRenderable.Acquire();

            dec.Reset( comp );

            bool onReleasedCalled = false;
            comp.OnReleased.Add( obj =>
                {
                    onReleasedCalled = true;
                } );

            Assert.IsFalse( onReleasedCalled );
            Assert.IsTrue( comp.IsAcquired );
            dec.Release();
            Assert.IsTrue( onReleasedCalled );
            Assert.IsFalse( comp.IsAcquired );
        }

        [TestMethod]
        public void ReleasingComponentCausesDecoratorToRelease()
        {
            var dec = ElementDecorator.Acquire();
            var comp = MockRenderable.Acquire();

            dec.Reset( comp );

            bool onReleasedCalled = false;
            dec.OnReleased.Add( obj =>
            {
                onReleasedCalled = true;
            } );

            Assert.IsFalse( onReleasedCalled );
            Assert.IsTrue( dec.IsAcquired );
            comp.Release();
            Assert.IsTrue( onReleasedCalled );
            Assert.IsFalse( dec.IsAcquired );
        }

        [TestMethod]
        public void ResettingDecoratorCausesOldComponentToRelease()
        {
            var dec = ElementDecorator.Acquire();
            var comp = MockRenderable.Acquire();

            dec.Reset( comp );

            Assert.IsTrue( comp.IsAcquired );
            //By reseting the decorating to a new component the relationship with the old one is lost. 
            dec.Reset( MockRenderable.Acquire() );
            Assert.IsFalse( comp.IsAcquired );
        }
    }
}
