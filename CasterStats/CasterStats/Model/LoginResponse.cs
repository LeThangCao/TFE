using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterStats.Model
{
    public class LoginResponse
    {
        public UserInfo Data { get; set; }
        public string Msg { get; set; }
        public string Result { get; set; }
        public bool Success { get; set; }
    }
}
