using System;
using System.Threading.Tasks;
using Microsoft.Phone.UserData;

namespace WP8.Async.Helpers
{
    public static class ContacsExtensions
    {
        public static Task<ContactsSearchEventArgs> SearchTaskAsync(this Contacts @this, string filter, FilterKind filterKind, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<ContactsSearchEventArgs>();
            EventHandler<ContactsSearchEventArgs> handler = null;
            handler = (sender, e) =>
                {
                    @this.SearchCompleted -= handler;
                    tcs.TrySetResult(e);
                };

            @this.SearchCompleted += handler;
            @this.SearchAsync(filter, filterKind, state);

            return tcs.Task;
        }
    }
}
