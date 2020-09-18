using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Orlen.Backend.Tests.Performance
{
    public class ArraysBenchmarks
    {
        private int[] _flatArray;
        private int[][] _jaggedArray;
        private int[,] _multiDimArray;

        [Params(10, 100)]
        public int Width { get; set; }

        [Params(10, 100)]
        public int Height { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            int total = Width * Height;
            _flatArray = new int[total];

            _multiDimArray = new int[Height, Width];

            _jaggedArray = new int[Height][];
            for (int i = 0; i < Height; ++i)
            {
                _jaggedArray[i] = new int[Width];
            }
        }

        [Benchmark]
        public int[] FlatArray()
        {
            int total = Width * Height;

            for (int i = 0; i < total; ++i)
            {
                _flatArray[i] = i;
            }

            return _flatArray;
        }

        [Benchmark]
        public int[,] MultiDimArray()
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    _multiDimArray[i, j] = i;
                }
            }

            return _multiDimArray;
        }

        [Benchmark]
        public int[][] JaggedArray()
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    _jaggedArray[i][j] = i;
                }
            }

            return _jaggedArray;
        }
    }
}