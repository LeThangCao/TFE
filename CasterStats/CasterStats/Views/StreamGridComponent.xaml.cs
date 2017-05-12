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
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Akavache;
using CasterStats.Model;
using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataTemplate = Xamarin.Forms.DataTemplate;


namespace CasterStats.Views
{


    public partial class StreamGridComponent : ContentPage
    {
        public StreamGridComponent()
        {
            InitializeComponent();
            InitStreamGrid();

        }

        public async void InitStreamGrid()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string streamGridId = null;
            List<StreamGridItem> streamGrid = null;

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
                    streamGridId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Stream Grid")).Id;

                }

                if (streamGridId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/streamgrid/" + streamGridId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    streamGrid = JsonConvert.DeserializeObject<List<StreamGridItem>>(content);
                }



            }
            //Display the content
            Grid innerG = new Grid();

            SfDataGrid dataGrid = new SfDataGrid();

            StreamGridDataRepository streamGridData = new StreamGridDataRepository();
            if ( streamGrid != null)
            {

                streamGridData.ListStreamGrid = streamGrid;

                dataGrid.AutoGenerateColumns = false;

                GridTextColumn name = new GridTextColumn();
                name.MappingName = "Name";
                name.HeaderText = "Name";
                GridTextColumn channel = new GridTextColumn();
                channel.MappingName = "Channel";
                channel.HeaderText = "Channel";
                GridImageColumn status = new GridImageColumn();
                status.MappingName = "StatusIcon";
                status.HeaderText = "Status";
                GridTemplateColumn load = new GridTemplateColumn();
                load.MappingName = "ProgressBarDisplay";
                load.CellTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid();

                    var progressBar = new ProgressBar()
                    {
                        HeightRequest = 100,
                        WidthRequest  = 50
                    };
                    progressBar.SetBinding(ProgressBar.ProgressProperty,"Load");

                    Label label = new Label();
                    
                    label.SetBinding(Label.TextProperty,"PercentProgressBar");
                    grid.Children.Add(progressBar);
                    grid.Children.Add(label);
                    return new ViewCell() { View = grid };
                });
              
             
                load.HeaderText = "Load";
                GridTextColumn  count = new GridTextColumn();
                count.MappingName = "Count";
                count.HeaderText = "Users";
                GridTextColumn max = new GridTextColumn();
                max.MappingName = "Max";
                max.HeaderText = "Max";
                GridTextColumn bandwidth = new GridTextColumn();
                bandwidth.MappingName = "Bandwid";
                bandwidth.HeaderText = "Bandwidth";


                dataGrid.Columns.Add(name);
                dataGrid.Columns.Add(channel);
                dataGrid.Columns.Add(status);
                dataGrid.Columns.Add(load);
                dataGrid.Columns.Add(count);
                dataGrid.Columns.Add(max);
                dataGrid.Columns.Add(bandwidth);


                dataGrid.ColumnSizer = ColumnSizer.Auto;

                dataGrid.ItemsSource = streamGridData.GetStreamGridInfo();
                dataGrid.AllowSorting = true;



            }


            Content = dataGrid;


            //create the columns for the header


            //header.ColumnDefinitions = new ColumnDefinitionCollection
            //{
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() { Width = new GridLength(120,GridUnitType.Absolute)}
            //};

            //header.RowDefinitions = new RowDefinitionCollection
            //{
            //            new RowDefinition() {Height = new GridLength(60,GridUnitType.Absolute)}

            //};
            ////Create frame for header
            //var name = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Name",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            //var channel = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Channel",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            //var status = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Status",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            //var load = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Load %",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            //var user = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Users",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            //var max = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Max",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            //var bandwidth = new Frame
            //{
            //    OutlineColor = Color.Black,
            //    Content = new Label
            //    {
            //        Text = "Bandwidth",
            //        HorizontalOptions = LayoutOptions.Start,
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    }

            //};

            ////add frame to header
            //header.Children.Add(name, 0, 0);
            //header.Children.Add(channel, 1, 0);
            //header.Children.Add(status, 2, 0);
            //header.Children.Add(load, 3, 0);
            //header.Children.Add(user, 4, 0);
            //header.Children.Add(max, 5, 0);
            //header.Children.Add(bandwidth, 6, 0);


            ////create columns for the rows
            //innerG.ColumnDefinitions = new ColumnDefinitionCollection()
            //{
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)},
            //            new ColumnDefinition() {  Width = new GridLength(120,GridUnitType.Absolute)}


            //};

            //innerG.RowDefinitions = new RowDefinitionCollection();
            ////create a new row for each object in playerItem
            //if (streamGrid != null)
            //{
            //    for (int i = 0; i < streamGrid.Count; i++)
            //    {
            //        Image icon = new Image();
                  
            //        innerG.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35, GridUnitType.Absolute) });
            //        innerG.Children.Add(new Label() { Text = streamGrid[i].Name, HorizontalOptions = LayoutOptions.Start }, 0, i);
            //        innerG.Children.Add(new Label() { Text = streamGrid[i].Channel, HorizontalOptions = LayoutOptions.Start }, 1, i);
            //        if (streamGrid[i].Status == 0)
            //        {

            //            icon.Source = ImageSource.FromFile("warningIcon.jpg");
            //            innerG.Children.Add(icon, 2, i);

            //            innerG.Children.Add(new Label()
            //            {
            //                Text = "Unknown",
            //                HorizontalOptions = LayoutOptions.End
            //            }, 2, i);
            //        }
            //        if (streamGrid[i].Status == 3)
            //        {
            //            icon.Source = ImageSource.FromFile("selectIcon.png");
            //            innerG.Children.Add(icon, 2, i);
            //            innerG.Children.Add(new Label()
            //            {
            //                Text = "Up",
            //                HorizontalOptions = LayoutOptions.End
            //            }, 2, i);
            //        }

            //        innerG.Children.Add(new ProgressBar()
            //        {
            //            Progress = streamGrid[i].Load,
            //            WidthRequest = 100,
            //            HeightRequest = 100
                        
            //        }, 3, i);
            //        innerG.Children.Add(new Label() { Text = streamGrid[i].Count.ToString(), HorizontalOptions = LayoutOptions.Start }, 4, i);
            //        innerG.Children.Add(new Label() { Text = streamGrid[i].Max.ToString(), HorizontalOptions = LayoutOptions.Start }, 5, i);
            //        if (streamGrid[i].Bandwidth >= 1000)
            //        {
            //            double mb = streamGrid[i].Bandwidth / 1024f;
            //            innerG.Children.Add(
            //                new Label()
            //                {
            //                    Text = mb + " mbps",
            //                    HorizontalOptions = LayoutOptions.Start
            //                }, 6, i);
            //        }
            //        else
            //        {
            //            innerG.Children.Add(
            //                new Label()
            //                {
            //                    Text = streamGrid[i].Bandwidth + " kbps",
            //                    HorizontalOptions = LayoutOptions.Start
            //                }, 6, i);

            //        }
            //    }
            //}

            //sc.Scrolled += (object sender, ScrolledEventArgs e) =>
            //{
            //    scHeadInner.ScrollToAsync(e.ScrollX, 0, false);
            //};

            //scHeadInner.Content = header;
            //sc.Content = innerG;
            //s.Children.Add(scHeadInner); //header grid view in stackLayout
            //s.Children.Add(sc); // scrollview in stackLayout


            //Content = s; //page content = stacklayout

        }
        
    }


}
