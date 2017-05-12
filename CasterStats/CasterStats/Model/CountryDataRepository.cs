using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class CountryDataRepository
    {
        public List<CountryData> ListCountry = new List<CountryData>();

        public ObservableCollection<CountryData> CityInfo { get; set; }

        public CountryDataRepository()
        {
            CityInfo = GetCountryInfo();
        }

        public ObservableCollection<CountryData> GetCountryInfo()
        {
            var data = new ObservableCollection<CountryData>();
            foreach (CountryData c in ListCountry)
            {
                Image countryFlag = new Image();
                if (c.CountryCode != null)
                {
                    countryFlag.Source = ImageSource.FromFile(c.CountryCode.ToLower() + ".png");
                }
                data.Add(new CountryData()
                {
                    
                    Count = (c.Count),
                    Country = c.Country,
                    Flag = countryFlag.Source,
                    
                });
            }
            return data;
        }
    }
}
