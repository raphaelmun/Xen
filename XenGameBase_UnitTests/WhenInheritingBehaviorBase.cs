using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XenGameBase;
using Xen2D;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenInheritingBehaviorBase
    {
        class MockBehavior : BehaviorBase<MockBehavior>
        {
        }

        class MockElement : Element2D<MockElement>
        {
            public MockElement()
            {
                VisualComponent = new PlaceholderRenderable2D();
            }
        }

        [TestMethod]
        public void NewlyAcquiredBehaviorHasNullParent()
        {
            var mock = MockBehavior.Acquire();
            Assert.IsNull( mock.Parent );
        }

        [TestMethod]
        public void SettingParentSecondTimeThrowsException()
        {
            var mock = MockBehavior.Acquire();
            mock.Parent = MockElement.Acquire();

            try
            {
                mock.Parent = MockElement.Acquire();
                Assert.Fail( "should not reach here" );
            }
            catch( InvalidOperationException ) { }
        }

        [TestMethod]
        public void DefaultWantsInputIsFalse()
        {
            var mock = MockBehavior.Acquire();
            Assert.IsFalse( mock.WantsInput );
        }

        [TestMethod]
        public void AddingBehaviorToParentCausesItsTargetToBeSet()
        {
            var mock = MockBehavior.Acquire();
            var element = MockElement.Acquire();

            Assert.AreNotEqual( element, mock.Parent );
            
            element.Behaviors.Add( mock );

            Assert.AreEqual( element, mock.Parent );
        }
    }
}
