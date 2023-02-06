using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;
using Banco.Domain;
using Banco.Domain.Contract.Mapper;
using Banco.Infra.Data;
using Banco.Infra.Data.Contract;
using Banco.Service;
using Banco.Service.Contract;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Banco.Infra.CrossCutting.IoC
{
    public static class CoreInjectorExtension
    {
        public static IServiceCollection CoreRegister(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddResponseCompress(this IServiceCollection services)
        {
            //Compressão de resposta da Api
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetGetMethod(true).IsPrivate || p.GetGetMethod(true).IsHideBySig;
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });

            return services;
        }

        public static IServiceCollection RepositoryRegister(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IContaHistoricoRepository, ContaHistoricoRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();

            return services;
        }

        public static IServiceCollection MediatRRegister(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(Domain.Cliente.Command.AddClienteCommand).Assembly,
                typeof(Domain.Cliente.Command.UpdClienteCommand).Assembly,
                typeof(Domain.Conta.Command.AddContaCommand).Assembly,
                typeof(Domain.Login.Command.AddLoginCommand).Assembly,
                typeof(Domain.Login.Command.UpdLoginCommand).Assembly,
                typeof(Domain.Login.Command.LoginAcessCommand).Assembly,
                typeof(Domain.ContaHistorico.Command.UpdContaHistoricoCommand).Assembly);

            return services;
        }

        public static IServiceCollection ServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<ILoginService, LoginService>();

            return services;
        }

        /// <summary>
        /// Configura e authentica a requisição
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? throw new ArgumentNullException("chave Jwt inexistente"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Banco Teste",
                    Description = "Api do contexto do Banco Teste"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}