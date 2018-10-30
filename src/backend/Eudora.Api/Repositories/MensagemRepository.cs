using Eudora.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public class MensagemRepository<T> : BaseRepository<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Mensagem>> GetAll()
        {
            return await Query("SELECT * FROM [mensagens] ORDER BY ID");
        }

        public async Task Add(Mensagem mensagem)
        {
            string sql = "INSERT INTO [mensagens] ([from], [to], [content], [sentiment], [date]) VALUES (@from, @to, @content, @sentiment, @date)";
            var sqlParams = new { from = mensagem.From, to = mensagem.To, content = mensagem.Content, sentiment = mensagem.Sentiment, date = mensagem.Date };
            await Execute(sql, sqlParams);
        }
    }
}
