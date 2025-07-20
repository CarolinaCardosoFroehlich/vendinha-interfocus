using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vendinha_backend;
using vendinha_backend.Models;
using vendinha_backend.Services;

namespace vendinha_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService servico;

        public ClientesController(ClienteService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get(string pesquisa)
        {
            return string.IsNullOrEmpty(pesquisa) ?
                Ok(servico.ConsultarClientesOrdenadosPorDivida()) :
                Ok(servico.Consultar(pesquisa));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (servico.Cadastrar(cliente, out List<MensagemErro> erros))
            {
                return Ok(cliente);
            }

            return UnprocessableEntity(erros);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(long codigo)
        {
            var resultado = servico.Deletar(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

    }
}
