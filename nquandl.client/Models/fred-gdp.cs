using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models
{
    public class FRED_GDP : QuandlV1Response<FRED_GDP>
    {
        public FRED_GDP() 
            : base(new QuandlCode {DatabaseCode = "FRED", TableCode = "GDP"})
        {
        }

        public string Date { get; set; }
        public double Value { get; set; }
      
    }


    public static class FRED_GDPHelper
    {
        public static FRED_GDP ConvertToType(object[] objects)
        {
            var fredGdp = new FRED_GDP
            {
                Date = objects[0].ToString(),
                Value = double.Parse(objects[1].ToString())
            };

            return fredGdp;
        }
    }

    

}
