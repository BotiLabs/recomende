using Eudora.Api.Utils;
using System.Runtime.Serialization;

namespace Eudora.Api.Models
{
    public class CompraCategoria
    {
        public string Categoria { get; set; }
        [IgnoreDataMember]
        public string ValorTotalTxt { get; set; }
        public decimal ValorTotal { get { return DataFormatter.AdjustDecimalPlaces(ValorTotalTxt); }}
    }
}
