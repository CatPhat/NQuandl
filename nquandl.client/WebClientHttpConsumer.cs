using System;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client
{

    public class WebClientHttpConsumer : IConsumeHttp
    {
        public async Task<string> DownloadStringAsync(string url, int? timeout = null, int retries = 0)
        {
            var uri = new Uri(url);
            var thisTry = 1;
            var tryLimit = retries + 1;

            var taskCompletionSource = new TaskCompletionSource<string>();
            await Task.Factory.StartNew(() =>
            {
                using (var httpClient = new HttpClient())
                {
                    if (timeout.HasValue) httpClient.Timeout = timeout.Value;

                    httpClient.DownloadDataCompleted += (sender, e) =>
                    {
                        if (e.Error != null)
                        {
                            if (thisTry++ < tryLimit)
                                ((HttpClient)sender).DownloadDataAsync(uri);
                            else
                                taskCompletionSource.SetException(e.Error);
                        }
                        else if (e.Cancelled)
                        {
                            if (thisTry++ < tryLimit)
                                ((HttpClient)sender).DownloadDataAsync(uri);
                            else
                                taskCompletionSource.SetCanceled();
                        }
                        else
                        {
                            taskCompletionSource.SetResult(Encoding.UTF8.GetString(e.Result));
                        }
                    };

                    httpClient.DownloadDataAsync(uri);
                }
            });
            return await taskCompletionSource.Task;
        }
    }
}


