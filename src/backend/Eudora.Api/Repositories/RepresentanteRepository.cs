using Eudora.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public class RepresentanteRepository<T> : BaseRepository<Representante>, IRepresentanteRepository
    {
        public RepresentanteRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Representante> GetById(int id)
        {
            var sqlParams = new { id };
            return await QueryFirstOrDefault("SELECT * FROM [representantes] r left join [credito] c on c.COD_REVENDEDOR = r.id where id = @id", sqlParams);
        }

        public async Task<IEnumerable<Representante>> GetAll()
        {
            return await Query("SELECT TOP 100 * FROM [representantes] WHERE [contato_realizado] = 0 ORDER BY [tipo], [nome]");
        }

        public async Task Add(Representante representante)
        {
            string sql = "INSERT INTO [representantes] (id, nome, telefone) VALUES (@id, @nome, @telefone)";
            var sqlParams = new { id = representante.Id, nome = representante.Nome, telefone = representante.Telefone };
            await Execute(sql, sqlParams);
        }

        public async Task SetContatoRealizado(int id)
        {
            string sql = "UPDATE [representantes] SET [contato_realizado] = 1 WHERE id = @id";
            var sqlParams = new { id };
            await Execute(sql, sqlParams);
        }

        public async Task Truncate()
        {
            await Execute("TRUNCATE TABLE [representantes]");
        }
    }
}
