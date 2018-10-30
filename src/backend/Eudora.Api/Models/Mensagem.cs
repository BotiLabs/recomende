using System;
using System.ComponentModel.DataAnnotations;

namespace Eudora.Api.Models
{
    public class Mensagem
    {
        public int Id { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Content { get; set; }
        public string Sentiment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
