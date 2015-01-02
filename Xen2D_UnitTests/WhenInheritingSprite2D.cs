using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenInheritingSprite2D
    {
        class MockSprite : Sprite2D<MockSprite>
        {
            public override void Render( SpriteBatch spriteBatch )
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void NewInstanceHasDefaultSpriteDisplayAttributes()
        {
            MockSprite sprite = MockSprite.Acquire();
            Assert.IsTrue( sprite.DisplayAttribtes.Equals( SpriteDisplayAttributes.Default ) );
        }

        [TestMethod]
        public void ResetRestoresSpriteDisplayAttributes()
        {
            MockSprite sprite = MockSprite.Acquire();

            sprite.DisplayAttribtes.ModulationColor = Color.Red;

            Assert.IsFalse( sprite.DisplayAttribtes.Equals( SpriteDisplayAttributes.Default ) );
            sprite.Reset();
            Assert.IsTrue( sprite.DisplayAttribtes.Equals( SpriteDisplayAttributes.Default ) );
        }

        [TestMethod]
        public void ResetRestoresTransform()
        {
            MockSprite sprite = MockSprite.Acquire();

            Space2DTranslation transform = new Space2DTranslation();
            transform.TranslateTo = Matrix.CreateTranslation( 1, 1, 1 );
            sprite.Transform = transform;

            sprite.Reset();
            Assert.AreEqual( Matrix.Identity, sprite.Transform.TranslateFrom );
            Assert.AreEqual( Matrix.Identity, sprite.Transform.TranslateTo );
        }
    }
}
