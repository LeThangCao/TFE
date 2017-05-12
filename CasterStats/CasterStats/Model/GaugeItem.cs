using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class GaugeItem
    {
        [JsonProperty(PropertyName = "data")]
        public List<List<string>> GaugeDatas { get; set; }
 
    }
}
