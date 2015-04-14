using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Generator
{
    // consolidated from https://www.quandl.com/resources/data-sources
    // fields such as dataset count and source url have been omitted
    public class QuandlDataSourceMasterList
    {
        public string DatasourceCategory { get; set; }  // "International Organizations"
        public string Name { get; set; } // "United Nations"
        public string Description { get; set; } // "Cross-country statistics on trade, energy, environment, agriculture, demography, labour, development, health and more"
        public string QuandlCode { get; set; } // "UN,UNODC" or "WORLDBANK" -- this section can contain multiple quandl codes delimited by a comma
    }
}
