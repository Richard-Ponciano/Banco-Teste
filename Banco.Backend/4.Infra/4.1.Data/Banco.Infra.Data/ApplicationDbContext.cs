using Banco.Domain.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Banco.Infra.Data
{
    public class ApplicationDbContext
        : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                    _configuration.GetConnectionString("BancoDbConnStr"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(3),
                            errorNumbersToAdd: null);
                    });
            options.EnableSensitiveDataLogging();
            options.EnableServiceProviderCaching();
        }

        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<ContaModel> Conta { get; set; }
        public DbSet<ContaHistoricoModel> ContaHistorico { get; set; }
        public DbSet<LoginModel> Login { get; set; }
    }
}