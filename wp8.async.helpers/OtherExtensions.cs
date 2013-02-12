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
        /// <param name="this"></param>
        /// <returns>A task that will complete when the storyboard is finished.</returns>
        public static Task BeginAsync(this Storyboard @this)
        {
            if (@this == null)
                throw new NullReferenceException();

            // it doesn't really matter which type is used here.
            var tcs = new TaskCompletionSource<bool>();
            EventHandler handler = null;
            handler = (sender, e) =>
                {
                    @this.Completed -= handler;
                    tcs.TrySetResult(true);
                };
            @this.Completed += handler;
            @this.Begin();
            return tcs.Task;
        }
    }
}
