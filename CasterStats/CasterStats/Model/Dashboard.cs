using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterStats.Model
{
    public class Dashboard
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<DashboardComponent> DashboardComponents { get; set; }
    }
}
