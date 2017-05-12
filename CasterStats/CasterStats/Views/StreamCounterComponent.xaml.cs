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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CasterStats.Views
{

    
    public partial class StreamCounterComponent : ContentPage
    {
        public StreamCounterComponent()
        {
            InitializeComponent();
            InitCounter();
        }

        public async void InitCounter()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string counterId = null;
            string imgName = null;
            CounterItem counterList = null;
            Image img = null;

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
                    counterId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Stream Counter")).Id;

                }

                if (counterId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/counter/" + counterId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    counterList = JsonConvert.DeserializeObject<CounterItem>(content);
                }
                if (counterList != null)
                {
                    foreach (var c in counterList.CounterDatas)
                    {
                       
                            var counterUrl = new Uri("http://app.casterstats.com" + c[1] + "");
                            resp = await client.GetAsync(counterUrl);
                            var byteContent = await resp.Content.ReadAsStreamAsync() ;
                            var image = ImageSource.FromStream(() => byteContent);
                            img = new Image { Source = image };
                            imgName = c[0];
                        
                    }

                }



            }
            //Display content
            StackLayout s = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center
            };
            s.Children.Add(img);
            s.Children.Add(new Label
            {
                Text = imgName,
                HorizontalOptions = LayoutOptions.Center
                
            });
            Content = s;
        }
    }

    
}
