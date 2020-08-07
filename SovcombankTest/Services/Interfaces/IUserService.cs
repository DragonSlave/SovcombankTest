using SovcombankTest.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Services.Interfaces
{
    public interface IUserService
    {
        int ApproveUser(string phone);        
    }
}
