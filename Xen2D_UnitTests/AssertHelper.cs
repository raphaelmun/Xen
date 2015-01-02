using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xen2D_UnitTests
{
    internal static class AssertHelper
    {
        public static void AreApproximatelyEqual( float expected, float actual, int roundDigits = 4 )
        {
            expected = (float)Math.Round( expected, roundDigits );
            actual = (float)Math.Round( actual, roundDigits );
            Assert.AreEqual( expected, actual );
        }
    }
}
