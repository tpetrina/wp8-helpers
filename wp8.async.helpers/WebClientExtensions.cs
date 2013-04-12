using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WP8.Async.Helpers
{
    public static class WebClientExtensions
    {
        #region DownloadString

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a string result of the operation.</returns>
        public static Task<String> DownloadStringTaskAsync(this WebClient webClient, Uri address)
        {
            return webClient.DownloadStringTaskAsync(address, CancellationToken.None, null);
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and monitors cancellation requests.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a string result of the operation.</returns>
        public static Task<String> DownloadStringTaskAsync(this WebClient webClient, Uri address, CancellationToken token)
        {
            return webClient.DownloadStringTaskAsync(address, token, null);
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and reports progress.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a string result of the operation.</returns>
        public static Task<String> DownloadStringTaskAsync(this WebClient webClient, Uri address, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            return webClient.DownloadStringTaskAsync(address, CancellationToken.None, progress);
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and monitors cancellation requests, and reports progress.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a string result of the operation.</returns>
        public static Task<String> DownloadStringTaskAsync(this WebClient webClient, Uri address, CancellationToken token, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<string>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            DownloadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
            {
                progressHandler = (sender, e) => progress.Report(e);
                webClient.DownloadProgressChanged += progressHandler;
            }

            DownloadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    webClient.DownloadProgressChanged -= progressHandler;

                webClient.DownloadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            webClient.DownloadStringCompleted += handler;
            try
            {
                webClient.DownloadStringAsync(address);
            }
            catch
            {
                webClient.DownloadProgressChanged -= progressHandler;
                webClient.DownloadStringCompleted -= handler;
                throw;
            }
            return tcs.Task;
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and monitors cancellation requests, and reports progress.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a Tuple instance containing downloaded data and user-specified identifier.</returns>
        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient webClient, Uri address, object userToken)
        {
            return webClient.DownloadStringTaskAsync(address, userToken, CancellationToken.None, null);
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and monitors cancellation requests.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a Tuple instance containing downloaded data and user-specified identifier.</returns>
        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient webClient, Uri address, object userToken, CancellationToken token)
        {
            return webClient.DownloadStringTaskAsync(address, userToken, token, null);
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and reports progress.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a Tuple instance containing downloaded data and user-specified identifier.</returns>
        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient webClient, Uri address, object userToken, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            return webClient.DownloadStringTaskAsync(address, userToken, CancellationToken.None, progress);
        }

        /// <summary>
        /// Asynchronously downloads the resource as a String from the URI specified,
        /// and monitors cancellation requests, and reports progress.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to download.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a Tuple instance containing downloaded data and user-specified identifier.</returns>
        public static Task<Tuple<String, object>> DownloadStringTaskAsync(this WebClient webClient, Uri address, object userToken, CancellationToken token, IProgress<DownloadProgressChangedEventArgs> progress)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<string, object>>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            DownloadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
            {
                progressHandler = (sender, e) => progress.Report(e);
                webClient.DownloadProgressChanged += progressHandler;
            }

            DownloadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    webClient.DownloadProgressChanged -= progressHandler;
                webClient.DownloadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, e.UserState));
            };

            webClient.DownloadStringCompleted += handler;
            try
            {
                webClient.DownloadStringAsync(address, userToken);
            }
            catch
            {
                webClient.DownloadProgressChanged -= progressHandler;
                webClient.DownloadStringCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        #endregion

        #region OpenRead

        /// <summary>
        /// Opens a readable stream containing the specified resource as an asynchronous operation using a task object.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to retrieve.</param>
        /// <returns>The task object representing the asynchronous operation. The Result property on the task object returns a  System.IO.Stream open for reading.</returns>
        public static Task<Stream> OpenReadTaskAsync(this WebClient webClient, Uri address)
        {
            return webClient.OpenReadTaskAsync(address, CancellationToken.None);
        }

        /// <summary>
        /// Opens a readable stream containing the specified resource as an asynchronous operation using a task object,
        /// and monitors cancellation requests.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to retrieve.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns>A System.IO.Stream open for reading.</returns>
        public static Task<Stream> OpenReadTaskAsync(this WebClient webClient, Uri address, CancellationToken token)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Stream>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            OpenReadCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                webClient.OpenReadCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            webClient.OpenReadCompleted += handler;
            try
            {
                webClient.OpenReadAsync(address);
            }
            catch
            {
                webClient.OpenReadCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        /// <summary>
        /// Opens a readable stream containing the specified resource as an asynchronous operation using a task object.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to retrieve.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <returns></returns>
        public static Task<Tuple<Stream, object>> OpenReadTaskAsync(this WebClient webClient, Uri address, object userToken)
        {
            return webClient.OpenReadTaskAsync(address, userToken, CancellationToken.None);
        }

        /// <summary>
        /// Opens a readable stream containing the specified resource as an asynchronous operation using a task object,
        /// and monitors cancellation requests.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to retrieve.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<Tuple<Stream, object>> OpenReadTaskAsync(this WebClient webClient, Uri address, object userToken, CancellationToken token)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<Stream, object>>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            OpenReadCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                webClient.OpenReadCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, userToken));
            };

            webClient.OpenReadCompleted += handler;
            try
            {
                webClient.OpenReadAsync(address, userToken);
            }
            catch
            {
                webClient.OpenReadCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        #endregion

        #region OpenWrite

        /// <summary>
        /// Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to receive the data.</param>
        /// <returns></returns>
        public static Task<Stream> OpenWriteTaskAsync(this WebClient webClient, Uri address)
        {
            return OpenWriteTaskAsync(webClient, address, CancellationToken.None);
        }

        /// <summary>
        /// Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to receive the data.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<Stream> OpenWriteTaskAsync(this WebClient webClient, Uri address, CancellationToken token)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Stream>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            OpenWriteCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                webClient.OpenWriteCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            webClient.OpenWriteCompleted += handler;
            webClient.OpenWriteAsync(address);
            return tcs.Task;
        }

        /// <summary>
        /// Opens a stream for writing data to the specified resource, using the specified method, as an asynchronous operation using a task object.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to receive the data.</param>
        /// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<Stream> OpenWriteTaskAsync(this WebClient webClient, Uri address, string method, CancellationToken token)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Stream>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            OpenWriteCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                webClient.OpenWriteCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            webClient.OpenWriteCompleted += handler;
            try
            {
                webClient.OpenWriteAsync(address, method);
            }
            catch
            {
                webClient.OpenWriteCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        /// <summary>
        /// Opens a stream for writing data to the specified resource, using the specified method, as an asynchronous operation using a task object.
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address">The URI of the resource to receive the data.</param>
        /// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<Tuple<Stream, object>> OpenWriteTaskAsync(this WebClient webClient, Uri address, string method, object userToken, CancellationToken token)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<Stream, object>>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            OpenWriteCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                webClient.OpenWriteCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, userToken));
            };

            webClient.OpenWriteCompleted += handler;
            try
            {
                webClient.OpenWriteAsync(address, method, userToken);
            }
            catch
            {
                webClient.OpenWriteCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        #endregion

        #region UploadString

        /// <summary>
        /// Uploads the specified string to the specified resource, using the specified model
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string data)
        {
            return webClient.UploadStringTaskAsync(address, data, CancellationToken.None, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string data, CancellationToken token)
        {
            return webClient.UploadStringTaskAsync(address, data, token, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string data, IProgress<UploadProgressChangedEventArgs> progress)
        {
            return webClient.UploadStringTaskAsync(address, data, CancellationToken.None, progress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string data, CancellationToken token, IProgress<UploadProgressChangedEventArgs> progress)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<string>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            UploadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
                progressHandler = (sender, e) => progress.Report(e);

            UploadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    webClient.UploadProgressChanged -= progressHandler;
                webClient.UploadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            webClient.UploadProgressChanged += progressHandler;
            webClient.UploadStringCompleted += handler;
            try
            {
                webClient.UploadStringAsync(address, data);
            }
            catch
            {
                webClient.UploadProgressChanged -= progressHandler;
                webClient.UploadStringCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data)
        {
            return webClient.UploadStringTaskAsync(address, method, data, CancellationToken.None, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, CancellationToken token)
        {
            return webClient.UploadStringTaskAsync(address, method, data, token, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, IProgress<UploadProgressChangedEventArgs> progress)
        {
            return webClient.UploadStringTaskAsync(address, method, data, CancellationToken.None, progress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns></returns>
        public static Task<string> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, CancellationToken token, IProgress<UploadProgressChangedEventArgs> progress)
        {
            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<string>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            UploadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
                progressHandler = (sender, e) => progress.Report(e);

            UploadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    webClient.UploadProgressChanged -= progressHandler;
                webClient.UploadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            webClient.UploadProgressChanged += progressHandler;
            webClient.UploadStringCompleted += handler;
            try
            {
                webClient.UploadStringAsync(address, method, data);
            }
            catch
            {
                webClient.UploadProgressChanged -= progressHandler;
                webClient.UploadStringCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <returns></returns>
        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, object userToken)
        {
            return webClient.UploadStringTaskAsync(address, method, data, userToken, CancellationToken.None, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <returns></returns>
        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, object userToken, CancellationToken token)
        {
            return webClient.UploadStringTaskAsync(address, method, data, userToken, token, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns></returns>
        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, object userToken, IProgress<UploadProgressChangedEventArgs> progress)
        {
            return webClient.UploadStringTaskAsync(address, method, data, userToken, CancellationToken.None, progress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient">The WebClient which will be used to dowload.</param>
        /// <param name="address"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
        /// <param name="token">The token to monitor for cancellation requests. The default value is None.</param>
        /// <param name="progress">An object that receives progress updates.</param>
        /// <returns></returns>
        public static Task<Tuple<string, object>> UploadStringTaskAsync(this WebClient webClient, Uri address, string method, string data, object userToken, CancellationToken token, IProgress<UploadProgressChangedEventArgs> progress)
        {

            if (webClient == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<Tuple<string, object>>();

            if (token != CancellationToken.None)
                token.Register(() => webClient.CancelAsync());

            UploadProgressChangedEventHandler progressHandler = null;
            if (progress != null)
                progressHandler = (sender, e) => progress.Report(e);

            UploadStringCompletedEventHandler handler = null;
            handler = (sender, e) =>
            {
                if (progress != null)
                    webClient.UploadProgressChanged -= progressHandler;
                webClient.UploadStringCompleted -= handler;

                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(Tuple.Create(e.Result, userToken));
            };

            webClient.UploadProgressChanged += progressHandler;
            webClient.UploadStringCompleted += handler;
            try
            {
                webClient.UploadStringAsync(address, method, data, userToken);
            }
            catch
            {
                webClient.UploadProgressChanged -= progressHandler;
                webClient.UploadStringCompleted -= handler;
                throw;
            }

            return tcs.Task;
        }

        #endregion
    }
}
