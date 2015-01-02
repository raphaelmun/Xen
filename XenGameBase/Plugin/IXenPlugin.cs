using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XenGameBase
{
    public interface IXenPlugin
    {
        // Plugin Identifiers
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        Action<IElement2D> OnItemBeforeAdded { get; }
        Action<IElement2D> OnItemAfterAdded { get; }
        Action<IElement2D> OnItemBeforeRemoved { get; }
        Action<IElement2D> OnItemAfterRemoved { get; }

        void Initialize( Layer layer );
        void Load();
        void Unload();

        void Update( GameTime gameTime );
        void Draw( GameTime gameTime, SpriteBatch spriteBatch );

        void Call( int functionalityID, params object[] parameterList );
    }
}
