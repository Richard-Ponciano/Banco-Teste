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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(
            IClienteService service)
        {
            _service = service;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lst = await _service.GetAllAsync().ConfigureAwait(false);
            return (lst?.Any() ?? false)
                ? Ok(lst) 
                : NotFound("* Clientes inexistentes.");
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteVM vm)
        {
            try
            {
                var result = await _service.AddAsync(vm).ConfigureAwait(false);
                return result > 0
                    ? Ok(result)
                    : BadRequest("* Problemas ao cadastrar Cliente.");
            }
            catch(Exception)
            {
                throw;
            }
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
