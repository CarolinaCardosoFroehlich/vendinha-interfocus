using vendinha_backend.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vendinha_backend.Models
{
    public class Cliente : IEntidade
    {
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public virtual IList<Divida> Dividas { get; set; } = new List<Divida>();

        public virtual string GetPrintMessage()
        {
            return $"{Id} - {Nome}";
        }
    }
}

