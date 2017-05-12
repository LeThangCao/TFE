using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Android.Content;
using Android.Media;
using CasterStats.Model;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CasterStats.Views
{
    public partial class HomePage : ContentPage
    {
        public static ObservableCollection<string> ComponentList { get; set; }
      
        public HomePage()
        {
            InitializeComponent();
            InitHomePage();
        }

        public  void InitHomePage()
        {
            
            StackLayout s = new StackLayout();
            var listView = new ListView();
            var buttonCreateCarrousel = new Button()
            {
                Text = "Create",
            };
            buttonCreateCarrousel.Clicked += OnclickedButtonCreate;
            ComponentList= new ObservableCollection<string>();
            listView.ItemsSource = ComponentList;
          
         

            var picker = new Picker
            {
                Title = "Select a component to display"
            };
            picker.Items.Add("Players");
            picker.Items.Add("PlatForms");
            picker.Items.Add("Graph");
            picker.Items.Add("Map");
            picker.Items.Add("Stream Grid");
            picker.Items.Add("Stream Counter");
            picker.Items.Add("Stream Gauges");
            picker.Items.Add("Cities");
            picker.Items.Add("Countries");

            picker.SelectedIndexChanged += OnClicked;

            listView.ItemSelected += OnSelection;


            var temp = new DataTemplate(typeof(ViewCellModelComponent));
            listView.ItemTemplate = temp;


           

            listView.IsPullToRefreshEnabled = true;

            s.Children.Add(picker);
            s.Children.Add(listView);
            s.Children.Add(buttonCreateCarrousel);
         
       
            Content = s;


        }

        public void OnclickedButtonCreate(object sender, EventArgs e)
        {
             Navigation.PushModalAsync(new CarouselComponent(ComponentList));
        }

        public void OnClicked(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker?.SelectedIndex == -1) return;
            var find = false;
            foreach (var compL in ComponentList)
            {
                if (picker != null && picker.Items[picker.SelectedIndex].Equals(compL))
                {
                    find = true;
                }
                   
            }
            if (find == false)
            {
                ComponentList.Add(picker?.Items[picker.SelectedIndex]);
            }
        }

        public void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
         
            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

   


    }
}
