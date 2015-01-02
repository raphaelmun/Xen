using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using Microsoft.Xna.Framework;

namespace XenGameBase
{
    public interface IScene : IRenderable2D
    {
        IPooledObjectNList<ILayer> Layers { get; }
    }

    /// <summary>
    /// A scene represents everything the user sees in a particular mode of the game.  Some examples of scenes:
    /// -Title scene/Main menu
    /// -Intro movie
    /// -Actual game level
    /// </summary>
    public class Scene : CompositeElement2D<Scene>, IScene
    {
        private SortedLayerCollection _layers = new SortedLayerCollection();

        public IPooledObjectNList<ILayer> Layers{ get { return _layers; } }

        protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
        {
            foreach( ILayer layer in _layers.Items )
            {
                layer.Draw( spriteBatch, transformFromWorldToCamera );
            }
        }
    }
}
