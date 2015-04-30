using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models
{
    public class FRED_GDP : QuandlV1Response
    {
        public FRED_GDP() 
            : base(new QuandlCode {DatabaseCode = "FRED", TableCode = "GDP"})
        {
            
        }

        public DateTime Date { get; set; }
        public int Value { get; set; }
    }


    

}
