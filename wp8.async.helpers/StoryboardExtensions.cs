using System;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WP8.Async.Helpers
{
    public static class StoryboardExtensions
    {
        /// <summary>
        /// Asynchronously initiates the set of animations associated with the storyboard.
        /// </summary>
        /// <param name="storyboard">A storyboard which will be executed asynchronously.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static Task BeginAsync(this Storyboard storyboard)
        {
            if (storyboard == null)
                throw new NullReferenceException();

            // it doesn't really matter which type is used here.
            var tcs = new TaskCompletionSource<bool>();
            EventHandler handler = null;
            handler = (sender, e) =>
            {
                storyboard.Completed -= handler;
                tcs.TrySetResult(true);
            };

            storyboard.Completed += handler;
            try
            {
                storyboard.Begin();
            }
            catch
            {
                storyboard.Completed -= handler;
                throw;
            }

            return tcs.Task;
        }
    }
}
