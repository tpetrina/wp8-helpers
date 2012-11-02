namespace System.Net
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public static class WebClientExtensions
    {
        #region DownloadString

        public static Task<String> DownloadStringTaskAsync(this WebClient @this, Uri address)
        {
            return @this.DownloadStringTaskAsync(address, CancellationToken.None, null);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient @this, Uri address, CancellationToken token)
        {
            return @this.DownloadStringTaskAsync(address, token, null);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient @this, Uri address, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            return @this.DownloadStringTaskAsync(address, CancellationToken.None, progress);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient @this, Uri address, CancellationToken token, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<string>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            DownloadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
            {
                progressHandler = (sender, e) => progress.Report(e);
                @this.DownloadProgressChanged += progressHandler;
            }

            DownloadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    @this.DownloadProgressChanged -= progressHandler;

                @this.DownloadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            @this.DownloadStringCompleted += handler;
            @this.DownloadStringAsync(address);
            return tcs.Task;
        }

        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient @this, Uri address, object userToken)
        {
            return @this.DownloadStringTaskAsync(address, userToken, CancellationToken.None, null);
        }

        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient @this, Uri address, object userToken, CancellationToken token)
        {
            return @this.DownloadStringTaskAsync(address, userToken, token, null);
        }

        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient @this, Uri address, object userToken, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            return @this.DownloadStringTaskAsync(address, userToken, CancellationToken.None, progress);
        }

        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient @this, Uri address, object userToken, CancellationToken token, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<string, object>>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            DownloadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
            {
                progressHandler = (sender, e) => progress.Report(e);
                @this.DownloadProgressChanged += progressHandler;
            }

            DownloadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    @this.DownloadProgressChanged -= progressHandler;
                @this.DownloadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, e.UserState));
            };

            @this.DownloadStringCompleted += handler;
            @this.DownloadStringAsync(address, userToken);
            return tcs.Task;
        }

        #endregion

        #region OpenRead

        public static Task<Stream> OpenReadTaskAsync(this WebClient @this, Uri address)
        {
            return @this.OpenReadTaskAsync(address, CancellationToken.None);
        }

        public static Task<Stream> OpenReadTaskAsync(this WebClient @this, Uri address, CancellationToken token)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Stream>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            OpenReadCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                @this.OpenReadCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            @this.OpenReadCompleted += handler;
            @this.OpenReadAsync(address);
            return tcs.Task;
        }

        public static Task<Tuple<Stream, object>> OpenReadTaskAsync(this WebClient @this, Uri address, object userToken)
        {
            return @this.OpenReadTaskAsync(address, userToken, CancellationToken.None);
        }

        public static Task<Tuple<Stream, object>> OpenReadTaskAsync(this WebClient @this, Uri address, object userToken, CancellationToken token)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<Stream, object>>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            OpenReadCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                @this.OpenReadCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, userToken));
            };

            @this.OpenReadCompleted += handler;
            @this.OpenReadAsync(address, userToken);
            return tcs.Task;
        }

        #endregion

        #region OpenWrite

        public static Task<Stream> OpenWriteTaskAsync(this WebClient @this, Uri address, CancellationToken token)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Stream>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            OpenWriteCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                @this.OpenWriteCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            @this.OpenWriteCompleted += handler;
            @this.OpenWriteAsync(address);
            return tcs.Task;
        }

        public static Task<Stream> OpenWriteTaskAsync(this WebClient @this, Uri address, string method, CancellationToken token)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Stream>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            OpenWriteCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                @this.OpenWriteCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            @this.OpenWriteCompleted += handler;
            @this.OpenWriteAsync(address, method);
            return tcs.Task;
        }

        public static Task<Tuple<Stream, object>> OpenWriteTaskAsync(this WebClient @this, Uri address, string method, object userToken, CancellationToken token)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<Stream, object>>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            OpenWriteCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                @this.OpenWriteCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, userToken));
            };

            @this.OpenWriteCompleted += handler;
            @this.OpenWriteAsync(address, method, userToken);
            return tcs.Task;
        }

        #endregion

        #region UploadString

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string data)
        {
            return @this.UploadStringTaskAsync(address, data, CancellationToken.None, null);
        }

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string data, CancellationToken token)
        {
            return @this.UploadStringTaskAsync(address, data, token, null);
        }

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string data, IProgress<UploadProgressChangedEventArgs> progress)
        {
            return @this.UploadStringTaskAsync(address, data, CancellationToken.None, progress);
        }

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string data, CancellationToken token, IProgress<UploadProgressChangedEventArgs> progress)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<string>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            UploadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
                progressHandler = (sender, e) => progress.Report(e);

            UploadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    @this.UploadProgressChanged -= progressHandler;
                @this.UploadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            @this.UploadProgressChanged += progressHandler;
            @this.UploadStringCompleted += handler;
            @this.UploadStringAsync(address, data);
            return tcs.Task;
        }


        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data)
        {
            return @this.UploadStringTaskAsync(address, method, data, CancellationToken.None, null);
        }

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, CancellationToken token)
        {
            return @this.UploadStringTaskAsync(address, method, data, token, null);
        }

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, IProgress<UploadProgressChangedEventArgs> progress)
        {
            return @this.UploadStringTaskAsync(address, method, data, CancellationToken.None, progress);
        }

        public static Task<string> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, CancellationToken token, IProgress<UploadProgressChangedEventArgs> progress)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<string>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            UploadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
                progressHandler = (sender, e) => progress.Report(e);

            UploadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    @this.UploadProgressChanged -= progressHandler;
                @this.UploadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            @this.UploadProgressChanged += progressHandler;
            @this.UploadStringCompleted += handler;
            @this.UploadStringAsync(address, method, data);
            return tcs.Task;
        }


        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, object userToken)
        {
            return @this.UploadStringTaskAsync(address, method, data, userToken, CancellationToken.None, null);
        }

        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, object userToken, CancellationToken token)
        {
            return @this.UploadStringTaskAsync(address, method, data, userToken, token, null);
        }

        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, object userToken, IProgress<UploadProgressChangedEventArgs> progress)
        {
            return @this.UploadStringTaskAsync(address, method, data, userToken, CancellationToken.None, progress);
        }

        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient @this, Uri address, string method, string data, object userToken, CancellationToken token, IProgress<UploadProgressChangedEventArgs> progress)
        {

            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<string, object>>();

            if (token != null)
                token.Register(() => @this.CancelAsync());

            UploadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
                progressHandler = (sender, e) => progress.Report(e);

            UploadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    @this.UploadProgressChanged -= progressHandler;
                @this.UploadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, userToken));
            };

            @this.UploadProgressChanged += progressHandler;
            @this.UploadStringCompleted += handler;
            @this.UploadStringAsync(address, method, data, userToken);
            return tcs.Task;
        }

        #endregion
    }
}
