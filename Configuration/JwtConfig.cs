using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Configuration
{
    public class JwtConfig
    {
        public string Token { get; set; }
        public string Secret { get; set; }
    }
}
