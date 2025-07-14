using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using vendinha_backend.Models;
using vendinha_backend.Repository;


namespace vendinha_backend.Services
{
    public class ClienteService
    {
        private readonly IRepository repository;

        public ClienteService(IRepository repository)
        {
            this.repository = repository;
        }
    }
     public bool Cadastrar(Cliente cliente, out List<MensagemErro> mensagens)
        {
            var valido = Validar(cliente, out mensagens);
            if (valido)
            {
                try
                {
                    using var transacao = repository.IniciarTransacao();
                    repository.Incluir(cliente);
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

        public static bool Validar(Cliente cliente, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(cliente);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(cliente,
                validationContext,
                erros,
                true);

            mensagens = new List<MensagemErro>();
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

            if (cliente.Divida > 2000)
            {
                mensagens.Add(new MensagemErro("divida", "o cliente não pode dever mais de 2000 R$"));
                validation = false;
            }

            return validation;
        }
    }
}