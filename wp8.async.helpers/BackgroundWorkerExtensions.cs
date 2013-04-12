using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace WP8.Async.Helpers
{
    public static class BackgroundWorkerExtensions
    {
        public static Task<RunWorkerCompletedEventArgs> RunWorkerTaskAsync(this BackgroundWorker backgroundWorker)
        {
            var tcs = new TaskCompletionSource<RunWorkerCompletedEventArgs>();

            RunWorkerCompletedEventHandler handler = null;
            handler = (sender, args) =>
            {
                if (args.Cancelled)
                    tcs.TrySetCanceled();
                else if (args.Error != null)
                    tcs.TrySetException(args.Error);
                else
                    tcs.TrySetResult(args);
            };

            backgroundWorker.RunWorkerCompleted += handler;
            try
            {
                backgroundWorker.RunWorkerAsync();
            }
            catch
            {
                backgroundWorker.RunWorkerCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        public static Task<RunWorkerCompletedEventArgs> RunWorkerTaskAsync(this BackgroundWorker backgroundWorker, object argument)
        {
            var tcs = new TaskCompletionSource<RunWorkerCompletedEventArgs>();

            RunWorkerCompletedEventHandler handler = null;
            handler = (sender, args) =>
            {
                if (args.Cancelled)
                    tcs.TrySetCanceled();
                else if (args.Error != null)
                    tcs.TrySetException(args.Error);
                else
                    tcs.TrySetResult(args);
            };

            backgroundWorker.RunWorkerCompleted += handler;
            try
            {
                backgroundWorker.RunWorkerAsync(argument);
            }
            catch
            {
                backgroundWorker.RunWorkerCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }
    }
}
