using System.ComponentModel.DataAnnotations;
using vendinha_backend.Models;
using vendinha_backend.Repository;

namespace vendinha_backend.Services
{
    public class PagamentoService
    {
        private readonly IRepository repository;

        public PagamentoService(IRepository repository)
        {
            this.repository = repository;
        }
        public bool Cadastrar(Pagamento pagamento, out List<MensagemErro> mensagens)
        {
            var valido = Validar(pagamento, out mensagens);
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
        public bool Validar(Pagamento pagamento, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(pagamento);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(pagamento,
                validationContext,
                erros,
                true);
            mensagens = new List<MensagemErro>();

            var cursosNome = repository.Consultar<Pagamento>()
                            .Where(e => e.Id == pagamento.Id)
                            .Count();


            decimal totalDividasCliente = pagamento.Cliente?.Dividas?.Sum(d => d?.ValorTotal ?? 0) ?? 0;
            decimal totalComNovoPagamento = totalDividasCliente + pagamento.ValorTotal;

            if (totalComNovoPagamento > 2000)
            {
                mensagens.Add(new MensagemErro("Dividas", "O cliente não pode dever mais de R$ 2000,00."));
                validation = false;
            }

            foreach (var erro in erros)
            {
                var propriedade = erro.MemberNames.FirstOrDefault() ?? "CampoDesconhecido";
                var mensagem = new MensagemErro(propriedade, erro.ErrorMessage);

                mensagens.Add(mensagem);
                Console.WriteLine($"{propriedade}: {erro.ErrorMessage}");
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
