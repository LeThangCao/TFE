using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CasterStats.Model
{
    public class CounterItem
    {
        [JsonProperty(PropertyName = "data")]
        public List<List<string>> CounterDatas { get; set; }
    }
}
