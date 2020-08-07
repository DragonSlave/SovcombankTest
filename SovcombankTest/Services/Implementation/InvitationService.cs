using Microsoft.EntityFrameworkCore;
using Npgsql;
using SovcombankTest.DB;
using SovcombankTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Services.Implementation
{
    public class InvitationService : IInvitationService
    {
        private readonly MessageDbContext _messageDbContext;

        public InvitationService(MessageDbContext ctx)
        {
            _messageDbContext = ctx;
        }

        public async Task<IEnumerable<string>> CheckDuplicatePhones(string[] phones)
        {
            var duplicatesPhoneNumbers = new List<string>();
            foreach (var phoneNum in phones)
            {
                var foundPhoneNum = await _messageDbContext.Invitation.FirstOrDefaultAsync(x => x.phone == phoneNum);
                if (foundPhoneNum != null)
                {
                    duplicatesPhoneNumbers.Add(phoneNum);
                }
            }
            return duplicatesPhoneNumbers;
        }

        public int GetInvitationsCountPerApiId(int apiId = 4)
        {
            int result;
            using (var cmd = _messageDbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "public.getcountinvitations";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                var apiIdParam = new NpgsqlParameter("apiid", NpgsqlTypes.NpgsqlDbType.Integer);
                apiIdParam.Value = apiId;
                cmd.Parameters.Add(apiIdParam);

                result = (int)cmd.ExecuteScalar();
                
            }
            return result;
        }

        public async Task SendInvites(string[] phones, int userId = 7)
        {
            var userIdParam = new NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Integer);
            userIdParam.Value = userId;
            var phoneListParam = new NpgsqlParameter("@phones", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text);
            phoneListParam.Value = phones;

            await _messageDbContext.Database.ExecuteSqlRawAsync("SELECT invite(@user_id, @phones)", userIdParam, phoneListParam);
        }
    }
}
