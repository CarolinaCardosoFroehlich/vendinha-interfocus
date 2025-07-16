using vendinha_backend.Interfaces;
using System;
using vendinha_backend.Services;

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

        public Cliente Cliente { get; set; }

        public decimal ValorTotal
        {
            get
            {
                return Cliente?.Dividas?.Sum(d => d?.ValorTotal ?? 0) ?? 0;
            }
        }
    }
}
