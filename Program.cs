using Gerenciamento_Financeiro.Repository_Pattern;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Apenas o Swashbuckle, remove AddOpenApi()
// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        // Adicione aqui os domínios que vão acessar sua API
        policy.AllowAnyOrigin()          
            .AllowAnyHeader()   // Todos Header
            .AllowAnyMethod();  // Todos os métodos
    });
});


// Registrar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Registrar Repositories
builder.Services.AddScoped<ILancamentosRepository, LancamentosRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// Autenticação JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciamento Financeiro API V1");
        c.RoutePrefix = string.Empty; // Abre Swagger direto no localhost:5038/
    });
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseAuthentication();
app.UseAuthorization();

// Mapeia controllers
app.MapControllers();

app.Run();
