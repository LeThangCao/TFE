using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using Akavache;
using CasterStats.Model;
using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace CasterStats.Views
{
    public partial class PlayerComponent : ContentPage
    {
        public PlayerComponent()
        {
            InitializeComponent();
            InitPlayer();




        }
        public async void InitPlayer()
        {
            //Get loginCookie with Akavache
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string playerId = null;
            List<PlayerItem> players = null;

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
                    playerId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Players")).Id;

                }

                if (playerId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/usersplayer/" + playerId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    players = JsonConvert.DeserializeObject<List<PlayerItem>>(content);
                }
            }
            SfDataGrid dataGrid = new SfDataGrid();

            PlayerDataRepository playerData = new PlayerDataRepository();
            if (players != null)
            {

                playerData.ListPlayer = players;

                dataGrid.AutoGenerateColumns = false;

                GridTextColumn pl = new GridTextColumn();
                pl.MappingName = "Player";
                pl.HeaderText = "Players";
                GridTextColumn nbUsers = new GridTextColumn();
                nbUsers.MappingName = "Count";
                nbUsers.HeaderText = "Users";



                dataGrid.Columns.Add(pl);
                dataGrid.Columns.Add(nbUsers);


                dataGrid.ColumnSizer = ColumnSizer.Auto;

                dataGrid.ItemsSource = playerData.GetPlayerInfo();
                dataGrid.AllowSorting = true;



            }


            Content = dataGrid; //page content = stacklayout


        }


    }
}
