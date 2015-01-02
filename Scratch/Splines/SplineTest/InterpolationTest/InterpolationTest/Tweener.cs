using System;
using Microsoft.Xna.Framework;

namespace InterpolationTest
{
    public static class Tweener
    {
        #region Private Methods

        private static float normalizeValue( float value, float period, bool loop )
        {
            // Normalize the value to [0.0, 1.0)
            value /= period; // Divide by the period
            if( loop )
            {
                // Remove the integer part and add 1.0 to the value too if it's negative
                //   to bring it back to between 0.0 and 1.0
                value -= (float)Math.Truncate( value ) + ( value < 0.0f ? 1.0f : 0.0f );
            }
            else
            {
                // Clamp the value to [0.0, 1.0]
                value = Math.Max( 0.0f, Math.Min( 1.0f, value ) );
            }
            return value;
        }

        private static float wrapNormalizeValue( float value, float period, bool loop )
        {
            // Normalize the value to [0.0, 1.0)
            value /= period; // Divide by the period
            if( loop )
            {
                bool reversed = ( (int)Math.Floor( value ) % 2 != 0 );
                // Remove the integer part and add 1.0 to the value too if it's negative
                //   to bring it back to between 0.0 and 1.0
                value -= (float)Math.Truncate( value ) + ( value < 0.0f ? 1.0f : 0.0f );
                if( reversed )
                {
                    value = 1.0f - value;
                }
            }
            else
            {
                // Clamp the value to [0.0, 1.0]
                value = Math.Max( 0.0f, Math.Min( 1.0f, value ) );
            }
            return value;
        }

        public static float powerLerp( float start, float end, float value, float power )
        {
            return MathHelper.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        public static Vector2 powerLerp( Vector2 start, Vector2 end, float value, float power )
        {
            return Vector2.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        public static Vector3 powerLerp( Vector3 start, Vector3 end, float value, float power )
        {
            return Vector3.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        public static Vector4 powerLerp( Vector4 start, Vector4 end, float value, float power )
        {
            return Vector4.Lerp( start, end, (float)Math.Pow( value, power ) );
        }

        public static float inversePowerLerp( float start, float end, float value, float power )
        {
            return MathHelper.Lerp( start, end, 1.0f - (float)Math.Pow( 1.0f - value, power ) ); // the inverse effect we want
            //float amount = (float)Math.Pow( value, 1.0f / power ); // the "real" inverse
            //return MathHelper.Lerp( start, end, amount );
            //float amount = (float)Math.Pow( value - 1.0f, power ); // integer-version of the inverse
            //return MathHelper.Lerp( start, end, ( amount < 0.0f ? amount + 1.0f : 1.0f - amount ) );
        }

        public static Vector2 inversePowerLerp( Vector2 start, Vector2 end, float value, float power )
        {
            return Vector2.Lerp( start, end, 1.0f - (float)Math.Pow( 1.0f - value, power ) );
        }

        public static Vector3 inversePowerLerp( Vector3 start, Vector3 end, float value, float power )
        {
            return Vector3.Lerp( start, end, 1.0f - (float)Math.Pow( 1.0f - value, power ) );
        }

        public static Vector4 inversePowerLerp( Vector4 start, Vector4 end, float value, float power )
        {
            return Vector4.Lerp( start, end, 1.0f - (float)Math.Pow( 1.0f - value, power ) );
        }

        #endregion

        #region Step Tween

        public static float Step( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            if( normalizeValue( value, period, loop ) < 0.5f )
            {
                return start;
            }
            else
            {
                return end;
            }
        }

        public static Vector2 Step( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            if( normalizeValue( value, period, loop ) < 0.5f )
            {
                return start;
            }
            else
            {
                return end;
            }
        }

        public static Vector3 Step( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            if( normalizeValue( value, period, loop ) < 0.5f )
            {
                return start;
            }
            else
            {
                return end;
            }
        }

        public static Vector4 Step( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            if( normalizeValue( value, period, loop ) < 0.5f )
            {
                return start;
            }
            else
            {
                return end;
            }
        }

        #endregion

        #region Linear Tween

        public static float Linear( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return MathHelper.Lerp( start, end, wrapNormalizeValue( value, period, loop ) );
        }

        public static Vector2 Linear( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector2.Lerp( start, end, wrapNormalizeValue( value, period, loop ) );
        }

        public static Vector3 Linear( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector3.Lerp( start, end, wrapNormalizeValue( value, period, loop ) );
        }

        public static Vector4 Linear( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector4.Lerp( start, end, wrapNormalizeValue( value, period, loop ) );
        }

        #endregion

        #region Sine Tween

        public static float Sine( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return MathHelper.Lerp( start, end, (float)( Math.Sin( MathHelper.Pi * value / period - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static Vector2 Sine( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector2.Lerp( start, end, (float)( Math.Sin( MathHelper.Pi * value / period - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static Vector3 Sine( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector3.Lerp( start, end, (float)( Math.Sin( MathHelper.Pi * value / period - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static Vector4 Sine( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector4.Lerp( start, end, (float)( Math.Sin( MathHelper.Pi * value / period - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static float SineIn( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return MathHelper.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 - MathHelper.PiOver2 ) + 1.0f );
        }

        public static Vector2 SineIn( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector2.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 - MathHelper.PiOver2 ) + 1.0f );
        }

        public static Vector3 SineIn( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector3.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 - MathHelper.PiOver2 ) + 1.0f );
        }

        public static Vector4 SineIn( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector4.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 - MathHelper.PiOver2 ) + 1.0f );
        }

        public static float SineOut( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return MathHelper.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 ) );
        }

        public static Vector2 SineOut( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector2.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 ) );
        }

        public static Vector3 SineOut( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector3.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 ) );
        }

        public static Vector4 SineOut( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector4.Lerp( start, end, (float)Math.Sin( normalizeValue( value, period, loop ) * MathHelper.PiOver2 ) );
        }

        public static float SineInOut( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return MathHelper.Lerp( start, end, (float)( Math.Sin( normalizeValue( value, period, loop ) * MathHelper.Pi - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static Vector2 SineInOut( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector2.Lerp( start, end, (float)( Math.Sin( normalizeValue( value, period, loop ) * MathHelper.Pi - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static Vector3 SineInOut( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector3.Lerp( start, end, (float)( Math.Sin( normalizeValue( value, period, loop ) * MathHelper.Pi - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        public static Vector4 SineInOut( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return Vector4.Lerp( start, end, (float)( Math.Sin( normalizeValue( value, period, loop ) * MathHelper.Pi - MathHelper.PiOver2 ) + 1.0f ) / 2.0f );
        }

        #endregion

        #region Quadratic Tween

        public static float Quadratic( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return QuadraticInOut( start, end, value, period, loop );
        }

        public static Vector2 Quadratic( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return QuadraticInOut( start, end, value, period, loop );
        }

        public static Vector3 Quadratic( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return QuadraticInOut( start, end, value, period, loop );
        }

        public static Vector4 Quadratic( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return QuadraticInOut( start, end, value, period, loop );
        }

        public static float QuadraticIn( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static Vector2 QuadraticIn( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static Vector3 QuadraticIn( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static Vector4 QuadraticIn( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static float QuadraticOut( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static Vector2 QuadraticOut( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static Vector3 QuadraticOut( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static Vector4 QuadraticOut( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 2 );
        }

        public static float QuadraticInOut( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, MathHelper.Lerp( start, end, 0.5f ), value, 2 ) : inversePowerLerp( MathHelper.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 2 ) );
        }

        public static Vector2 QuadraticInOut( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector2.Lerp( start, end, 0.5f ), value, 2 ) : inversePowerLerp( Vector2.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 2 ) );
        }

        public static Vector3 QuadraticInOut( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector3.Lerp( start, end, 0.5f ), value, 2 ) : inversePowerLerp( Vector3.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 2 ) );
        }

        public static Vector4 QuadraticInOut( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector4.Lerp( start, end, 0.5f ), value, 2 ) : inversePowerLerp( Vector4.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 2 ) );
        }

        #endregion

        #region Cubic Tween

        public static float Cubic( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return CubicInOut( start, end, value, period, loop );
        }

        public static Vector2 Cubic( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return CubicInOut( start, end, value, period, loop );
        }

        public static Vector3 Cubic( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return CubicInOut( start, end, value, period, loop );
        }

        public static Vector4 Cubic( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return CubicInOut( start, end, value, period, loop );
        }

        public static float CubicIn( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static Vector2 CubicIn( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static Vector3 CubicIn( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static Vector4 CubicIn( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static float CubicOut( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static Vector2 CubicOut( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static Vector3 CubicOut( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static Vector4 CubicOut( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), 3 );
        }

        public static float CubicInOut( float start, float end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, MathHelper.Lerp( start, end, 0.5f ), value, 3 ) : inversePowerLerp( MathHelper.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 3 ) );
        }

        public static Vector2 CubicInOut( Vector2 start, Vector2 end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector2.Lerp( start, end, 0.5f ), value, 3 ) : inversePowerLerp( Vector2.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 3 ) );
        }

        public static Vector3 CubicInOut( Vector3 start, Vector3 end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector3.Lerp( start, end, 0.5f ), value, 3 ) : inversePowerLerp( Vector3.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 3 ) );
        }

        public static Vector4 CubicInOut( Vector4 start, Vector4 end, float value, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector4.Lerp( start, end, 0.5f ), value, 3 ) : inversePowerLerp( Vector4.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), 3 ) );
        }

        #endregion

        #region Exponential Tween

        public static float Exponential( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, power, period, loop );
        }

        public static Vector2 Exponential( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, power, period, loop );
        }

        public static Vector3 Exponential( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, power, period, loop );
        }

        public static Vector4 Exponential( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, power, period, loop );
        }

        public static float ExponentialIn( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static Vector2 ExponentialIn( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static Vector3 ExponentialIn( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static Vector4 ExponentialIn( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return powerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static float ExponentialOut( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static Vector2 ExponentialOut( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static Vector3 ExponentialOut( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static Vector4 ExponentialOut( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return inversePowerLerp( start, end, normalizeValue( value, period, loop ), power );
        }

        public static float ExponentialInOut( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, MathHelper.Lerp( start, end, 0.5f ), value, power ) : inversePowerLerp( MathHelper.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), power ) );
        }

        public static Vector2 ExponentialInOut( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector2.Lerp( start, end, 0.5f ), value, power ) : inversePowerLerp( Vector2.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), power ) );
        }

        public static Vector3 ExponentialInOut( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector3.Lerp( start, end, 0.5f ), value, power ) : inversePowerLerp( Vector3.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), power ) );
        }

        public static Vector4 ExponentialInOut( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            return ( value < 1.0f ? powerLerp( start, Vector4.Lerp( start, end, 0.5f ), value, power ) : inversePowerLerp( Vector4.Lerp( start, end, 0.5f ), end, ( value - 1.0f ), power ) );
        }

        #endregion

        #region Logarithmic Tween

        public static float Logarithmic( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return LogarithmicInOut( start, end, value, power, period, loop );
        }

        public static Vector2 Logarithmic( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return LogarithmicInOut( start, end, value, power, period, loop );
        }

        public static Vector3 Logarithmic( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return LogarithmicInOut( start, end, value, power, period, loop );
        }

        public static Vector4 Logarithmic( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return LogarithmicInOut( start, end, value, power, period, loop );
        }

        public static float LogarithmicIn( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialIn( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector2 LogarithmicIn( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialIn( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector3 LogarithmicIn( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialIn( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector4 LogarithmicIn( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialIn( start, end, value, 1.0f / power, period, loop );
        }

        public static float LogarithmicOut( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialOut( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector2 LogarithmicOut( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialOut( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector3 LogarithmicOut( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialOut( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector4 LogarithmicOut( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialOut( start, end, value, 1.0f / power, period, loop );
        }

        public static float LogarithmicInOut( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector2 LogarithmicInOut( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector3 LogarithmicInOut( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, 1.0f / power, period, loop );
        }

        public static Vector4 LogarithmicInOut( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return ExponentialInOut( start, end, value, 1.0f / power, period, loop );
        }

        #endregion

        #region Circular Tween

        public static float Circular( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return CircularInOut( start, end, value, power, period, loop );
        }

        public static Vector2 Circular( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return CircularInOut( start, end, value, power, period, loop );
        }

        public static Vector3 Circular( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return CircularInOut( start, end, value, power, period, loop );
        }

        public static Vector4 Circular( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            return CircularInOut( start, end, value, power, period, loop );
        }

        public static float CircularIn( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = normalizeValue( value, period, loop );
            return MathHelper.Lerp( start, end, 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static Vector2 CircularIn( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = normalizeValue( value, period, loop );
            return Vector2.Lerp( start, end, 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static Vector3 CircularIn( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = normalizeValue( value, period, loop );
            return Vector3.Lerp( start, end, 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static Vector4 CircularIn( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = normalizeValue( value, period, loop );
            return Vector4.Lerp( start, end, 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static float CircularOut( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = 1.0f - normalizeValue( value, period, loop );
            return MathHelper.Lerp( start, end, (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static Vector2 CircularOut( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = 1.0f - normalizeValue( value, period, loop );
            return Vector2.Lerp( start, end, (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static Vector3 CircularOut( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = 1.0f - normalizeValue( value, period, loop );
            return Vector3.Lerp( start, end, (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static Vector4 CircularOut( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = 1.0f - normalizeValue( value, period, loop );
            return Vector4.Lerp( start, end, (float)Math.Sqrt( 1.0f - value * value ) );
        }

        public static float CircularInOut( float start, float end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            if( value < 1.0f )
            {
                return MathHelper.Lerp( start, MathHelper.Lerp( start, end, 0.5f ), 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
            }
            else
            {
                value = 2.0f - value;// = ( 1.0f - ( value - 1.0f ) );
                return MathHelper.Lerp( MathHelper.Lerp( start, end, 0.5f ), end, (float)Math.Sqrt( 1.0f - value * value ) );
            }
        }

        public static Vector2 CircularInOut( Vector2 start, Vector2 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            if( value < 1.0f )
            {
                return Vector2.Lerp( start, Vector2.Lerp( start, end, 0.5f ), 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
            }
            else
            {
                value = 2.0f - value;// = ( 1.0f - ( value - 1.0f ) );
                return Vector2.Lerp( Vector2.Lerp( start, end, 0.5f ), end, (float)Math.Sqrt( 1.0f - value * value ) );
            }
        }

        public static Vector3 CircularInOut( Vector3 start, Vector3 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            if( value < 1.0f )
            {
                return Vector3.Lerp( start, Vector3.Lerp( start, end, 0.5f ), 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
            }
            else
            {
                value = 2.0f - value;// = ( 1.0f - ( value - 1.0f ) );
                return Vector3.Lerp( Vector3.Lerp( start, end, 0.5f ), end, (float)Math.Sqrt( 1.0f - value * value ) );
            }
        }

        public static Vector4 CircularInOut( Vector4 start, Vector4 end, float value, float power = MathHelper.E, float period = 1.0f, bool loop = false )
        {
            value = wrapNormalizeValue( value, period, loop ) * 2.0f;
            if( value < 1.0f )
            {
                return Vector4.Lerp( start, Vector4.Lerp( start, end, 0.5f ), 1.0f - (float)Math.Sqrt( 1.0f - value * value ) );
            }
            else
            {
                value = 2.0f - value;// = ( 1.0f - ( value - 1.0f ) );
                return Vector4.Lerp( Vector4.Lerp( start, end, 0.5f ), end, (float)Math.Sqrt( 1.0f - value * value ) );
            }
        }

        #endregion
    }
}
