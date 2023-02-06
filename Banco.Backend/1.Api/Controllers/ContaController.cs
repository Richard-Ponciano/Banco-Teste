using Banco.Domain.Contract.ViewModel;
using Banco.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banco.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _service;

        public ContaController(
            IContaService service)
        {
            _service = service;
        }

        // GET: api/<ContaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lst = await _service.GetAllAsync().ConfigureAwait(false);
            return (lst?.Any() ?? false)
                ? Ok(lst)
                : NotFound();
        }

        // GET api/<ContaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContaVM vm)
        {
            var result = await _service.AddAsync(vm).ConfigureAwait(false);
            return result > 0
                ? Ok(result)
                : BadRequest("* Problemas ao cadastrar conta.");
        }

        // PUT api/<ContaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("UpdSaldo")]
        public async Task<IActionResult> AtualizarSaldo([FromBody] UpdSaldoContaVM vm)
        {
            try
            {
                var result = await _service.AtualizarSaldoContaAsync(vm).ConfigureAwait(false);
                return (!result.HasValue)
                    ? NotFound("* Conta Inexistente.")
                    : (result.Value
                        ? Ok("* Saldo Atualizado.")
                        : BadRequest("* Problemas ao atualizar saldo."));
            }
            catch(Exception ex)
            {
                return BadRequest($"* Problemas ao atualizar saldo: {ex.Message}");
            }
        }
    }
}