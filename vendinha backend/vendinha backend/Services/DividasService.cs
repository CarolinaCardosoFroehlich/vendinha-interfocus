﻿using System.ComponentModel.DataAnnotations;
using vendinha_backend.Models;
using vendinha_backend.Repository;

namespace vendinha_backend.Services
{
    public class DividasService
    {

        private readonly IRepository repository;

        public DividasService(IRepository repository)
        {
            this.repository = repository;
        }
        public bool Cadastrar(Divida divida, out List<MensagemErro> mensagens)
        {
            var valido = Validar(divida, out mensagens);
            if (valido)
            {
                try
                {
                    using var transacao = repository.IniciarTransacao();
                    repository.Commit();
                    return true;
                }
                catch (Exception)
                {
                    repository.Rollback();
                    return false;
                }
            }
            return false;
        }
        public bool Validar(Divida divida, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(divida);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(divida,
                validationContext,
                erros,
                true);
            mensagens = new List<MensagemErro>();

            var cursosNome = repository.Consultar<Divida>()
                            .Where(e => e.Id == divida.Id)
                            .Count();


            decimal total = divida.Cliente.Dividas.Sum(d => d.ValorTotal) + divida.ValorTotal;

            if (total > 200)
            {
                mensagens.Add(new MensagemErro("Dividas", "O cliente não pode dever mais de R$ 200,00."));
                validation = false;
            }
            foreach (var erro in erros)
            {
                var mensagem = new MensagemErro(
                    erro.MemberNames.First(),
                    erro.ErrorMessage);

                mensagens.Add(mensagem);
                Console.WriteLine("{0}: {1}",
                    erro.MemberNames.First(),
                    erro.ErrorMessage);
            }
            return validation;
        }
        public List<Divida> Consultar()
        {
            return repository.Consultar<Divida>()
                .OrderByDescending(c => c.Id)
                .ToList();
        }

        public List<Divida> Consultar(string pesquisa)
        {
            var resultado = repository
                .Consultar<Divida>()
                .Where(item => item.ValorTotal.ToString().Contains(pesquisa))
                .OrderByDescending(item => item.ValorTotal)
                .Take(10)
                .ToList();

            return resultado;
        }
        public Divida ConsultarPorCodigo(long id)
        {
            return repository.ConsultarPorId<Divida>(id);
        }
        public Divida Deletar(int id)
        {
            var existente = ConsultarPorCodigo(id);

            try
            {
                using var transacao = repository.IniciarTransacao();
                repository.Excluir(existente);
                repository.Commit();
                return existente;
            }
            catch (Exception)
            {
                repository.Rollback();
                return null;
            }
        }
    }
}