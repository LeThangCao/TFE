using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class StreamGridDataRepository
    {
        public List<StreamGridItem> ListStreamGrid = new List<StreamGridItem>();
        public ObservableCollection<StreamGridItem> StreamGridInfo { get; set; }

        public StreamGridDataRepository()
        {
            StreamGridInfo = GetStreamGridInfo();
        }

        public ObservableCollection<StreamGridItem> GetStreamGridInfo()
        {
            var data = new ObservableCollection<StreamGridItem>();
            foreach (var s in ListStreamGrid)
            {
                Image statusIcon = new Image();
                string mb = null;
                if (s.Status == 0)
                {
                    statusIcon.Source = ImageSource.FromFile("warningIcon.jpg");
                }
                if (s.Status == 3)
                {
                    statusIcon.Source = ImageSource.FromFile("selectIcon.png");
                }
                if (s.Bandwidth >= 1000)
                {
                    mb = (s.Bandwidth / 1024f).ToString();
                    mb = mb + " mbps";

                }
                else
                {
                    mb = s.Bandwidth.ToString();
                    mb = mb + " kbps";
                }
                double percent = 0;
                if (s.Load <= 100)
                {
                    percent = s.Load / 100;
                }
                else
                {
                    percent = s.Load;
                }

                data.Add(new StreamGridItem()
                {
                   Name = s.Name,
                   Channel = s.Channel,
                   StatusIcon = statusIcon.Source,
                   Load  = percent,
                   Count = s.Count,
                   Max = s.Max,
                   Bandwid = mb,
                   PercentProgressBar = s.Load + "%"
                });
            }
            return data;
        }
    }
}
