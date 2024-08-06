// Program da API é responsável por configurar a aplicação e iniciar o servidor web.

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Bankokeo.Data;
using Bankokeo.Services;

var builder = WebApplication.CreateBuilder(args);

// Especifica que a aplicação vai usar autenticação JWT e como vou descriptografar o token
var jwtKey = builder.Configuration["JwtKey"];
var key = jwtKey != null ? Encoding.ASCII.GetBytes(jwtKey) : throw new ArgumentNullException("JwtKey", "The JWT key cannot be null.");

if (key == null || key.Length == 0)
{
    throw new ArgumentNullException("JwtKey", "The JWT key cannot be null or empty.");
}

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddControllers();
// Configuração para não retornar erro 400 quando o ModelState for inválido
// .ConfigureApiBehaviorOptions(options =>
// {
//     options.SuppressModelStateInvalidFilter = true;
// });

// Lembra que foi dito que não pode ter 2 conexões com o banco de dados abertas ao mesmo tempo?
// Por isso, SEMPRE USAR O #USING para criar uma instância do #DataContext dentro de onde eu precisar.
// Mas, ASP.NET configura o contexto por requisição e cuida da conexão e fim da conexão
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=localhost;Database=okeo;User Id=sa"));
// virou um serviço
// a partir disso, posso usar injeção de dependência no HomeController
builder.Services.AddDbContext<AppDbContext>();

// resolvendo injeção de dependência
builder.Services.AddTransient<TokenService>(); // sempre cria uma nova instância a cada requisição

// cria e dura a cada transação
// builder.Services.AddScoped(); 

// cria uma única instância para toda a aplicação
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();