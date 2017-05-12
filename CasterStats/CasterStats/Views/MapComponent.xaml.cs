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
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CasterStats.Views
{

    public partial class MapComponent : ContentPage
    {
        public MapComponent()
        {
            InitializeComponent();
            InitMap();
        }

        public async void InitMap()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string mapId = null;

            MapItem mapContent = null;


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
                    mapId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Map")).Id;

                }

                if (mapId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/map/" + mapId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    mapContent = JsonConvert.DeserializeObject<MapItem>(content);
                }


            }
            //Display content
            var customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight,
                CustomPins = new List<CustomPin>()
            };
            StackLayout stack = new StackLayout();

            if (mapContent != null)
            {
                foreach (var mapCity in mapContent.Cities)
                {

                    var pin = new CustomPin
                    {
                        Pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(Convert.ToDouble(mapCity[1]), Convert.ToDouble(mapCity[2])),
                            Label = mapCity[0] + ": " + mapCity[3] + " user(s)"
                        },
                        Country = mapCity[4]
                    };
                    if (mapCity[4] != null)
                    {
                        pin.Img = new Image
                        {
                            Source = mapCity[4].ToLower() + ".png"
                        };
                    }
                    else
                    {
                        
                    }

                    customMap.CustomPins.Add(pin);
                    customMap.Pins.Add(pin.Pin);
                }
            }


            stack.Children.Add(customMap);
            Content = stack;
        }

    }
}



