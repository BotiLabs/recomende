using Eudora.Api.Utils;

namespace Eudora.Api.Models
{
    public class Representante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get { return DataBuilder.Telefone(); } }
        public string Classificacao_Re { get; set; }
        public string Tipo { get; set; }
        public string Photo { get { return DataBuilder.WomanImageUrl(); } }
        public bool Contato_Realizado { get; set; }
        public string CidadeResidencial { get; set; }
        public string EstadoResidencial { get; set; }
        public string LimiteCredito { get; set; }
        public int CiclosInatividade { get; set; }
    }
}
