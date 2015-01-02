using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace GarageDemo
{
    public class TweensElement : Element2D<TweensElement>
    {
        StaticSprite _markerIntersect_Blue;
        const float stepInterval = 0.01f;
        Vector2 _pStart, _pEnd;

        public TweensElement()
        {
            _markerIntersect_Blue = StaticSprite.Acquire( Textures.Get( TexId.Marker_Blue ), new Vector2( 4, 4 ) );
            _markerIntersect_Blue.LayerDepth = 0;
            _markerIntersect_Blue.Visible = false;
        }

        public override void Reset()
        {
            base.Reset();
            VisualComponent = _markerIntersect_Blue;
        }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            base.DrawInternal( spriteBatch, transformFromWorldToCamera );
            _pStart = Vector2.UnitY * (float)Globals.GraphicsDevice.Viewport.Height;
            _pEnd = Vector2.UnitX * (float)Globals.GraphicsDevice.Viewport.Width;
            for( float i = 0.0f; i < 1.0f; i += stepInterval )
            {
                float x1 = i, x2 = i + stepInterval;
                Vector2 start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Step( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                Vector2 end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Step( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Blue, start, end, 3 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Linear( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Linear( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Yellow, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Sine( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Sine( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.SineIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.SineIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.SineOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.SineOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.SineInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.SineInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Quadratic( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Quadratic( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.QuadraticIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.QuadraticIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.QuadraticOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.QuadraticOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.QuadraticInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.QuadraticInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Cubic( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Cubic( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CubicIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CubicIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CubicOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CubicOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CubicInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CubicInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Exponential( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Exponential( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.ExponentialIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.ExponentialIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.ExponentialOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.ExponentialOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.ExponentialInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.ExponentialInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Logarithmic( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.25f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Logarithmic( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.25f, true ) );
                spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.LogarithmicIn( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.LogarithmicIn( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.LogarithmicOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.LogarithmicOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.LogarithmicInOut( _pStart.Y, _pEnd.Y, x1, MathHelper.E, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.LogarithmicInOut( _pStart.Y, _pEnd.Y, x2, MathHelper.E, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Purple, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Circular( _pStart.Y, _pEnd.Y, x1, 0.25f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.Circular( _pStart.Y, _pEnd.Y, x2, 0.25f, true ) );
                spriteBatch.DrawLine( Color.Pink, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CircularIn( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CircularIn( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Green, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CircularOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CircularOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Red, start, end, 2 );

                start = new Vector2( x1 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CircularInOut( _pStart.Y, _pEnd.Y, x1, 0.5f, true ) );
                end = new Vector2( x2 * Globals.GraphicsDevice.Viewport.Width, Interpolator.CircularInOut( _pStart.Y, _pEnd.Y, x2, 0.5f, true ) );
                spriteBatch.DrawLine( Color.Purple, start, end, 2 );
            }
        }
    }
}
