using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Services.DTO
{
    public class UserDto
    {
        public long? Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsApproved { get; set; }
    }
}
