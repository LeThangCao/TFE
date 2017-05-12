using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using Org.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CasterStats.Views
{

  
    public partial class DashboardManagement : ContentPage
    {
        public DashboardManagement()
        {
            InitializeComponent();
         
        }

        private void HomeOnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new HomePage());
        }

        private async void NewDashboardClicked(object sender, EventArgs e)
        {
            var idLogin =  await BlobCache.LocalMachine.GetObject<string>("loginCookie");
          

            //Set the cookiee manually (propertie 'UseCookies = false') 
            //Or use cookieContainer
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                client.DefaultRequestHeaders.Add("Cookie", idLogin);
                var dashboardUrl = new Uri("http://app.casterstats.com/dashboard");
               
                var j = new
                {
                    Name = "Test",
                    Configuration = "{'layout':'3columns'}",
                    Startup = "false"
                };
                var content = JsonConvert.SerializeObject(j);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var resp =  client.PutAsync(dashboardUrl,httpContent).Result;

                var respContent = await resp.Content.ReadAsStringAsync();

                var idDashboard = JsonConvert.DeserializeObject<CreateDashboard>(respContent);

                //var id = Guid.NewGuid();





            }
        }
    }

   
}
