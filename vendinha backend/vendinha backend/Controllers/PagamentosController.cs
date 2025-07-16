using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vendinha_backend;
using vendinha_backend.Models;
using vendinha_backend.Services;

namespace vendinha_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly PagamentoService servico;

        public PagamentosController(PagamentoService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get(string busca)
        {
            if (string.IsNullOrEmpty(busca))
            {
                return Ok(servico.Consultar());
            }
            return Ok(servico.Consultar(busca));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pagamento pagamento)
        {

            if (servico.Cadastrar(pagamento, out List<MensagemErro> erros))
            {
                return Ok(pagamento);
            }
            return UnprocessableEntity(erros);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            var resultado = servico.Deletar(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var divida = servico.ConsultarPorCodigo(id);
            if (divida == null)
            {
                return NotFound();
            }
            return Ok(divida);
        }
    }
}
