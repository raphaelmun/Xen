using System;
using System.Diagnostics;
using XenAspects;

namespace Demo_PoolMemoryTest
{
    class TestObject : ComposableObject<TestObject>
    {
    }
    
    class Program
    {
        struct MockEventArgs
        {
            public float A;
            public long B;
        }

        static void Main( string[] args )
        {
            Action<MockEventArgs> handler = new Action<MockEventArgs>( OnEventFired );
            Event<MockEventArgs> ev = new Event<MockEventArgs>();
            ev.Add( handler );

            GC.Collect();
            WeakReference weakRef = new WeakReference( new Object() );
            long memBefore = GC.GetTotalMemory( true );

            for( int i = 0; i < 1000000; i++ )
            {
                ev.Notify( new MockEventArgs() );
            }

            long totalAllocated = GC.GetTotalMemory( true ) - memBefore;
            Console.WriteLine( "Total Allocated: " + totalAllocated );
        }

        static void OnEventFired( MockEventArgs args )
        {
        }
    }
}
