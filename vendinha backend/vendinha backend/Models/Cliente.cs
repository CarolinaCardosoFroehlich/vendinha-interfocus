using vendinha_backend.Interfaces;
using System;
using vendinha_backend.Controllers;
using vendinha_backend.Services;

namespace vendinha_backend.Models
{
    public class Cliente : IEntidade
    {
        public long Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public string? Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public virtual IList<Divida> Dividas { get; set; } = new List<Divida>();
    }
}
