using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CasterStats.Model
{
    public class GraphItem
    {
        [JsonProperty(PropertyName = "data")]
        public List<List<string>> GraphDatas { get; set; }
        public List<string> Streams { get; set; }
        public int NbUsers { get; set; }
        public DateTime HStream { get; set; }
    }
}
