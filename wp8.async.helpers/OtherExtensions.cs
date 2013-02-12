using System;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WP8.Async.Helpers
{
    public static class OtherExtensions
    {
        /// <summary>
        /// Initiates the set of animations associated with the storyboard asynchronously.
        /// </summary>
        /// <param name="storyboard">A storyboard which will be executed asynchronously.</param>
        /// <returns>A task that will complete when the storyboard is finished.</returns>
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
            storyboard.Begin();
            return tcs.Task;
        }
    }
}
