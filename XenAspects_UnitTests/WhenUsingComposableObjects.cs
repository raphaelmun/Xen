using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XenAspects;

namespace XenAspects_UnitTests
{
    //This example shows a component in a car: the chassis which contains four wheels.  Both chassis and wheels have 
    //direct state; state that moves with the object to and from the pool.  This direct state can be reset to a set of defaults.
    //When the Chassis is released, all four wheels are released.  When the Chassis is re-acquired from the pool, 
    //all four wheels are re-acquired.  
    public class Wheel : ComposableObject<Wheel>
    {
        private struct WheelState
        {
            public float _angle;
            public bool _rotating;
        }

        #region Direct State
        WheelState _state;

        protected override void ResetDirectState()
        {
            _state = new WheelState();
            base.ResetDirectState();
        }
        #endregion

        #region Properties
        public float Angle
        {
            get { return _state._angle; }
            set { _state._angle = value; }
        }

        public bool Rotating
        {
            get { return _state._rotating; }
            set { _state._rotating = value; }
        }
        #endregion

        public bool ResetCalled = false;
        public override void Reset()
        {
            ResetCalled = true;
            base.Reset();
        }
    }

    public class Chassis : ComposableObject<Chassis>
    {
        #region Direct State
        long _distanceTraveled;

        protected override void ResetDirectState()
        {
            _distanceTraveled = 0;
            base.ResetDirectState();
        }
        #endregion

        #region Composed State
        ComposedField<Wheel> _wheelOne = new ComposedField<Wheel>( Wheel.Acquire );
        ComposedField<Wheel> _wheelTwo = new ComposedField<Wheel>( Wheel.Acquire );
        #endregion

        #region Properties

        public long DistanceTraveled
        {
            get { return _distanceTraveled; }
            set { _distanceTraveled = value; }
        }

        public Wheel WheelOne
        {
            get { return _wheelOne.Value; }
        }

        public Wheel WheelTwo
        {
            get { return _wheelTwo.Value; }
        }
        #endregion

        public Chassis()
        {
            DeclareComposition( _wheelOne, _wheelTwo );
        }

        public bool ResetCalled = false;
        public override void Reset()
        {
            ResetCalled = true;
            base.Reset();
        }

        public void Use()
        {
            _distanceTraveled = 50;
            WheelOne.Rotating = true;
            WheelOne.Angle = 15;
            WheelTwo.Rotating = true;
            WheelTwo.Angle = 25;
        }
    }

    [TestClass]
    public class WhenUsingComposableObjects
    {
        [TestMethod]
        public void ReleasingParentCausesDeclaredComposedFieldsToExpire()
        {
            Chassis chassis = Chassis.Acquire();
            Assert.IsTrue( chassis.WheelOne != null );
            Assert.IsTrue( chassis.WheelTwo != null );

            var w1 = chassis.WheelOne;
            var w2 = chassis.WheelTwo;

            Assert.AreEqual( PooledObjectState.Acquired, w1.PoolState );
            Assert.AreEqual( PooledObjectState.Acquired, w2.PoolState );

            chassis.Release();

            Assert.AreEqual( null, chassis.WheelOne );
            Assert.AreEqual( null, chassis.WheelTwo );

            Assert.AreEqual( PooledObjectState.Available, w1.PoolState );
            Assert.AreEqual( PooledObjectState.Available, w2.PoolState );
        }

        [TestMethod]
        public void AcquiringComposableCallsReset()
        {
            Chassis c = Chassis.Acquire();
            Assert.IsTrue( c.ResetCalled );
        }

        [TestMethod]
        public void AcquiringComposableCallsResetOnChildren()
        {
            Chassis c = Chassis.Acquire();
            Assert.IsTrue( c.WheelOne.ResetCalled );
            Assert.IsTrue( c.WheelTwo.ResetCalled );
        }

        [TestMethod]
        public void ResetWorks()
        {
            Chassis c = Chassis.Acquire();
            c.Use();

            Assert.AreNotEqual( 0, c.DistanceTraveled );
            Assert.AreNotEqual( 0, c.WheelOne.Angle );
            Assert.AreNotEqual( 0, c.WheelTwo.Angle );
            Assert.AreNotEqual( false, c.WheelOne.Rotating );
            Assert.AreNotEqual( false, c.WheelTwo.Rotating );

            c.Reset();
            Assert.AreEqual( 0, c.DistanceTraveled );
            Assert.AreEqual( 0, c.WheelOne.Angle );
            Assert.AreEqual( 0, c.WheelTwo.Angle );
            Assert.AreEqual( false, c.WheelOne.Rotating );
            Assert.AreEqual( false, c.WheelTwo.Rotating );
        }

        [TestMethod]
        public void ResetInvokesReleasedEvent()
        {
            Chassis c = Chassis.Acquire();

            bool releaseEventRaised = false;
            c.OnReleased.Add( ( obj ) => 
            {
                releaseEventRaised = true;
            } );

            Assert.IsFalse( releaseEventRaised );
            c.Reset();
            Assert.IsTrue( releaseEventRaised );
        }
    }
}
