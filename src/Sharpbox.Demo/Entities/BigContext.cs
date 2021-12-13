using System;
using System.Linq;

namespace Sharpbox.Demo.Entities
{
    public class BigContext : IDisposable
    {
        private static ulong _instancesCount = 0;
        private bool _isDisposed;

        public BigContext()
        {
            Id = _instancesCount;
            _instancesCount++;
        }

        public ulong Id { get; }
        public int[] BigArray { get; set; } = Enumerable.Range(0, 1000).ToArray();

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            System.Console.WriteLine($"Disposing -- {this.GetType()}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}