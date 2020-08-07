using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Logging;
using Npgsql;
using SovcombankTest.DB;
using SovcombankTest.Services.DTO;
using SovcombankTest.Services.Interfaces;
using SovcombankTest.Validation;

namespace SovcombankTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvitationController : ControllerBase
    {        
        private readonly ILogger<InvitationController> _logger;
        private readonly IInvitationService _invitationService;

        public InvitationController(ILogger<InvitationController> logger, IInvitationService invitationService)
        {
            _logger = logger;
            _invitationService = invitationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InvitationDto invitationDto)
        {
            var validator = new InvitationValidator();
            var result = validator.Validate(invitationDto);
            if (result.Errors.Count > 0)
            {
                return new BadRequestObjectResult(new {
                    ErrorCode = result.Errors.First().ErrorCode,
                    Message = result.Errors.First().ErrorMessage
                });
            }

            try
            {
                var invitationsCountSent = _invitationService.GetInvitationsCountPerApiId(4);

                if (invitationsCountSent + invitationDto.PhoneNumbers.Length > 128)
                {
                    return new BadRequestObjectResult( new
                    {
                        ErrorCode = "403",
                        ErrorMessage = "BAD_REQUEST PHONE_NUMBERS_INVALID: Too much phone numbers, should be less or equal to 128 per day"
                    });
                }

                var duplicatesPhoneNumbers = await _invitationService.CheckDuplicatePhones(invitationDto.PhoneNumbers);
                
                if (duplicatesPhoneNumbers.Count() > 0)
                {
                    invitationDto.PhoneNumbers = invitationDto.PhoneNumbers.Except(duplicatesPhoneNumbers).ToArray();
                }

                await _invitationService.SendInvites(invitationDto.PhoneNumbers, 7);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "INTERNAL " + ex.Source + ": "+ ex.Message);
            }
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
