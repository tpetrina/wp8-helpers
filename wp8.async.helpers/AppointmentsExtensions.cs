using System;
using System.Threading.Tasks;
using Microsoft.Phone.UserData;

namespace WP8.Async.Helpers
{
    public static class AppointmentsExtensions
    {
        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments @this, DateTime startTimeInclusive, DateTime endTimeInclusive, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
                {
                    @this.SearchCompleted -= handler;
                    tcs.TrySetResult(e);
                };

            @this.SearchCompleted += handler;
            @this.SearchAsync(startTimeInclusive, endTimeInclusive, state);

            return tcs.Task;
        }

        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments @this, DateTime startTimeInclusive, DateTime endTimeInclusive, Account account, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                @this.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            @this.SearchCompleted += handler;
            @this.SearchAsync(startTimeInclusive, endTimeInclusive, account, state);

            return tcs.Task;
        }

        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments @this, DateTime startTimeInclusive, DateTime endTimeInclusive, int maximumItems, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                @this.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            @this.SearchCompleted += handler;
            @this.SearchAsync(startTimeInclusive, endTimeInclusive, maximumItems, state);

            return tcs.Task;
        }

        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments @this, DateTime startTimeInclusive, DateTime endTimeInclusive, int maximumItems, Account account, object state)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                @this.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            @this.SearchCompleted += handler;
            @this.SearchAsync(startTimeInclusive, endTimeInclusive, maximumItems, account, state);

            return tcs.Task;
        }
    }
}
