using Dapper;
using Eudora.Api.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public class BaseRepository<T>
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetEudoraConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString(Configurations.ConnectionStringEudora));
        }

        public async Task<IEnumerable<T>> Query(string sql)
        {
            using (var connection = GetEudoraConnection())
            {
                return await connection.QueryAsync<T>(sql);
            }
        }

        public async Task<IEnumerable<T>> Query(string sql, object sqlParams)
        {
            using (var connection = GetEudoraConnection())
            {
                return await connection.QueryAsync<T>(sql, sqlParams);
            }
        }

        public async Task<T> QueryFirstOrDefault(string sql, object sqlParams)
        {
            using (var connection = GetEudoraConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, sqlParams);
            }
        }

        public async Task Execute(string sql)
        {
            using (var connection = GetEudoraConnection())
            {
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task Execute(string sql, object sqlParams)
        {
            using (var connection = GetEudoraConnection())
            {
                await connection.ExecuteAsync(sql, sqlParams);
            }
        }

        public async Task<bool> ExecuteScalarExists(string sql, object sqlParams)
        {
            using (var connection = GetEudoraConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, sqlParams);
            }
        }
    }
}
