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
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(
            ILoginService service)
        {
            _service = service;
        }

        // GET: api/<LoginController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lst = await _service.GetAllAsync().ConfigureAwait(false);
            return (lst?.Any() ?? false)
                ? Ok(lst)
                : NotFound("* Logins insxistentes.");
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginVM vm)
        {
            try
            {
                var result = await _service.AddAsync(vm).ConfigureAwait(false);
                return result > 0
                    ? Ok(result)
                    : BadRequest("* Problemas ao cadastrar Login.");
            }
            catch(Exception)
            {
                throw;
            }
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [AllowAnonymous]
        [HttpPost("LoginAcess")]
        public async Task<IActionResult> LoginAcess([FromBody] LoginAcessVM vm)
        {
            try
            {
                var token = await _service.GetAcessTokenAsync(vm).ConfigureAwait(false);
                return !string.IsNullOrEmpty(token)
                    ? Ok(token)
                    : BadRequest("* Problemas ao gerar token.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}