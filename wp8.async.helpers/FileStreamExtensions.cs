using System;
using System.IO;
using System.Threading.Tasks;

namespace WP8.Async.Helpers
{
    public static class FileStreamExtensions
    {
        public static Task<int> ReadAsync(this FileStream @this, byte[] array, int offset, int numBytes, AsyncCallback userCallback, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            return Task<int>.Factory.FromAsync(@this.BeginRead, @this.EndRead, array, offset, numBytes, state);
        }

        public static Task WriteAsync(this FileStream @this, byte[] array, int offset, int numBytes, AsyncCallback userCallback, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            return Task.Factory.FromAsync(@this.BeginWrite, @this.EndWrite, array, offset, numBytes, state);
        }
    }
}
