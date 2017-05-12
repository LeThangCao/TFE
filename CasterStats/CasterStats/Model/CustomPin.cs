using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Image = Xamarin.Forms.Image;

namespace CasterStats.Model
{
    public class CustomPin
    {
        public Pin Pin { get; set; }
        public Image Img { get; set; }
        public string Url { get; set; }
        public string Country { get; set; }
    }
}
