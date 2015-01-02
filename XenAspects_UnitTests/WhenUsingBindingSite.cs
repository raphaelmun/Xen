using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    public class Parent
    {
    }

    public class Child
    {
    }

    public class MockBindingSite<HostType, TargetType> : IBindingSite<HostType, TargetType>
    {
        public bool IsBound { get; set; }

        public HostType Host { get; set; }

        public TargetType Target { get; set; }

        public void Unbind()
        {
            throw new NotImplementedException();
        }

        public bool Bind( IBindingSite<TargetType, HostType> targetBindingSite )
        {
            throw new NotImplementedException();
        }

        public bool _BindBackCalled = false;
        public void BindBack( IBindingSite<TargetType, HostType> targetBindingSite )
        {
            _BindBackCalled = true;
        }
    }

    [TestClass]
    public class WhenUsingBinding
    {
        [TestMethod]
        public void NewlyCreatedBindingSitesAreNotBound()
        {
            Parent parent = new Parent();
            Child child = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );
            BindingSite<Child,Parent> childToParent = new BindingSite<Child, Parent>( child );

            Assert.AreEqual( false, parentToChild.IsBound );
            Assert.AreEqual( false, childToParent.IsBound );
        }

        [TestMethod]
        public void BindingSitesCausesOtherSiteToBindBack()
        {
            Parent parent = new Parent();
            Child child = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );
            MockBindingSite<Child,Parent> childToParent = new MockBindingSite<Child, Parent>();

            Assert.AreEqual( false, childToParent._BindBackCalled );

            parentToChild.Bind( childToParent );

            Assert.AreEqual( true, childToParent._BindBackCalled );
        }

        [TestMethod]
        public void BindingToNullYieldsFalse()
        {
            Parent parent = new Parent();
            Child child = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );

            Assert.IsFalse( parentToChild.Bind( null ) );
        }

        [TestMethod]
        public void BindingToAlreadyBoundSiteYieldsFalse()
        {
            Parent parent = new Parent();
            Child child = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );
            MockBindingSite<Child,Parent> childToParent = new MockBindingSite<Child, Parent>();
            childToParent.IsBound = true;

            Assert.IsFalse( parentToChild.Bind( childToParent ) );
        }

        [TestMethod]
        public void BindingSitesCausesBothToBecomeBound()
        {
            Parent parent = new Parent();
            Child child = new Child();
            BindingSite<Parent,Child> parentToChild = new BindingSite<Parent, Child>( parent );
            BindingSite<Child,Parent> childToParent = new BindingSite<Child, Parent>( child );

            parentToChild.Bind( childToParent );

            Assert.AreEqual( true, parentToChild.IsBound );
            Assert.AreEqual( true, childToParent.IsBound );
            Assert.AreEqual( child, parentToChild.Target );
            Assert.AreEqual( parent, childToParent.Target );
        }

        [TestMethod]
        public void SimpleUnbindingWorks()
        {
            Parent parent = new Parent();
            Child child = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );
            BindingSite<Child,Parent> childToParent = new BindingSite<Child, Parent>( child );

            parentToChild.Bind( childToParent );
            parentToChild.Unbind();

            Assert.AreEqual( false, parentToChild.IsBound );
            Assert.AreEqual( false, childToParent.IsBound );
            Assert.AreEqual( null, parentToChild.Target );
            Assert.AreEqual( null, childToParent.Target );
        }

        [TestMethod]
        public void BoundSiteCannotBindToAnotherSite()
        {
            Parent parent = new Parent();
            Child child = new Child();
            Child child2 = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );
            BindingSite<Child,Parent> childToParent = new BindingSite<Child, Parent>( child );
            BindingSite<Child,Parent> child2ToParent = new BindingSite<Child, Parent>( child2 );

            parentToChild.Bind( childToParent );
            Assert.IsFalse( parentToChild.Bind( child2ToParent ) );
        }

        [TestMethod]
        public void UnbindingBoundSiteCanBindToAnotherSite()
        {
            Parent parent = new Parent();
            Child child = new Child();
            Child child2 = new Child();
            BindingSite<Parent, Child> parentToChild = new BindingSite<Parent, Child>( parent );
            BindingSite<Child,Parent> childToParent = new BindingSite<Child, Parent>( child );
            BindingSite<Child,Parent> child2ToParent = new BindingSite<Child, Parent>( child2 );

            parentToChild.Bind( childToParent );
            parentToChild.Unbind();
            Assert.IsTrue( parentToChild.Bind( child2ToParent ) );
        }
    }
}
