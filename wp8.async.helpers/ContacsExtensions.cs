using System;
using System.Threading.Tasks;
using Microsoft.Phone.UserData;

namespace WP8.Async.Helpers
{
    public static class ContacsExtensions
    {
        /// <summary>
        /// Asynchronously searches for contacts in the user’s contact data.
        /// </summary>
        /// <param name="contacts">The contacts which will be searched.</param>
        /// <param name="filter">The filter to use to search for contacts.</param>
        /// <param name="filterKind">The kind of filter to use when searching for contacts.</param>
        /// <param name="state">A user-defined object that contains information about the operation.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property
        /// on the task object returns search results.</returns>
        public static Task<ContactsSearchEventArgs> SearchTaskAsync(this Contacts contacts, string filter, FilterKind filterKind, object state)
        {
            if (contacts == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<ContactsSearchEventArgs>();
            EventHandler<ContactsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                contacts.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            contacts.SearchCompleted += handler;
            try
            {
                contacts.SearchAsync(filter, filterKind, state);
            }
            catch
            {
                contacts.SearchCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }
    }
}
