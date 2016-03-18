using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NQuandl.Api.Quandl
{
    public interface IHttpClient
    {


        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.Uri"/>.The base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </returns>
        Uri BaseAddress {  get;  set; }

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before the request times out.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.TimeSpan"/>.The number of milliseconds to wait before the request times out.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite"/>.</exception><exception cref="T:System.InvalidOperationException">An operation has already been started on the current instance. </exception><exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
        TimeSpan Timeout {  get;  set; }


        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="requestUri">The Uri the request is sent to.</param><exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        Task<HttpResponseMessage> GetAsync(string requestUri);
        
    }
}