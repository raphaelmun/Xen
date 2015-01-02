using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace TextRender
{
    public static class SpriteBatchEx
    {/*
        public static void DrawString(
            this SpriteBatch spriteBatch,
            XenString text
            )
        {
            DrawString( spriteBatch, null, text, Matrix.Identity );
        }

        public static void DrawString(
            this SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            XenString text
            )
        {
            DrawString( spriteBatch, spriteFont, text, Matrix.Identity );
        }

        public static void DrawString(
            this SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            XenString text,
            Matrix transformFromWorldToCamera
            )
        {
            SpriteFont font = ( spriteFont != null ? spriteFont : text.SpriteFont );
            if( font == null ) throw new ArgumentNullException( "spriteFont" );

            Matrix transformFromTextToWorld = text.RenderingExtent.TranslateFrom;
            Matrix fromTextToCamera = transformFromTextToWorld * transformFromWorldToCamera;
            Vector2 position = Vector2.Transform( text.RenderingExtent.ReferenceRegion.TopLeft, fromTextToCamera );

            Vector3 scale, translate;
            Quaternion rotate;
            fromTextToCamera.Decompose( out scale, out rotate, out translate );
            fromTextToCamera.Translation = Vector3.Zero;
            
            float angle = XenMath.GetAngleFloat( Vector2.Transform( Vector2.UnitX, fromTextToCamera ) );

            Vector2 nextPosition = position;
            switch( text._type )
            {
                case XenString.XenStringType.String:
                    spriteBatch.DrawString( font, text.Text, position, text.DisplayAttributes.ModulationColorWithOpacity, angle, Vector2.Zero, new Vector2( scale.X, scale.Y ), text.DisplayAttributes.SpriteEffects, text.DisplayAttributes.LayerDepth );
                    nextPosition += font.MeasureString( text.Text ) * Vector2.Transform( Vector2.UnitX, Matrix.CreateRotationZ( angle ) );
                    break;
                case XenString.XenStringType.Integer:
                    nextPosition = spriteBatch.DrawInteger( font, text.ValueInt, position, text.DisplayAttributes.ModulationColorWithOpacity, angle, Vector2.Zero, new Vector2( scale.X, scale.Y ), text.DisplayAttributes.SpriteEffects, text.DisplayAttributes.LayerDepth );
                    break;
                case XenString.XenStringType.Float:
                    nextPosition = spriteBatch.DrawDecimal( font, text.ValueFloat, text.FloatDecimalPlaces, position, text.DisplayAttributes.ModulationColorWithOpacity, angle, Vector2.Zero, new Vector2( scale.X, scale.Y ), text.DisplayAttributes.SpriteEffects, text.DisplayAttributes.LayerDepth );
                    break;
            }
            if( text._nextString != null )
            {
                spriteBatch.DrawString( spriteFont, text._nextString, nextPosition, text.DisplayAttributes.ModulationColorWithOpacity, angle, Vector2.Zero, new Vector2( scale.X, scale.Y ), text.DisplayAttributes.SpriteEffects, text.DisplayAttributes.LayerDepth );
            }
            if( text.IsTemporary )
            {
                text.Release();
            }
        }

        public static void DrawString(
            this SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            XenString text,
            Vector2 position,
            Color color
            )
        {
            DrawString( spriteBatch, spriteFont, text, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f );
        }

        public static void DrawString(
            this SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            XenString text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
            )
        {
            DrawString( spriteBatch, spriteFont, text, position, color, rotation, origin, Vector2.One * scale, effects, layerDepth );
        }

        public static void DrawString(
            this SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            XenString text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
            )
        {
            SpriteFont font = ( spriteFont != null ? spriteFont : text.SpriteFont );
            if( font == null ) throw new ArgumentNullException( "spriteFont" );

            Vector2 nextPosition = position;
            switch( text._type )
            {
                case XenString.XenStringType.String:
                    spriteBatch.DrawString( font, text.Text, position, color, rotation, origin, scale, effects, layerDepth );
                    nextPosition += font.MeasureString( text.Text ) * Vector2.Transform( Vector2.UnitX, Matrix.CreateRotationZ( rotation ) );
                    break;
                case XenString.XenStringType.Integer:
                    nextPosition = spriteBatch.DrawInteger( font, text.ValueInt, position + Vector2.Transform( text.RenderingExtent.ReferenceRegion.TopLeft, text.Transform.TranslateFrom ), text.DisplayAttributes.ModulationColorWithOpacity );
                    break;
                case XenString.XenStringType.Float:
                    nextPosition = spriteBatch.DrawDecimal( font, text.ValueFloat, position + Vector2.Transform( text.RenderingExtent.ReferenceRegion.TopLeft, text.Transform.TranslateFrom ), text.DisplayAttributes.ModulationColorWithOpacity );
                    break;
            }
            if( text._nextString != null )
            {
                spriteBatch.DrawString( spriteFont, text._nextString, nextPosition, color, rotation, origin, scale, effects, layerDepth );
            }
            if( text.IsTemporary )
            {
                text.Release();
            }
        }*/
    }
}
