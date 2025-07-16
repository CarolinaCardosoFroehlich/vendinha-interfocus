using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using vendinha_backend.Models;
using vendinha_backend.Repository;


namespace vendinha_backend.Services
{
    public class ClienteService
    {
        private readonly IRepository repository;

        //cadastrar
        public ClienteService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Cadastrar(Cliente cliente, out List<MensagemErro> mensagens)
        {
            var valido = Validar(cliente, out mensagens);
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

        //validar
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

            if (cliente.Dividas.Sum(d => d.ValorTotal) > 200)
            {
                mensagens.Add(new MensagemErro("Dividas", "O cliente não pode dever mais de R$ 200,00."));
                validation = false;
            }

            //throw new Exception("dados invalidos!!!!");
            return validation;
        }

        //consultar
        public List<Cliente> ConsultarClientesOrdenadosPorDivida()
        {
            return repository.Consultar<Cliente>()
                .Where(c => c.Dividas != null && c.Dividas.Any()) 
                .OrderByDescending(c => c.Dividas.Sum(d => d.ValorTotal))
                .ToList();
        }

        public List<Cliente> Consultar(string pesquisa)
        {
            // lambda expression
            var resultado2 = repository
                .Consultar<Cliente>()
                .Where(item => item.Nome.Contains(pesquisa))
                .OrderBy(item => item.Nome)
                .Take(10)
                .ToList();
            return resultado2;
        }


        //consultarPorId
        public Cliente ConsultarPorCodigo(long codigo)
        {
            return repository.ConsultarPorId<Cliente>(codigo);
        }


        //deletar
        public Cliente Deletar(long codigo)
        {
            var existente = ConsultarPorCodigo(codigo);
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