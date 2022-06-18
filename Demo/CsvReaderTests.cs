using CSVReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Dynamic;
using Dynamitey;

namespace Demo
{
    public class CsvReaderTests
    {
        public CsvReaderTests()
        {

        }
        [Fact]
        public void ReadFirstLineShouldBeOk()
        {
            CSVReaderService reader = new CSVReaderService("demo.csv", ';');
            dynamic entity = reader.FirstOrDefault();
                
            Assert.Equal("08.02.2020", entity.Data);
        }
        [Fact]
        public void ReadAllEntriesShouldBeOk()
        {
            CSVReaderService reader = new CSVReaderService("demo.csv", ';');

            IList<dynamic> expected = new List<dynamic>()
            {
                new {Data = "08.02.2020", Transaction_ID = "ab494a8f88f64affb47a7f0e3a6278f1",
                     Commodity_Currency = "CURRENCY::RUB", Account_Title = "heating",
                     Amount_With_Sym = "-1092.54 р.",Amount_Num = "-1092.54", Rate_Price = "1.0000"},
                new
                {
                    Data = "02.06.2022", Transaction_ID = "9a9907e6386745de8eecea360f9bfea6",
                     Commodity_Currency = "CURRENCY::RUB", Account_Title = "heating",
                     Amount_With_Sym = "615.41 р.",Amount_Num = "615.41",Rate_Price = "1.0000"
                }
            };          
            AssertDynamic.Equal(expected, reader);
        }          
    }
}
