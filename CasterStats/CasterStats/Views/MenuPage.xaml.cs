using System;
using Xamarin.Forms;

namespace CasterStats.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();

            IsPresented = false;
           
            
        }
        private  void Player_Cliked(object sender, EventArgs e)
        {
           

            Detail =  new NavigationPage(new PlayerComponent());

            IsPresented = false;


        }

        private void Platforms_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PlatFormComponent());
            IsPresented = false;
        }

        private void StreamGrid_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new StreamGridComponent());
            IsPresented = false;
        }
        private void Cities_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new CityComponent());
            IsPresented = false;
        }
        private void Country_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new CountryComponent());
            IsPresented = false;
        }
        private void StreamCounter_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new StreamCounterComponent());
            IsPresented = false;
        }
        private void StreamGauge_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new StreamGaugeComponent());
            IsPresented = false;
        }
        private void Graph_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new GraphComponent());
            IsPresented = false;
        }
        private void Map_Cliked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MapComponent());
            IsPresented = false;
        }

    }
}
