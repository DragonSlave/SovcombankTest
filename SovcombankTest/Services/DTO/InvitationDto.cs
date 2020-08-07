using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Services.DTO
{
    public class InvitationDto
    {

        public string[] PhoneNumbers { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
