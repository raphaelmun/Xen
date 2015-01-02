using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XenAspects;

namespace Xen3D
{
    /// <summary>
    /// This base class represents a sprite, which consists of a texture or animation that can draw itself
    /// at a particular location on the screen.
    /// </summary>
    /// <typeparam name="T">The parameterized type.</typeparam>
    public abstract class Model3D<T> : Renderable3DBase<T>, IModel
        where T : ComposableObject, new()
    {
        public virtual IModelInfo ModelInfo { get; protected set; }

        protected override void DrawInternal( Matrix transformFromWorldToCamera )
        {
            //Matrix finalTransform = 
            //    Matrix.CreateScale( RenderingExtent.Scale.X / ModelInfo.PixelSize.X, RenderingExtent.Scale.Y / ModelInfo.PixelSize.Y, 1.0f ) *
            //    Matrix.CreateTranslation( -RenderingExtent.Origin.X, -RenderingExtent.Origin.Y, 0.0f ) *
            //    Matrix.CreateRotationZ( RenderingExtent.Angle ) *
            //    Matrix.CreateTranslation( RenderingExtent.Origin.X + RenderingExtent.Anchor.X, RenderingExtent.Origin.Y + RenderingExtent.Anchor.Y, 0.0f ) *

            ////spriteBatch.DrawSprite( this, transformFromWorldToCamera );
            //// TODO: Fix according to transformFromWorldToCamera
            //foreach( ModelMesh mesh in ModelInfo.Asset.Meshes )
            //{
            //    foreach( BasicEffect effect in mesh.Effects )
            //    {
            //        effect.EnableDefaultLighting();
            //        effect.View = Matrix.Identity;
            //        effect.Projection = Matrix.Identity;
            //        mesh.Draw();
            //    }
            //}
        }
    }
}
