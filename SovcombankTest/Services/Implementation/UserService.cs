using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using SovcombankTest.DB;
using SovcombankTest.Services.DTO;
using SovcombankTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovcombankTest.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly MessageDbContext _messageDbContext;

        public UserService(MessageDbContext ctx)
        {
            _messageDbContext = ctx;
        }

        public int ApproveUser(string phone)
        {
            
            var phoneParam = new NpgsqlParameter("@userphone", NpgsqlTypes.NpgsqlDbType.Text);
            phoneParam.Value = phone;

            int result = 0;
            using (var cmd = _messageDbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "public.approve_user";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.Add(phoneParam);
                var res = cmd.ExecuteScalar();
                if (res != null && res!= DBNull.Value)
                {
                    result = (int)res;
                }
            }
            return result;

        }
    }
}
