using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingSpriteDisplayAttributes
    {
        class MockDisplayAttributes : I2DDisplayModifiers
        {
            public float LayerDepth { get; set; }
            public Color ModulationColor { get; set; }
            public Color ModulationColorWithOpacity { get { return ModulationColor * OpacityFinal; } }
            public float Opacity { get; set; }
            public float OpacityModifier { get; set; }
            public float OpacityFinal { get { return Opacity * OpacityModifier; } }
            public SpriteEffects SpriteEffects { get; set; }
        }

        [TestMethod]
        public void ResetRestoresEqualityWithDefaultInstance()
        {
            MockDisplayAttributes sda = new MockDisplayAttributes();
            sda.ModulationColor = Color.Red;
            sda.LayerDepth = 0.5f;
            sda.Opacity = 0.9f;
            sda.OpacityModifier = 0.2f;
            sda.SpriteEffects = SpriteEffects.FlipHorizontally;

            Assert.IsFalse( SpriteDisplayAttributeDefaults.HasDefaultDisplayAttributes( sda ) );
            SpriteDisplayAttributeDefaults.SetDefaults( sda );
            Assert.IsTrue( SpriteDisplayAttributeDefaults.HasDefaultDisplayAttributes( sda ) );
        }

        [TestMethod]
        public void DefaultOpacityModifierEqualsOne()
        {
            I2DDisplayModifiers sda = SpriteDisplayAttributeDefaults.Default;
            Assert.AreEqual( 1.0f, sda.OpacityModifier );
        }
    }
}
