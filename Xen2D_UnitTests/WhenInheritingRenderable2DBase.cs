using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenInheritingRenderable2DBase
    {
        class MockRenderable : Renderable2DBase<MockRenderable>
        {
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
                throw new NotImplementedException();
            }

            public bool OnOpacityChangedCalled = false;
            protected override void OnOpacityChanged()
            {
                OnOpacityChangedCalled = true;            
            }
        }

        [TestMethod]
        public void NewInstanceHasDefaultSpriteDisplayAttributes()
        {
            MockRenderable mock = MockRenderable.Acquire();
            Assert.IsTrue( SpriteDisplayAttributeDefaults.HasDefaultDisplayAttributes( mock ) );
        }

        [TestMethod]
        public void ResetRestoresSpriteDisplayAttributes()
        {
            MockRenderable mock = MockRenderable.Acquire();

            mock.ModulationColor = Color.Red;

            Assert.IsFalse( SpriteDisplayAttributeDefaults.HasDefaultDisplayAttributes( mock ) );
            mock.Reset();
            Assert.IsTrue( SpriteDisplayAttributeDefaults.HasDefaultDisplayAttributes( mock ) );
        }

        [TestMethod]
        public void ResetRestoresTransform()
        {
            MockRenderable mock = MockRenderable.Acquire();

            Space2DTranslation transform = new Space2DTranslation();
            transform.TranslateTo = Matrix.CreateTranslation( 1, 1, 1 );
            //mock.Transform = transform;

            mock.Reset();
            Assert.AreEqual( Matrix.Identity, mock.Transform.TranslateFrom );
            Assert.AreEqual( Matrix.Identity, mock.Transform.TranslateTo );
        }

        [TestMethod]
        public void ResetRestoresDrawOrderToDefault()
        {
            MockRenderable mock = new MockRenderable();
            mock.DrawOrder = 23847;
            mock.Reset();
            Assert.AreEqual( MockRenderable.DefaultDrawOrder, mock.DrawOrder );
        }

        [TestMethod]
        public void OnOpacityChangedGetsCalledWhenOpacityChanges()
        {
            MockRenderable mock = new MockRenderable();
            Assert.IsFalse( mock.OnOpacityChangedCalled );

            mock.Opacity = 0.5f;

            Assert.IsTrue( mock.OnOpacityChangedCalled );
        }
    }
}
