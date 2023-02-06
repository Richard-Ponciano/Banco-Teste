using Banco.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .CoreRegister()
    .AddAuth(builder.Configuration)
    .AddMapper()
    .RepositoryRegister()
    .MediatRRegister()
    .ServiceRegister()
    .AddResponseCompress()
    .AddSwagger();

// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
