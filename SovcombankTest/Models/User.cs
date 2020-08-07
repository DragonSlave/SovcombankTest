using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Models
{
    public class User
    {
        public long Id { get; set; }
        public string phone { get; set; }
        public bool isApproved { get; set; }
    }
}
