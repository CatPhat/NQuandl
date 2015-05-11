using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlJsonUriV2 : QuandlUriV2
    {
        public QuandlJsonUriV2(ForcedRequestOptionsV2 options)
            : base(options, ResponseFormat.JSON)
        {
        }
    }
}