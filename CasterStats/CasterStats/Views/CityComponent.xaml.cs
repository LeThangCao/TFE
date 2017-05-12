using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using CasterStats.Model;
using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CasterStats.Views
{


    public partial class CityComponent : ContentPage
    {
        public CityComponent()
        {
            InitializeComponent();
            InitCity();
        }

        public async void InitCity()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string cityId = null;
            CitiesItem cityList = null;

            //Set the cookiee manually (propertie 'UseCookies = false') 
            //Or use cookieContainer
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                client.DefaultRequestHeaders.Add("Cookie", idLogin);
                var dashboardUrl = new Uri("http://app.casterstats.com/dashboard");
                var resp = await client.GetAsync(dashboardUrl);

                string content = await resp.Content.ReadAsStringAsync();


                List<Dashboard> dashboards = JsonConvert.DeserializeObject<List<Dashboard>>(content);
                foreach (var dashboard in dashboards)
                {
                   cityId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Cities")).Id;

                }

                if (cityId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/userscity/" + cityId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    cityList = JsonConvert.DeserializeObject<CitiesItem>(content);
                }



            }
            //Display the content
         
            SfDataGrid dataGrid = new SfDataGrid();

            CityDataRepository cityData = new CityDataRepository();
            if (cityList != null)
            {

                cityData.ListCity = cityList.CityDatas;

                dataGrid.AutoGenerateColumns = false;

                GridImageColumn countryIcon = new GridImageColumn();
                countryIcon.MappingName = "Flag";
                countryIcon.HeaderText = "Flag";
                GridTextColumn countryName = new GridTextColumn();
                countryName.MappingName = "Country";
                countryName.HeaderText = "Country";
                GridTextColumn nbUsers = new GridTextColumn();
                nbUsers.MappingName = "Count";
                nbUsers.HeaderText = "Users";
              

                dataGrid.Columns.Add(countryIcon);
                dataGrid.Columns.Add(countryName);
                dataGrid.Columns.Add(nbUsers);
      
                dataGrid.ColumnSizer = ColumnSizer.Auto;

                dataGrid.ItemsSource = cityData.GetCityInfo();
                dataGrid.AllowSorting = true;


               
            }


            Content = dataGrid; //page content = stacklayout


        }
    }

   
}
