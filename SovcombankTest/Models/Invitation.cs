using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public string phone { get; set; }
        public DateTime createdon { get; set; }
        public int author { get; set; }
        
    }
}
