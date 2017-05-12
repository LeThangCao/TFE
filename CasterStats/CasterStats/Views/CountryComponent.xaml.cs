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

    
    public partial class CountryComponent : ContentPage
    {
        public CountryComponent()
        {
            InitializeComponent();
            InitCountry();
           
        }

        public async void InitCountry()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string countryId = null;
            CountryItem countryList = null;

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
                    countryId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Countries")).Id;

                }

                if (countryId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/userscity/" + countryId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    countryList = JsonConvert.DeserializeObject<CountryItem>(content);
                }



            }
            //Display the content
            SfDataGrid dataGrid = new SfDataGrid();

            CountryDataRepository countryData = new CountryDataRepository();
            if (countryList != null)
            {

                countryData.ListCountry = countryList.CountryData;

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

                dataGrid.ItemsSource = countryData.GetCountryInfo();
                dataGrid.AllowSorting = true;



            }


            Content = dataGrid; //page content = stacklayout

        }
    }

    
}
