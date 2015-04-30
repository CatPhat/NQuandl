using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models
{
   
    [DataContract]
    public class FRED_GDP : QuandlV1Response
    {
        public FRED_GDP() 
            : base(new QuandlCode {DatabaseCode = "FRED", TableCode = "GDP"})
        {
        }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
        [DataMember(Name = "value")]
        public int Value { get; set; }
    }


    

}
