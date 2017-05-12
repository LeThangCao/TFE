using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class CityDataRepository
    {
        public List<CityData> ListCity = new List<CityData>();

        public ObservableCollection<CityData> CityInfo { get; set; }

        public CityDataRepository()
        {
            CityInfo = GetCityInfo();
        }

        public ObservableCollection<CityData> GetCityInfo()
        {
            var data = new ObservableCollection<CityData>();
            foreach (CityData c in ListCity)
            {
                Image countryFlag = new Image();
                if (c.CountryCode != null)
                {
                    countryFlag.Source = ImageSource.FromFile(c.CountryCode.ToLower() + ".png");
                }
                data.Add(new CityData() {City = c.City,Count = (c.Count),
                    Country = c.Country,Flag = countryFlag.Source,Region = c.Region});
            }
            return data;
        }
    }
}
