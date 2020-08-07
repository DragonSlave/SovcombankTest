using FluentValidation;
using SovcombankTest.Services.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SovcombankTest.Validation
{
    public class InvitationValidator: AbstractValidator<InvitationDto>
    {
        public static Regex EncodingGsm7BitSymbols = new Regex(@"^[@£$¥èéùìòÇØøÅå_ÆæßÉ!""#%&'()*+,./0123456789:;<=>? ¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà^{}\[~\]|€-]+$");

        public string GetTransliteratedMessage(string message)
        {
            var transliteratedMessage = message;

            if (!String.IsNullOrEmpty(message) && Regex.IsMatch(message, @"\p{IsCyrillic}"))
            {
                transliteratedMessage = Iuliia.IuliiaTranslator.Translate(message, Iuliia.Schemas.GOST_779);
            }
            return transliteratedMessage;
        }

        public InvitationValidator()
        {
            RuleFor(invitation => invitation.Message).NotNull().WithErrorCode("405").WithMessage("MESSAGE_EMPTY: Invite message is missing.");

            RuleForEach(invitation => invitation.PhoneNumbers).Matches(@"^7[0-9]{10}").WithErrorCode("400").WithMessage("BAD_REQUEST PHONE_NUMBERS_INVALID: One or several phone numbers do not match with international format");

            RuleFor(invitation => invitation.PhoneNumbers).NotEmpty().WithErrorCode("401").WithMessage("BAD_REQUEST PHONE_NUMBERS_EMPTY: Phone_numbers is missing");

            RuleFor(invitation => invitation.PhoneNumbers).Must(phones => phones.Length <= 16).WithErrorCode("402").WithMessage("BAD_REQUEST PHONE_NUMBERS_INVALID: Too much phone numbers, should be less or equal to 16 per request");

            RuleFor(invitation => invitation.PhoneNumbers).Must(phones => phones.Length == phones.Distinct().Count()).WithErrorCode("404").WithMessage("BAD_REQUEST PHONE_NUMBERS_INVALID: Duplicate numbers detected");

            RuleFor(invitation => invitation.Message).Must(message =>
            {
                var transliteratedMessage = GetTransliteratedMessage(message);
                if (!String.IsNullOrEmpty(message) &&
                (transliteratedMessage == message && !EncodingGsm7BitSymbols.IsMatch(message) 
                || (transliteratedMessage != message && !EncodingGsm7BitSymbols.IsMatch(transliteratedMessage))))
                {
                    return false;
                }
                return true;
            }).WithErrorCode("406").WithMessage("BAD_REQUEST MESSAGE_INVALID: Invite message should contain only characters in 7-bit GSM encoding or Cyrillic letters as well");

            RuleFor(invitation => invitation.Message).Must(message =>
            {
                var transliteratedMessage = GetTransliteratedMessage(message);
                if (!String.IsNullOrEmpty(message) && ((transliteratedMessage != message && transliteratedMessage.Length > 128) || (transliteratedMessage == message && message.Length > 160)))
                {
                    return false;
                }
                return true;
            }).WithErrorCode("407").WithMessage("BAD_REQUEST MESSAGE_INVALID: Invite message too long, should be less or equal to 128 characters of 7-bit GSM charset");
        }
    }
}
