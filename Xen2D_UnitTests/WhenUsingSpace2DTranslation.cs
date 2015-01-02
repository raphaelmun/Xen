using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Xen2D;

namespace Xen2D_UnitTests
{
    [TestClass]
    public class WhenUsingSpace2DTranslation
    {
        [TestMethod]
        public void DefaultConstructedInstanceHasZeroAsBothMatrices()
        {
            Space2DTranslation st = new Space2DTranslation();
            Assert.AreEqual( MatrixUtility.Zero, st.TranslateTo );
            Assert.AreEqual( MatrixUtility.Zero, st.TranslateFrom );
        }

        [TestMethod]
        public void DefaultFactoryInstanceHasIdentityAsBothMatrices()
        {
            Space2DTranslation st = Space2DTranslation.Create();
            Assert.AreEqual( Matrix.Identity, st.TranslateTo );
            Assert.AreEqual( Matrix.Identity, st.TranslateFrom );
        }

        [TestMethod]
        public void SettingOneTransformCausesOtherToEqualInverse()
        {
            Space2DTranslation st = Space2DTranslation.Create();
            Matrix matrix = Matrix.CreateTranslation( 10, -10, 0 );
            st.TranslateTo = matrix;

            Assert.AreEqual( st.TranslateFrom, Matrix.Invert( matrix ) );
        }

        [TestMethod]
        public void SimpleTranslationWorks()
        {
            Space2DTranslation st = Space2DTranslation.Create();

            st.TranslateTo = Matrix.CreateTranslation( -10, 10, 0 );
            Assert.AreEqual( new Vector2( -10, 10 ), st.TranslateVectorFromAbsoluteToThisSpace( Vector2.Zero ) );
            Assert.AreEqual( Vector2.Zero, st.TranslateVectorFromAbsoluteToThisSpace( new Vector2( 10, -10 ) ) );
        }

        [TestMethod]
        public void TranslationRoundTripYieldsOriginalVector()
        {
            Space2DTranslation st = Space2DTranslation.Create();

            st.TranslateTo = Matrix.CreateTranslation( -10, 10, 0 );
            Vector2 vec = new Vector2( 123, 234 );
            Assert.AreEqual( vec, st.TranslateVectorFromThisSpaceToAbsolute( st.TranslateVectorFromAbsoluteToThisSpace( vec ) ) );
        }
    }
}
