using System;
using System.IO;
using System.Threading.Tasks;

namespace WP8.Async.Helpers
{
    public static class FileStreamExtensions
    {
        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current
        /// stream and advances the position within the stream by the number of bytes
        /// read.
        /// </summary>
        /// <param name="fileStream">The FileStream which will be read from.</param>
        /// <param name="buffer">The buffer to read the data into.</param>
        /// <param name="offset">The byte offset in buffer at which to begin writing data read from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="userCallback">An optional asynchronous callback, to be called when the read is complete.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous read
        ///  request from other requests.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property
        /// on the task object returns the number of bytes read from the stream, between zero (0)
        /// and the number of bytes you requested. Streams return zero (0) only at the end of the
        /// stream, otherwise, they should block until at least one byte is available.</returns>
        public static Task<int> ReadAsync(this FileStream fileStream, byte[] buffer, int offset, int count, AsyncCallback userCallback, object state)
        {
            if (fileStream == null)
                throw new NullReferenceException();

            return Task<int>.Factory.FromAsync(fileStream.BeginRead, fileStream.EndRead, buffer, offset, count, state);
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number
        /// of bytes written.
        /// </summary>
        /// <param name="fileStream">The FileStream which will be read from.</param>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The byte offset in buffer from which to begin writing.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="userCallback">An optional asynchronous callback, to be called when the write is complete.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write
        /// request from other requests.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static Task WriteAsync(this FileStream fileStream, byte[] buffer, int offset, int count, AsyncCallback userCallback, object state)
        {
            if (fileStream == null)
                throw new NullReferenceException();

            return Task.Factory.FromAsync(fileStream.BeginWrite, fileStream.EndWrite, buffer, offset, count, state);
        }
    }
}
