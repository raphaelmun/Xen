using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InterpolationTest
{
    public static class Interpolator
    {
        #region Linear Interpolators

        public static float Linear( float start, float end, float value )
        {
            return MathHelper.Lerp( start, end, value );
        }

        public static Vector2 Linear( Vector2 start, Vector2 end, float value )
        {
            return Vector2.Lerp( start, end, value );
        }

        public static Vector3 Linear( Vector3 start, Vector3 end, float value )
        {
            return Vector3.Lerp( start, end, value );
        }

        public static Vector4 Linear( Vector4 start, Vector4 end, float value )
        {
            return Vector4.Lerp( start, end, value );
        }

        #endregion

        #region Quadratic Interpolators

        public static float Quadratic( float start, float end, float value )
        {
            return MathHelper.Lerp( start, end, value * value );
        }

        public static Vector2 Quadratic( Vector2 start, Vector2 end, float value )
        {
            return Vector2.Lerp( start, end, value * value );
        }

        public static Vector3 Quadratic( Vector3 start, Vector3 end, float value )
        {
            return Vector3.Lerp( start, end, value * value );
        }

        public static Vector4 Quadratic( Vector4 start, Vector4 end, float value )
        {
            return Vector4.Lerp( start, end, value * value );
        }

        #endregion

        #region Cubic Interpolators

        public static float Cubic( float start, float end, float value )
        {
            return MathHelper.SmoothStep( start, end, value );
        }

        public static Vector2 Cubic( Vector2 start, Vector2 end, float value )
        {
            return Vector2.SmoothStep( start, end, value );
        }

        public static Vector3 Cubic( Vector3 start, Vector3 end, float value )
        {
            return Vector3.SmoothStep( start, end, value );
        }

        public static Vector4 Cubic( Vector4 start, Vector4 end, float value )
        {
            return Vector4.SmoothStep( start, end, value );
        }

        #endregion
        
        #region Exponential Interpolators

        public static float Exponential( float start, float end, float value, float power = MathHelper.E )
        {
            return MathHelper.Lerp( start, end, (float)Math.Pow(value, power) );
        }

        public static Vector2 Exponential( Vector2 start, Vector2 end, float value, float power = MathHelper.E )
        {
            return Vector2.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        public static Vector3 Exponential( Vector3 start, Vector3 end, float value, float power = MathHelper.E )
        {
            return Vector3.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        public static Vector4 Exponential( Vector4 start, Vector4 end, float value, float power = MathHelper.E )
        {
            return Vector4.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        #endregion

        #region Logarithmic Interpolators

        public static float Logarithmic( float start, float end, float value, float power = MathHelper.E )
        {
            return MathHelper.Lerp( start, end, (float)Math.Pow( value, 1.0f / power ) );
        }

        public static Vector2 Logarithmic( Vector2 start, Vector2 end, float value, float power = MathHelper.E )
        {
            return Vector2.Lerp( start, end, (float)Math.Pow( value, 1.0f / power ) );
        }

        public static Vector3 Logarithmic( Vector3 start, Vector3 end, float value, float power = MathHelper.E )
        {
            return Vector3.Lerp( start, end, (float)Math.Pow( value, 1.0f / power ) );
        }

        public static Vector4 Logarithmic( Vector4 start, Vector4 end, float value, float power = MathHelper.E )
        {
            return Vector4.Lerp( start, end, (float)Math.Pow( value, 1.0f / power ) );
        }

        #endregion

        #region Sine Interpolators

        public static float Sine( float start, float end, float value )
        {
            return MathHelper.Lerp( start, end, (float)Math.Sin( value * MathHelper.PiOver2 ) );
        }

        public static Vector2 Sine( Vector2 start, Vector2 end, float value )
        {
            return Vector2.Lerp( start, end, (float)Math.Sin( value * MathHelper.PiOver2 ) );
        }

        public static Vector3 Sine( Vector3 start, Vector3 end, float value )
        {
            return Vector3.Lerp( start, end, (float)Math.Sin( value * MathHelper.PiOver2 ) );
        }

        public static Vector4 Sine( Vector4 start, Vector4 end, float value )
        {
            return Vector4.Lerp( start, end, (float)Math.Sin( value * MathHelper.PiOver2 ) );
        }

        #endregion
    }
}
