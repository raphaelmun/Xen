using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XenAspects_UnitTests
{
    public interface IMethod
    {
        bool InterfaceMethod();
        bool VirtualInterfaceMethod();
    }

    public class Base : IMethod
    {
        private int _value = 5;
        private bool _bool = true;

        public int GetValue()
        {
            return _value;
        }

        public bool InterfaceMethod()
        {
            return _bool;
        }

        public virtual bool VirtualInterfaceMethod()
        {
            return _bool;
        }
    }

    public class Derived : Base
    {
        private int _myValue = 10;
        private bool _bool = false;

        new public int GetValue()
        {
            return _myValue;
        }

        public override bool VirtualInterfaceMethod()
        {
            return _bool;
        }
    }

    [TestClass]
    public class CLRPerfMetrics
    {
        [TestMethod]
        public void NewModifier()
        {
            Base b = new Base();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for( int i = 0; i < 100000; i++ )
            {
                b.GetValue();
            }

            sw.Stop();
            double elapsedDirect = sw.Elapsed.TotalMilliseconds;
            //100000 calls: 3.4ms

            Derived d = new Derived();
            sw.Reset();
            sw.Start();

            for( int i = 0; i < 100000; i++ )
            {
                d.GetValue();
            }

            sw.Stop();
            double elapsedNew = sw.Elapsed.TotalMilliseconds;
            //100000 calls: 3.4ms
        }
    }
}
