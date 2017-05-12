using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CasterStats.Model
{
    public class CitiesItem 
    {
        [JsonProperty(PropertyName = "data")]
        public List<CityData> CityDatas { get; set; }

       
    }
}
