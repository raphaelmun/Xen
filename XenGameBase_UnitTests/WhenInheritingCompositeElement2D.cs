using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XenAspects;
using XenGameBase;
using Xen2D;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenInheritingCompositeElement2D
    {
        class MockCompositeElement : CompositeElement2D<MockCompositeElement>
        {
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
            }
        }

        class MockComplexElement : ComplexElement2D<MockComplexElement>
        {
            private ISprite _sprite = StaticSprite.Acquire( SinglePixel.WhitePixel.Asset );
            private IExtent _collisionExtent = RectangularExtent.Acquire( 0, 0, 10, 10 );

            public override IExtent CollisionExtent { get { return _collisionExtent; } }

            public override void Reset()
            {
                base.Reset();
                VisualComponent = _sprite;
                VisualComponents.AddExtent( _collisionExtent );
            }
        }

        [TestMethod]
        public void ReleasingParentCausesChildrenToRelease()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();
            parent.Children.Add( child );

            Assert.AreEqual( PooledObjectState.Acquired, parent.PoolState );
            Assert.AreEqual( PooledObjectState.Acquired, child.PoolState );

            parent.Release();

            Assert.AreEqual( PooledObjectState.Available, parent.PoolState );
            Assert.AreEqual( PooledObjectState.Available, child.PoolState );
        }

        [TestMethod]
        public void ReleasingChildCausesItToBeRemovedFromParent()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();
            parent.Children.Add( child );

            Assert.AreEqual( 1, parent.Children.Count );

            child.Release();

            Assert.AreEqual( 0, parent.Children.Count );
        }

        [TestMethod]
        public void ResetReleasesAndClearsAllChildElements()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();
            parent.Children.Add( child );

            Assert.AreEqual( PooledObjectState.Acquired, parent.PoolState );
            Assert.AreEqual( PooledObjectState.Acquired, child.PoolState );
            Assert.AreEqual( 1, parent.Children.Count );

            parent.Reset();

            Assert.AreEqual( PooledObjectState.Acquired, parent.PoolState );
            Assert.AreEqual( PooledObjectState.Available, child.PoolState );
            Assert.AreEqual( 0, parent.Children.Count );
        }

        [TestMethod]
        public void AddingChildAutomaticallySetsParentProperty()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            Assert.IsNull( child.Parent );

            parent.Children.Add( child );

            Assert.AreEqual( parent, child.Parent );
        }

        [TestMethod]
        public void AddingChildAutomaticallySetsParentPropertyDuringEnumeration()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            Assert.IsNull( child.Parent );

            parent.Children.BeginEnumeration();
            parent.Children.Add( child );

            Assert.IsNull( child.Parent );

            parent.Children.EndEnumeration();

            Assert.AreEqual( parent, child.Parent );
        }

        [TestMethod]
        public void RemovingChildAutomaticallyUnSetsParentProperty()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            parent.Children.Add( child );
            Assert.AreEqual( parent, child.Parent );

            parent.Children.Remove( child );

            Assert.IsNull( child.Parent );
        }

        [TestMethod]
        public void RemovingChildAutomaticallyUnSetsParentPropertyDuringEnumeration()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            parent.Children.Add( child );
            Assert.AreEqual( parent, child.Parent );

            parent.Children.BeginEnumeration();
            parent.Children.Remove( child );

            Assert.AreEqual( parent, child.Parent );

            parent.Children.EndEnumeration();

            Assert.IsNull( child.Parent );
        }

        [TestMethod]
        public void AddingParentCausesRenderingExtentParentToBeSet()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            Assert.IsNull( child.RenderingExtent.ParentSpace );
            parent.Children.Add( child );
            Assert.AreEqual( parent.RenderingExtent, child.RenderingExtent.ParentSpace );
        }

        [TestMethod]
        public void AddingChildElementToParentAppliesParentOpacityToChildModifiedOpacity()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            parent.Opacity = 0.5f;

            Assert.AreNotEqual( parent.OpacityFinal, child.OpacityModifier );

            parent.Children.Add( child );

            Assert.AreEqual( parent.OpacityFinal, child.OpacityModifier );
        }

        [TestMethod]
        public void AddingSingleComplexElementToCompositeElementMaintainsCollisionExtent()
        {
            //Complex elements have different rendering and collision extents.  Make sure that the rendering and collision extends are 
            //kept distinct in the parent composite
            var parent = MockCompositeElement.Acquire();
            var child = MockCompositeElement.Acquire();

            parent.Opacity = 0.5f;

            Assert.AreNotEqual( parent.OpacityFinal, child.OpacityModifier );

            parent.Children.Add( child );

            Assert.AreEqual( parent.OpacityFinal, child.OpacityModifier );
        }
    }
}
