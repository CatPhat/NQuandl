using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;



namespace NQuandl.Generator
{
    public class ParseCSVToModel
    {
        public IEnumerable<CommodityCSVModel> GetCSVModelsFromCSVFile(string csvFilePath)
        {
            var csvRows = ParseCSVFromFileStream(csvFilePath);
            return GetCSVModelFromCSVRows(csvRows);
        }

        private static IEnumerable<string[]> ParseCSVFromFileStream(string csvFilePath)
        {
            var reader = new StreamReader(File.OpenRead(csvFilePath));
            var parser = new CsvParser(reader);
            parser.Configuration.HasHeaderRecord = true;
            parser.Configuration.Delimiter = ",";

            var rows = new List<string[]>();
            while (true)
            {
                var row = parser.Read();
                if (row == null)
                {
                    break;
                }
                rows.Add(row);
            }

            reader.Close();
            return rows;
        }


        private IEnumerable<CommodityCSVModel> GetCSVModelFromCSVRows(IEnumerable<string[]> CSVRows)
        {
            var csvRows = CSVRows.ToList();
            var headerRow = csvRows.First();

            var nameIndex = 0;
            var codeIndex = 0;
            var headerIndex = 0;

            foreach (var header in headerRow)
            {
                if (header.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                {
                    nameIndex = headerIndex;
                }
                else if (header.Equals("code", StringComparison.InvariantCultureIgnoreCase))
                {
                    codeIndex = headerIndex;
                }
                headerIndex = headerIndex + 1;
            }

            var quandlCSVModels = csvRows.Skip(1).Select(row => new CommodityCSVModel { Name = row[nameIndex], Code = row[codeIndex] }).ToList(); // skip 1 to ignore header
            return quandlCSVModels;
        }

     
    }
}
