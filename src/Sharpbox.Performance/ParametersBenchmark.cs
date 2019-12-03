using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Sharpbox.Performance
{
    public class ParametersBenchmark
    {
        [Params(1, 100)]
        public int N { get; set; }

        [Benchmark]
        public void PassIntViaValue()
        {
            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaValueInt(i);
            }
        }

        [Benchmark]
        public void PassIntViaInRef()
        {
            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefInt(i);
            }
        }

        [Benchmark]
        public void PassCachedIntViaValue()
        {
            const int subject = 42;

            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaValueInt(subject);
            }
        }

        [Benchmark]
        public void PassCachedIntViaInRef()
        {
            const int subject = 42;

            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefInt(subject);
            }
        }

        [Benchmark]
        public void PassDoubleViaValue()
        {
            for (double i = 0; i < N; ++i)
            {
                SendParameter.ViaValueDouble(i);
            }
        }

        [Benchmark]
        public void PassDoubleViaInRef()
        {
            for (double i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefDouble(i);
            }
        }

        [Benchmark]
        public void PassNewLargeStructViaValue()
        {
            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaValueLargeStruct(new LargeStruct());
            }
        }

        [Benchmark]
        public void PassNewLargeStructViaInRef()
        {
            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefLargeStruct(new LargeStruct());
            }
        }

        [Benchmark]
        public void PassCachedLargeStructViaValue()
        {
            var subject = new LargeStruct();

            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaValueLargeStruct(subject);
            }
        }

        [Benchmark]
        public void PassCachedLargeStructViaInRef()
        {
            var subject = new LargeStruct();

            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefLargeStruct(subject);
            }
        }

        [Benchmark]
        public void PassNewClassViaValue()
        {
            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaValueClass(new SomeClass());
            }
        }

        [Benchmark]
        public void PassNewClassViaInRef()
        {
            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefClass(new SomeClass());
            }
        }

        [Benchmark]
        public void PassCachedClassViaValue()
        {
            var subject = new SomeClass();

            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaValueClass(subject);
            }
        }

        [Benchmark]
        public void PassCachedClassViaInRef()
        {
            var subject = new SomeClass();

            for (int i = 0; i < N; ++i)
            {
                SendParameter.ViaInRefClass(subject);
            }
        }
    }

    public struct LargeStruct
    {
        public string Name { get; set; }
        public decimal X;
        public decimal Y;
        public decimal Z;
        public decimal W;
    }

    public class SomeClass
    {
        public decimal X;
        public decimal Y;
        public decimal Z;
        public string SomeName;
        public LargeStruct LargeStruct;
        public List<string> SomeProperty;
    }

    public static class SendParameter
    {
        public static int ViaValueInt(int value)
        {
            return value;
        }

        public static int ViaInRefInt(in int value)
        {
            return value;
        }

        public static double ViaValueDouble(double value)
        {
            return value;
        }

        public static double ViaInRefDouble(in double value)
        {
            return value;
        }

        public static LargeStruct ViaValueLargeStruct(LargeStruct value)
        {
            return value;
        }

        public static LargeStruct ViaInRefLargeStruct(in LargeStruct value)
        {
            return value;
        }

        public static SomeClass ViaValueClass(SomeClass value)
        {
            return value;
        }

        public static SomeClass ViaInRefClass(in SomeClass value)
        {
            return value;
        }
    }
}