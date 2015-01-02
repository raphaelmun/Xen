using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xen2D;

namespace XenGameBase_UnitTests
{
    [TestClass]
    public class WhenUsingSortedDrawable2DCollection
    {
        class MockRenderable : Renderable2DBase<MockRenderable>
        {
            protected override void DrawInternal( SpriteBatch spriteBatch, Matrix transformFromWorldToCamera )
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void DefaulSortComparerIsNull()
        {
            var col = new SortedRenderable2DCollection<MockRenderable>();
            Assert.IsNull( col.SortComparer );
        }

        //[TestMethod]
        //public void InsertingRenderablesResultsInSortedOrder()
        //{
        //    var col = new SortedRenderable2DCollection<MockRenderable>();
        //    var element0 = MockRenderable.Acquire();
        //    element0.DrawOrder = 0;

        //    var element2 = MockRenderable.Acquire();
        //    element2.DrawOrder = 2;

        //    var element7 = MockRenderable.Acquire();
        //    element7.DrawOrder = 7;

        //    col.Add( element7 );
        //    col.Add( element0 );
        //    col.Add( element2 );

        //    Assert.AreEqual( element0, col.Items[ 0 ] );
        //    Assert.AreEqual( element2, col.Items[ 1 ] );
        //    Assert.AreEqual( element7, col.Items[ 2 ] );
        //}
    }
}
