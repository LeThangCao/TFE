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


    public partial class PlatFormComponent : ContentPage
    {
        public PlatFormComponent()
        {
            InitializeComponent();
            InitPlatForm();


        }

        public async void InitPlatForm()
        {
            {
                //Get loginCookie with Akavache
                var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
                string platformId = null;
                List<PlatformsItem> platforms = null;

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
                        platformId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Platforms")).Id;

                    }

                    if (platformId != null)
                    {
                        dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/usersplatform/" + platformId + "");
                        resp = await client.GetAsync(dashboardUrl);

                        content = await resp.Content.ReadAsStringAsync();



                        platforms = JsonConvert.DeserializeObject<List<PlatformsItem>>(content);
                    }
                    
                }
                SfDataGrid dataGrid = new SfDataGrid();

                PlatFormDataRepository platFormData = new PlatFormDataRepository();
                if (platforms != null)
                {

                   platFormData.ListPlatform = platforms;

                    dataGrid.AutoGenerateColumns = false;

                    GridTextColumn plPlatform = new GridTextColumn();
                    plPlatform.MappingName = "PlayerPlatForm";
                    plPlatform.HeaderText = "PlatForms";
                    GridTextColumn nbUsers = new GridTextColumn();
                    nbUsers.MappingName = "Count";
                    nbUsers.HeaderText = "Users";
                  
               

                    dataGrid.Columns.Add(plPlatform);
                    dataGrid.Columns.Add(nbUsers);


                    dataGrid.ColumnSizer = ColumnSizer.Auto;

                    dataGrid.ItemsSource = platFormData.GetPlatFormInfo();
                    dataGrid.AllowSorting = true;



                }


                Content = dataGrid; //page content = stacklayout


            }

        }
    }



}
