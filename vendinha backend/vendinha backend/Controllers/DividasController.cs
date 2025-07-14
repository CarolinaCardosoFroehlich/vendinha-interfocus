using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vendinha_backend;
using vendinha_backend.Models;
using vendinha_backend.Services;

namespace vendinha_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividasController : ControllerBase
    {
        private readonly DividasService servico;

        public DividasController(DividasService servico)
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
        public IActionResult Post([FromBody] Divida divida)
        {

            if (servico.Cadastrar(divida, out List<MensagemErro> erros))
            {
                return Ok(divida);
            }
            return UnprocessableEntity(erros);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Divida divida)
        {
            var resultado = servico.Editar( divida);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
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
            var curso = servico.ConsultarPorCodigo(id);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(dividaaaaaaaaaaaaaaaaaaaaaaaaaa);
        }
    }
}
