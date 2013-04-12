using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WP8.Async.Helpers
{
    public static class WebRequestExtensions
    {
        /// <summary>
        /// Begins an asynchronous request for a System.IO.Stream object to use to write
        /// data.
        /// </summary>
        /// <param name="request">The request that will be written into.</param>
        /// <param name="state">The state object for this request.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property
        /// returns a System.IO.Stream to use to write request data.</returns>
        public static Task<Stream> BeginGetRequestStreamAsync(this WebRequest request, object state)
        {
            return Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, state);
        }

        /// <summary>
        /// Begins an asynchronous request to an Internet resource.
        /// </summary>
        /// <param name="request">The request which will be invoked.</param>
        /// <param name="state">The state object for this request.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property
        /// returns a System.Net.WebResponse.</returns>
        public static Task<WebResponse> BeginGetResponseAsync(this WebRequest request, object state)
        {
            return Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, state);
        }
    }
}
