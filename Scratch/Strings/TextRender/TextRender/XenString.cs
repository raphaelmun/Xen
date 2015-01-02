using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using Microsoft.Xna.Framework;
using System.Text;

namespace TextRender
{/*
    public class XenString : Renderable2DBase<XenString>
    {
        internal enum XenStringType
        {
            String,
            Integer,
            Float,
        };

        private static readonly uint _defaultDecimalPlaces = 5;
        internal XenStringType _type = XenStringType.String;
        internal XenString _nextString = null;
        public StringBuilder Text { get; set; }
        public int ValueInt { get; set; }
        public float ValueFloat { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public bool IsTemporary { get; internal set; }
        public uint FloatDecimalPlaces { get; set; }

        public XenString()
        {
            _type = XenStringType.String;
            Text = null;
            _nextString = null;
            SpriteFont = null;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( string text )
        {
            _type = XenStringType.String;
            Text = new StringBuilder( text );
            _nextString = null;
            // TODO: Implement default SpriteFont!
            //SpriteFont = Globals.;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( SpriteFont spriteFont, string text )
        {
            _type = XenStringType.String;
            Text = new StringBuilder( text );
            _nextString = null;
            SpriteFont = spriteFont;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( StringBuilder text )
        {
            _type = XenStringType.String;
            Text = text;
            _nextString = null;
            // TODO: Implement default SpriteFont!
            //SpriteFont = Globals.;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( SpriteFont spriteFont, StringBuilder text )
        {
            _type = XenStringType.String;
            Text = text;
            _nextString = null;
            SpriteFont = spriteFont;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( int value )
        {
            _type = XenStringType.Integer;
            ValueInt = value;
            Text = null;
            _nextString = null;
            // TODO: Implement default SpriteFont!
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( SpriteFont spriteFont, int value )
        {
            _type = XenStringType.Integer;
            ValueInt = value;
            Text = null;
            _nextString = null;
            SpriteFont = spriteFont;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( float value )
        {
            _type = XenStringType.Float;
            ValueFloat = value;
            Text = null;
            _nextString = null;
            // TODO: Implement default SpriteFont!
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }

        public void Reset( SpriteFont spriteFont, float value )
        {
            _type = XenStringType.Float;
            ValueFloat = value;
            Text = null;
            _nextString = null;
            SpriteFont = spriteFont;
            IsTemporary = false;
            FloatDecimalPlaces = _defaultDecimalPlaces;
        }
        
        /// <summary>
        /// Appends two strings together
        /// </summary>
        /// <param name="a">Left-side string</param>
        /// <param name="b">Right-side string</param>
        /// <returns>A temporary XenString appended with the second string</returns>
        /// <remarks>Temporary XenStrings are released and invalid after a Draw</remarks>
        public static XenString operator &( XenString a, XenString b )
        {
            XenString tempA = (a.IsTemporary ? a : a.GenerateTemporaryCopy()),
                      tempB = (b.IsTemporary ? b : b.GenerateTemporaryCopy());
            XenString tempChain = tempA;
            while( tempChain._nextString != null )
            {
                tempChain = tempChain._nextString;
            }
            tempChain._nextString = tempB;
            return tempA;
        }

        protected override void RenderInternal( SpriteBatch spriteBatch )
        {
            if( SpriteFont != null )
            {
                spriteBatch.DrawString( SpriteFont, this );
            }
        }

        public XenString GenerateTemporaryCopy()
        {
            XenString tempSelf = XenString.Acquire();
            switch (_type)
            {
                case XenStringType.String:
                    tempSelf.Reset( SpriteFont, Text );
                    break;
                case XenStringType.Integer:
                    tempSelf.Reset( SpriteFont, ValueInt );
                    break;
                case XenStringType.Float:
                    tempSelf.Reset( SpriteFont, ValueFloat );
                    break;
            }
            tempSelf.IsTemporary = true;
            return tempSelf;
        }

        public static XenString Temporary( int value )
        {
            return Temporary( null, value ); // TODO: Default SpriteFont!
        }

        public static XenString Temporary( SpriteFont spriteFont, int value )
        {
            XenString text = XenString.Acquire();
            text.Reset( spriteFont, value );
            text.IsTemporary = true;
            return text;
        }

        public static XenString Temporary( float value )
        {
            return Temporary( null, value ); // TODO: Default SpriteFont!
        }

        public static XenString Temporary( SpriteFont spriteFont, float value )
        {
            XenString text = XenString.Acquire();
            text.Reset( spriteFont, value );
            text.IsTemporary = true;
            return text;
        }
    }*/
}
