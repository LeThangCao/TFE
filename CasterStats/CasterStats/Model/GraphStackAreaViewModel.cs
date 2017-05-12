using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterStats.Model
{
    public class GraphStackAreaViewModel
    {
        public List<int> Nusers = new List<int>();
        public List<DateTime> HmStream = new List<DateTime>();
        

        public ObservableCollection<GraphItem> GraphData { get; set; }

        public GraphStackAreaViewModel()
        {
            this.GraphData = GetGraphData();
        }

     

        public ObservableCollection<GraphItem> GetGraphData()
        {
            var data = new ObservableCollection<GraphItem>();
            for (var i = 0; i < Nusers.Count; i++)
            {
                data.Add(new GraphItem() { NbUsers = Nusers[i], HStream = HmStream[i]});
            }

            return data;
        }
    }
}
