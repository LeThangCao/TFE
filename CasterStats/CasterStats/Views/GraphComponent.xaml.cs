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
using Syncfusion.SfChart.XForms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CasterStats.Views
{


    public partial class GraphComponent : ContentPage
    {
        public GraphComponent()
        {
            InitializeComponent();
            InitGraph();
        }

        public async void InitGraph()
        {
            var idLogin = await BlobCache.LocalMachine.GetObject<string>("loginCookie");
            string graphId = null;
            GraphItem graphContent = null;


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
                    graphId = dashboard.DashboardComponents.FirstOrDefault(x => x.Name.Equals("Graph")).Id;

                }

                if (graphId != null)
                {
                    dashboardUrl = new Uri("http://app.casterstats.com/dashboard/component/graph/" + graphId + "");
                    resp = await client.GetAsync(dashboardUrl);

                    content = await resp.Content.ReadAsStringAsync();



                    graphContent = JsonConvert.DeserializeObject<GraphItem>(content);
                }

            }
            //Display content
            SfChart chart = new SfChart();

            DateTimeAxis timeAxis = new DateTimeAxis()
            {   
               
                Interval = 30,
                IntervalType = DateTimeIntervalType.Minutes
                
            };
            timeAxis.LabelStyle.LabelFormat = "HH:mm";
            chart.PrimaryAxis = timeAxis;

            
            NumericalAxis userAxis = new NumericalAxis();
            userAxis.Title.Text = "Users";
            chart.SecondaryAxis = userAxis;

            if (graphContent != null)
            {
                //if (graphContent.NbUsers == null &&  graphContent.HStream == null)
                //{
                //    graphContent.NbUsers = new List<int>();
                //    graphContent.HStream = new List<DateTime>();
                //}
                //;

                var nbStream = graphContent.GraphDatas[0].Count;
                nbStream = nbStream - 1;
                var idUser = 1;
                var idDate = 0;
                

                for (var s = 0; s < nbStream; s++)
                { 
                    var data = new GraphStackAreaViewModel();
                    var graphData = 0;
                    foreach (var t in graphContent.GraphDatas)
                    {
                        if (t[idDate] == null || t[idUser] == null)
                            continue;

                        data.Nusers.Add(int.Parse(t[idUser]));

                        var dt = DateTime.Parse(t[idDate]);
                        data.HmStream.Add(dt);
                        graphData++;
                    }
                    if (graphData > 0)
                    {
                        var splineArea = new StackingAreaSeries
                        {
                            BindingContext = graphData,
                            ItemsSource = data.GetGraphData(),
                            Label = graphContent.Streams[idUser - 1],
                            XBindingPath = "HStream",
                            YBindingPath = "NbUsers",
                            EnableTooltip = true
                        };

                        chart.Series.Add(splineArea);
                        chart.Legend = new ChartLegend();
                        var tool = new ChartTooltipBehavior
                        {
                            BackgroundColor = Color.White,
                            TextColor = Color.Black,
                            Duration = 5
                        };
                        chart.ChartBehaviors.Add(tool);
                    }
                    idUser++;
                }
            }

            //var chart = new RadCartesianChart
            //{
            //    HorizontalAxis = new DateTimeContinuousAxis
            //    {
            //        LabelFormat = "HH:mm",
            //        LabelFitMode = AxisLabelFitMode.MultiLine,
            //        MajorStepUnit = TimeInterval.Minute,
            //        MajorStep = 30
            //    },
            //    VerticalAxis = new NumericalAxis(),
            //    BindingContext = new GraphSplineAreaViewModel()
            //};

            //var series = new SplineAreaSeries();
            //series.SetBinding(ChartSeries.ItemsSourceProperty, new Binding("GraphData"));
            //series.ValueBinding = new PropertyNameDataPointBinding("NbUsers");
            //series.CategoryBinding = new PropertyNameDataPointBinding("HStream");

            //chart.Series.Add(series);


            //this.Content = chart;
       
           
            this.Content = chart;
            
        }
    }


}
