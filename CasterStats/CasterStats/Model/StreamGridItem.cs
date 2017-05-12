using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class StreamGridItem
    {
        public string Name { get; set; }
        public string Channel { get; set; }
        public int Status { get; set; }
        public double Load { get; set; }
        public int Count { get; set; }
        public int  Max { get; set; }
        public int Bandwidth { get; set; }
        public ImageSource StatusIcon { get; set; }
        public string Bandwid { get; set; }
        public string PercentProgressBar { get; set; }
    }
}
