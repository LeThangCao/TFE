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

    public partial class StreamGaugeComponent : ContentPage
    {
        public StreamGaugeComponent()
        {
            InitializeComponent();
            InitGauge();

        }

        public async void InitGauge()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string gaugeId = null;
            List<Image> imgList = new List<Image>();
            GaugeItem gaugeList = null;


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
                    gaugeId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Stream Gauges")).Id;

                }

                if (gaugeId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/gauge/" + gaugeId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    gaugeList = JsonConvert.DeserializeObject<GaugeItem>(content);
                }
                if (gaugeList != null)
                {
                    foreach (var c in gaugeList.GaugeDatas)
                    {

                        var counterUrl = new Uri("http://app.casterstats.com" + c[1] + "");
                        resp = await client.GetAsync(counterUrl);
                        var byteContent = await resp.Content.ReadAsStreamAsync();
                        var image = ImageSource.FromStream(() => byteContent);
                        imgList.Add(new Image
                        {
                            Source = image
                        });


                    }

                }

            }
            //Display content
            StackLayout s = new StackLayout();
            Grid g = new Grid();
            ScrollView sc = new ScrollView();

            g.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition() { Width = GridLength.Auto},
                new ColumnDefinition() { Width = GridLength.Auto}
            };
            g.RowDefinitions = new RowDefinitionCollection();
            var row = -1;
            for (var i = 0; i < imgList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    g.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(120, GridUnitType.Absolute)});
                    row++;
                    g.Children.Add(imgList[i], 0, row);
                }
                else
                {
                    g.Children.Add(imgList[i], 1, row);
                }
            }


            sc.Content = g;//content in scrollview
            s.Children.Add(sc);// add content scrollview in stacklayout


            Content = s;

        }
    }
}
