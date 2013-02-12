using System;
using System.Threading.Tasks;
using Microsoft.Phone.UserData;

namespace WP8.Async.Helpers
{
    public static class AppointmentsExtensions
    {
        /// <summary>
        /// Asynchronously searches for appointments that occur between the specified
        /// start date and time and end date and time.
        /// </summary>
        /// <param name="appointments">The appointments which will be searched.</param>
        /// <param name="startTimeInclusive">The start date and time to use to search for appointments.</param>
        /// <param name="endTimeInclusive">The end date and time to use to search for appointments.</param>
        /// <param name="state">A user-defined object that contains information about the operation.</param>
        /// <returns>Search results.</returns>
        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments appointments, DateTime startTimeInclusive, DateTime endTimeInclusive, object state)
        {
            if (appointments == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
                {
                    appointments.SearchCompleted -= handler;
                    tcs.TrySetResult(e);
                };

            appointments.SearchCompleted += handler;
            appointments.SearchAsync(startTimeInclusive, endTimeInclusive, state);

            return tcs.Task;
        }

        /// <summary>
        /// Asynchronously searches for appointments that occur between the specified
        /// start date and time and end date and time, from the specified data source.
        /// </summary>
        /// <param name="appointments">The appointments which will be searched.</param>
        /// <param name="startTimeInclusive">The start date and time to use to search for appointments.</param>
        /// <param name="endTimeInclusive">The end date and time to use to search for appointments.</param>
        /// <param name="account">The data source to search for appointments.</param>
        /// <param name="state">A user-defined object that contains information about the operation.</param>
        /// <returns>Search results.</returns>
        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments appointments, DateTime startTimeInclusive, DateTime endTimeInclusive, Account account, object state)
        {
            if (appointments == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                appointments.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            appointments.SearchCompleted += handler;
            appointments.SearchAsync(startTimeInclusive, endTimeInclusive, account, state);

            return tcs.Task;
        }

        /// <summary>
        /// Asynchronously searches for appointments that occur between the specified
        /// start date and time and end date and time, returning no more than the specified
        /// number of appointments.
        /// </summary>
        /// <param name="appointments">The appointments which will be searched.</param>
        /// <param name="startTimeInclusive">The start date and time to use to search for appointments.</param>
        /// <param name="endTimeInclusive">The end date and time to use to search for appointments.</param>
        /// <param name="maximumItems">The maximum number of appointments to return.</param>
        /// <param name="state">A user-defined object that contains information about the operation.</param>
        /// <returns>Search results.</returns>
        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments appointments, DateTime startTimeInclusive, DateTime endTimeInclusive, int maximumItems, object state)
        {
            if (appointments == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                appointments.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            appointments.SearchCompleted += handler;
            appointments.SearchAsync(startTimeInclusive, endTimeInclusive, maximumItems, state);

            return tcs.Task;
        }

        /// <summary>
        /// Asynchronously searches for appointments that occur between the specified
        /// start date and time and end date and time, from the specified data source,
        /// returning no more than the specified number of appointments.
        /// </summary>
        /// <param name="appointments">The appointments which will be searched.</param>
        /// <param name="startTimeInclusive">The start date and time to use to search for appointments.</param>
        /// <param name="endTimeInclusive">The end date and time to use to search for appointments.</param>
        /// <param name="maximumItems">The maximum number of appointments to return.</param>
        /// <param name="account">The data source to search for appointments.</param>
        /// <param name="state"></param>
        /// <returns>Search results</returns>
        public static Task<AppointmentsSearchEventArgs> SearchTaskAsync(this Appointments appointments, DateTime startTimeInclusive, DateTime endTimeInclusive, int maximumItems, Account account, object state)
        {
            if (appointments == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<AppointmentsSearchEventArgs>();
            EventHandler<AppointmentsSearchEventArgs> handler = null;
            handler = (sender, e) =>
            {
                appointments.SearchCompleted -= handler;
                tcs.TrySetResult(e);
            };

            appointments.SearchCompleted += handler;
            appointments.SearchAsync(startTimeInclusive, endTimeInclusive, maximumItems, account, state);

            return tcs.Task;
        }
    }
}
