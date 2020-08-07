using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Services.Interfaces
{
    public interface IInvitationService
    {
        int GetInvitationsCountPerApiId(int apiId);

        Task SendInvites(string[] phones, int userId);

        Task<IEnumerable<string>> CheckDuplicatePhones(string[] phones);

    }
}
