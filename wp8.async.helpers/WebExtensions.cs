using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WP8.Async.Helpers
{
    public static class WebExtensions
    {
        public static Task<Stream> BeginGetRequestStreamAsync(this WebRequest @this, object state)
        {
            return Task<Stream>.Factory.FromAsync(@this.BeginGetRequestStream, @this.EndGetRequestStream, state);
        }

        public static Task<WebResponse> BeginGetResponseAsync(this WebRequest @this, object state)
        {
            return Task<WebResponse>.Factory.FromAsync(@this.BeginGetResponse, @this.EndGetResponse, state);
        }
    }
}
