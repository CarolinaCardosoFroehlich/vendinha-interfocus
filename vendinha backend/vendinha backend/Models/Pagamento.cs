using vendinha_backend.Interfaces;
using System;

namespace vendinha_backend.Models
{
    public class Pagamento : IEntidade
    {
        public long Id { get; set; }

        public long IdDivida { get; set; }

        public decimal ValorPagamento { get; set; }

        public DateTime DataPagamento { get; set; } = DateTime.Today;

        public string Descricao { get; set; } = string.Empty;

        public Divida? Divida { get; set; }
    }
}
