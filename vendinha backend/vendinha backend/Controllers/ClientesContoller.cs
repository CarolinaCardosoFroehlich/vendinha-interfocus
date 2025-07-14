using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vendinha_backend;
using vendinha_backend.Models;
using vendinha_backend.Services;

namespace vendinha_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesContoller : ControllerBase
    {
        private readonly ClienteService servico;

        public ClientesContoller(ClienteService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get(string pesquisa)
        {
            return string.IsNullOrEmpty(pesquisa) ?
                Ok(servico.Consultar()) :
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


        [HttpPut]
        public IActionResult Put([FromBody] Cliente cliente)
        {
            var resultado = servico.Editar(cliente, out List<MensagemErro> mensagens);
            if (resultado == null)
            {
                if (mensagens == null)
                    return NotFound();
                else
                {
                    return UnprocessableEntity(mensagens);
                }
            }
            return Ok(resultado);
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

        [HttpGet("{codigo}")]
        public IActionResult GetById(long codigo)
        {
            var resultado = servico.ConsultarPorCodigo(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

    }
}
