using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Xen3D
{
    /// <summary>
    /// This class represents a static model.  
    /// </summary>
    /// <typeparam name="T">The parameterized type.</typeparam>
    public class StaticModel : Model3D<StaticModel>
    {
        private static BoundingBox CreateFromModel( Model model, Matrix sourceTransform )
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            Matrix[] transforms = new Matrix[ model.Bones.Count ];
            model.CopyAbsoluteBoneTransformsTo( transforms );

            foreach( ModelMesh mesh in model.Meshes )
            {
                foreach( ModelMeshPart part in mesh.MeshParts)
                {
                    int vertexStride = part.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = part.NumVertices * vertexStride;

                    float[] vertexData = new float[ vertexBufferSize / sizeof( float ) ];
                    part.VertexBuffer.GetData<float>( vertexData );

                    for( int i = 0; i < vertexBufferSize / sizeof( float ); i += vertexStride / sizeof( float ) )
                    {
                        Vector3 transformedPosition = Vector3.Transform( new Vector3( vertexData[ i ], vertexData[ i + 1 ], vertexData[ i + 2 ] ), sourceTransform * transforms[ mesh.ParentBone.Index ] );

                        min = Vector3.Min( min, transformedPosition );
                        max = Vector3.Max( max, transformedPosition );
                    }
                }
            }

            return new BoundingBox( min, max );
        }

        public static StaticModel Acquire( Model model )
        {
            // default pixel size of 100px
            return Acquire( model, 100, Vector2.Zero, Matrix.Identity );
        }

        public static StaticModel Acquire( Model model, Matrix sourceTransform )
        {
            // default pixel size of 100px
            return Acquire( model, 100, Vector2.Zero, sourceTransform );
        }

        public static StaticModel Acquire( Model model, int pixelSize )
        {
            return Acquire( model, pixelSize, Vector2.Zero, Matrix.Identity );
        }

        public static StaticModel Acquire( Model model, int pixelSize, Matrix sourceTransform )
        {
            return Acquire( model, pixelSize, Vector2.Zero, sourceTransform );
        }

        public static StaticModel AcquireAndCenter( Model model, int pixelSize )
        {
            BoundingBox aabb = StaticModel.CreateFromModel( model, Matrix.Identity );
            Vector3 diffVec = ( aabb.Max - aabb.Min );
            float ratio = (float)pixelSize / Math.Max( diffVec.X, diffVec.Y );
            return Acquire( model, pixelSize, new Vector2( ratio * diffVec.X / 2, ratio * diffVec.Y / 2 ), Matrix.Identity );
        }

        public static StaticModel Acquire( Model model, int pixelSize, Vector2 origin, Matrix sourceTransform )
        {
            BoundingBox aabb = StaticModel.CreateFromModel( model, sourceTransform );
            Vector3 diffVec = ( aabb.Max - aabb.Min );
            float ratio = (float)pixelSize / Math.Max( diffVec.X, diffVec.Y );

            Debug.Assert( null != model );
            var tempModel = Acquire();
            tempModel.Reset( model, (int)( ratio * diffVec.X ), (int)( ratio * diffVec.Y ), sourceTransform );
            tempModel.RenderingExtent.ReAnchor( origin );
            return tempModel;
        }

        protected CachedModelDescriptor ModelDescriptor { get; set; }

        public StaticModel() 
        {
            ModelDescriptor = new CachedModelDescriptor();
            ModelInfo = ModelDescriptor;
        }

        //TODO: what does it mean to recycle instances of StaticModel?  What happens to the model?
        protected override void ResetDirectState()
        {
            base.ResetDirectState();
            RenderingExtent.Reset();
        }

        public void Reset( Model model, int pixelWidth, int pixelHeight, Matrix sourceTransform )
        {
            BoundingBox aabb = StaticModel.CreateFromModel( model, sourceTransform );
            Vector3 diffVec = ( aabb.Max - aabb.Min );

            Matrix transform = Matrix.CreateScale( pixelWidth / diffVec.X, pixelHeight / diffVec.Y, 1.0f ) *
                Matrix.CreateTranslation( -aabb.Min ) * sourceTransform;

            ModelDescriptor.Reset( model, pixelWidth, pixelHeight, transform );
            RenderingExtent.Reset( pixelWidth, pixelHeight );
        }
    }
}
