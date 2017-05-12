using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CasterStats.Views
{


    public partial class CarouselComponent : CarouselPage
    {
        public ObservableCollection<string> ListItemCollection;
        public ObservableCollection<ContentPage> ListContentPage = new ObservableCollection<ContentPage>();
        public CarouselComponent(ObservableCollection<string> listItem )
        {
            InitializeComponent();
            ListItemCollection = listItem;
            InitCareouselPage();
         
        }

        public void InitCareouselPage()
        {
            foreach (var listItem in ListItemCollection)
            {
                if (listItem.Equals("Players"))
                {
                    var playerComponent = new PlayerComponent();
                    ListContentPage.Add(playerComponent);
                    
                }
                if (listItem.Equals("PlatForms"))
                {
                    var platFormComponent = new PlatFormComponent();
                    ListContentPage.Add(platFormComponent);
                }
                if (listItem.Equals("Graph"))
                {
                    var graphComponent = new GraphComponent();
                    ListContentPage.Add(graphComponent);
                }
                if (listItem.Equals("Map"))
                {
                    var mapComponent = new MapComponent();
                    ListContentPage.Add(mapComponent);
                }
                if (listItem.Equals("Stream Grid"))
                {
                    var streamGridComponent = new StreamGridComponent();
                    ListContentPage.Add(streamGridComponent);
                }
                if (listItem.Equals("Stream Counter"))
                {
                    var counterComponent = new StreamCounterComponent();
                    ListContentPage.Add(counterComponent);
                }
                if (listItem.Equals("Stream Gauges"))
                {
                    var gaugesComponent = new StreamGaugeComponent();
                    ListContentPage.Add(gaugesComponent);
                }
                if (listItem.Equals("Cities"))
                {
                    var citiesComponent = new CityComponent();
                    ListContentPage.Add(citiesComponent);
                }
                if (listItem.Equals("Countries"))
                {
                    var countryComponent = new CountryComponent();
                    ListContentPage.Add(countryComponent);
                }
            }
            foreach (var contentPage in ListContentPage)
            {
                Children.Add(contentPage);
            }
            
        }
        
    }
}
