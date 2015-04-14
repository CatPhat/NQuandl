using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.QuandlRequests
{
    public class QuandlMetadataV1Request : BaseQuandlRequestV1<QuandlMetadataV1Response>
    {
        public QuandlMetadataV1Request(string databaseCode, string tableCode)
        {
            if (String.IsNullOrEmpty(databaseCode)) throw new NullReferenceException("databaseCode");
            if (String.IsNullOrEmpty(tableCode)) throw new NullReferenceException("tableCode");

            _databaseCode = databaseCode.ToUpper();
            _tableCode = tableCode.ToUpper();
        }

        private readonly string _databaseCode;
        private readonly string _tableCode;

        public override string QueryCode
        {
            get { return _databaseCode + "/" + _tableCode; }
        }

        public override string Parameters
        {
            get { return "exclude_data=true"; }
        }
    }
}
