using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Banco.Domain.Contract;
using Banco.Domain.Login.Command;
using Banco.Infra.Data.Contract;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Banco.Domain.Login.Handler
{
    public class LoginAcessHandler
        : LoginHandle, IRequestHandler<LoginAcessCommand, string>
    {
        private static string _secret;

        public LoginAcessHandler(
            ILoginRepository repository,
            IConfiguration settings)
            : base(repository)
        {
            _secret = settings["Jwt:Key"];
        }

        public async Task<string> Handle(LoginAcessCommand request, CancellationToken cancellationToken)
        {
            if(request.Entity == null)
                throw new Exception("* Entidade Login vazia.");

            if(string.IsNullOrEmpty(request.Entity.Usuario) || string.IsNullOrEmpty(request.Entity.Senha))
                throw new Exception("* Credenciais Inválidas.");

            var login = await _repository.GetSingleAsync(l =>
                l.Usuario == request.Entity.Usuario &&
                l.Senha == request.Entity.Senha).ConfigureAwait(false);

            if(login != null)
            {
                return GenerateToken(login);
            }

            throw new Exception("* Login Inexistente.");
        }

        private static string GenerateToken(LoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[2]
            {
                new Claim(ClaimTypes.Name, user.Usuario.ToString()),
                new Claim("LoginId", user.Id.ToString())
            };

            try
            {
                var token = new JwtSecurityToken(
                   issuer: null,
                   audience: null,
                   claims: claims,
                   expires: DateTime.Now.AddHours(4),
                   signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception)
            {
                // add AppInsights
                throw;
            }
            finally
            {
                securityKey = null;
                credentials = null;
                claims = null;
            }
        }
    }
}