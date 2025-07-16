using vendinha_backend.Enums;
using vendinha_backend.Interfaces;
using vendinha_backend.Services;
using System;

namespace vendinha_backend.Models
{
    public class Divida : IEntidade
    {
        public long Id { get; set; }

        public long IdCliente { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal ValorTotal { get; set; }

        public decimal ValorPago { get; set; } = 0;

        public DateTime DataCriacao { get; set; } = DateTime.Today;

        public DateTime? DataPagamento { get; set; }

        public Situacao Situacao { get; set; } = Situacao.Pendente;

        public Cliente? Cliente { get; set; }
    }
}
