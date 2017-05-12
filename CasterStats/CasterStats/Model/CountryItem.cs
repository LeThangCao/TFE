using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CasterStats.Model
{
    public class CountryItem
    {
        [JsonProperty(PropertyName = "data")]
        public List<CountryData> CountryData{ get; set; }
    }
}
