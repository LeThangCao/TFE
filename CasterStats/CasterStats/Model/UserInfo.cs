using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterStats.Model
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Gravatar { get; set; }
        public string Roles { get; set; }
        public bool AnyEditable { get; set; }
        public bool Demo { get; set; }
        public ConfigurationData Configuration { get; set; }
    }
}
