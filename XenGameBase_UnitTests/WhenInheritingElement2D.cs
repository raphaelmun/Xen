using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;
using XenAspects;
using XenGameBase;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenInheritingElement2D
    {
        class MockElement : Element2D<MockElement>
        {
            public MockElement()
            {
                VisualComponent = new PlaceholderRenderable2D();
            }

            public void SetCollisionClass( uint cc )
            {
                CollisionClass = cc;
            }

            internal void SetCollisionMode( CollisionMode collisionMode )
            {
                CollisionMode = collisionMode;
            }
        }

        class MockCompositeElement : CompositeElement2D<MockCompositeElement>
        {
        }

        class MockBehavior : BehaviorBase<MockBehavior>, IBehavior 
        {
            public bool DrawCalled = false;
            public bool UpdateCalled = false;
            public bool ProcessInputCalled = false;

            protected override void UpdateInternal( GameTime gameTime ) { UpdateCalled = true; }
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera ) { DrawCalled = true; }
            protected override void ProcessInputInternal( ref InputState input, Matrix transformFromCameraToWorld ) { ProcessInputCalled = true; }
            internal void SetWantsInput( bool wantsInput ) { WantsInput = wantsInput; }
        }

        class MockRenderable : Renderable2DBase<MockRenderable>
        {
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
            }
        }

        [TestMethod]
        public void NewlyAcquiredInstanceIsVisible()
        {
            var mock = MockElement.Acquire();
            Assert.IsTrue( mock.Visible );
        }

        [TestMethod]
        public void ReleaseClearsBehaviorCollection()
        {
            var mock = MockElement.Acquire();
            mock.BehaviorItems.Add( MockBehavior.Acquire() );

            Assert.AreNotEqual( 0, mock.BehaviorItems.Count );
            mock.Release();

            Assert.AreEqual( 0, mock.BehaviorItems.Count );
        }

        [TestMethod]
        public void ReleaseCausesBehaviorsToRelease()
        {
            var mock = MockElement.Acquire();
            var behavior = MockBehavior.Acquire();
            mock.BehaviorItems.Add( behavior );

            Assert.AreEqual( PooledObjectState.Acquired, behavior.PoolState );
            mock.Release();

            Assert.AreEqual( PooledObjectState.Available, behavior.PoolState );
        }

        [TestMethod]
        public void ResetClearsBehaviorCollection()
        {
            var mock = MockElement.Acquire();
            mock.Behaviors.Add( MockBehavior.Acquire() );

            Assert.AreNotEqual( 0, mock.BehaviorItems.Count );
            mock.Reset();

            Assert.AreEqual( 0, mock.BehaviorItems.Count );
        }

        [TestMethod]
        public void ResetSetsParentToNull()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockElement.Acquire();
            child.Parent = parent;

            child.Reset();
            Assert.IsNull( child.Parent );
        }

        [TestMethod]
        public void DefaultCollisionClassDoesNotCollide()
        {
            var mock = MockElement.Acquire();
            Assert.AreEqual( CollisionClasses.DoesNotCollide, mock.CollisionClass );
        }

        [TestMethod]
        public void ResetRestoresCollisionClass()
        {
            var mock = MockElement.Acquire();
            mock.SetCollisionClass( 3 );

            Assert.AreNotEqual( CollisionClasses.DoesNotCollide, mock.CollisionClass );
            mock.Reset();
            Assert.AreEqual( CollisionClasses.DoesNotCollide, mock.CollisionClass );
        }

        [TestMethod]
        public void DefaultCollisionModeIsExtent()
        {
            var mock = MockElement.Acquire();
            Assert.AreEqual( CollisionMode.Extent, mock.CollisionMode );
        }

        [TestMethod]
        public void ResetRestoresCollisionMode()
        {
            var mock = MockElement.Acquire();
            mock.SetCollisionMode( CollisionMode.InnerCenterCircle );

            Assert.AreNotEqual( CollisionMode.Extent, mock.CollisionMode );
            mock.Reset();
            Assert.AreEqual( CollisionMode.Extent, mock.CollisionMode);
        }

        [TestMethod]
        public void SettingNonNullParentToAnotherParentThrows()
        {
            var parent = MockCompositeElement.Acquire();
            var parent2 = MockCompositeElement.Acquire();
            var child = MockElement.Acquire();
            child.Parent = parent;

            try
            {
                //Child must first be detached from the current parent before being attached to another parent.
                child.Parent = parent2;
                Assert.Fail( "Should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void SettingParentToItselfWorks()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockElement.Acquire();
            child.Parent = parent;
            child.Parent = parent;
        }

        [TestMethod]
        public void SettingParentToNullCausesChildToBeRemovedFromParentsChildren()
        {
            var parent = MockCompositeElement.Acquire();
            var child = MockElement.Acquire();

            Assert.AreEqual( 0, parent.Children.Count );
            child.Parent = parent;

            Assert.AreEqual( 1, parent.Children.Count );
            child.Parent = null;
            Assert.AreEqual( 0, parent.Children.Count );
        }

        [TestMethod]
        public void UpdateCausesEnabledBehaviorsToBeUpdated()
        {
            var mock = MockElement.Acquire();
            var enabledBehavior = MockBehavior.Acquire();
            enabledBehavior.Enabled = true;
            var unenabledBehavior = MockBehavior.Acquire();
            unenabledBehavior.Enabled = false;

            mock.BehaviorItems.Add( enabledBehavior );
            mock.BehaviorItems.Add( unenabledBehavior );

            Assert.IsFalse( enabledBehavior.UpdateCalled );
            Assert.IsFalse( unenabledBehavior.UpdateCalled );

            mock.Update( GameTimeUtility.GameTimeZero );

            Assert.IsTrue( enabledBehavior.UpdateCalled );
            Assert.IsFalse( unenabledBehavior.UpdateCalled );
        }

        [TestMethod]
        public void AddingBehaviorCausesBehaviorParentToBeSet()
        {
            var element = MockElement.Acquire();
            var behavior = MockBehavior.Acquire();

            Assert.AreNotEqual( element, behavior.Parent );
            element.Behaviors.Add( behavior );

            Assert.AreEqual( element, behavior.Parent );
        }

        [TestMethod]
        public void RemovingBehaviorCausesBehaviorParentToBeUnSet()
        {
            var element = MockElement.Acquire();
            var behavior = MockBehavior.Acquire();

            Assert.AreNotEqual( element, behavior.Parent );
            element.Behaviors.Add( behavior );

            Assert.AreEqual( element, behavior.Parent );

            element.Behaviors.Remove( behavior );

            Assert.IsNull( behavior.Parent );
        }

        [TestMethod]
        public void DerivedInstanceWantsInput()
        {
            var element = MockElement.Acquire();
            Assert.IsTrue( element.WantsInput );
        }

        [TestMethod]
        public void BehaviorsCalledWithProcessInputIfWantInputIsTrue()
        {
            var element = MockElement.Acquire();
            var behavior = MockBehavior.Acquire();
            behavior.SetWantsInput( false );
            element.Behaviors.Add( behavior );

            InputState input = new InputState();

            Assert.IsFalse( behavior.ProcessInputCalled );
            element.ProcessInput( ref input, Matrix.Identity );
            Assert.IsFalse( behavior.ProcessInputCalled );
            behavior.SetWantsInput( true );
            element.ProcessInput( ref input, Matrix.Identity );
            Assert.IsTrue( behavior.ProcessInputCalled );
        }

        [TestMethod]
        public void DefaultElementCollisionModeIsExtent()
        {
            var element = MockElement.Acquire();
            Assert.AreEqual( CollisionMode.Extent, element.CollisionMode );
        }

        [TestMethod]
        public void DefaultElementCollisionClassEqualsDoesNotCollide()
        {
            var element = MockElement.Acquire();
            Assert.AreEqual( CollisionClasses.DoesNotCollide, element.CollisionClass );
        }

        [TestMethod]
        public void DefaultElementIsNotCollidable()
        {
            var element = MockElement.Acquire();
            Assert.IsFalse( element.IsCollidable );
        }

        [TestMethod]
        public void DefaultElementHasEmptyDecorators()
        {
            var element = MockElement.Acquire();
            Assert.AreEqual( 0, element.Decorators.Count );
        }

        [TestMethod]
        public void ResetCausesDecoratorsToBeReleased()
        {
            var element = MockElement.Acquire();
            var decorator = ElementDecorator.Acquire( MockRenderable.Acquire(), Vector2.Zero, Vector2.Zero );
            element.Decorators.Add( decorator );

            Assert.IsTrue( decorator.IsAcquired );

            element.Release();

            Assert.IsFalse( decorator.IsAcquired );
        }
    }
}
