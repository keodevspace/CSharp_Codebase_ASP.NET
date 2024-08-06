// Program da API é responsável por configurar a aplicação e iniciar o servidor web.

using Bankokeo.Data;

var builder = WebApplication.CreateBuilder(args);

builder
.Services
.AddControllers();
// Configuração para não retornar erro 400 quando o ModelState for inválido
// .ConfigureApiBehaviorOptions(options =>
// {
//     options.SuppressModelStateInvalidFilter = true;
// });

// Lembra que foi dito que não pode ter 2 conexões com o banco de dados abertas ao mesmo tempo?
// Por issso, SEMPRE USAR O #USING para criar uma instância do #DataContext dentro de onde eu precisar.
// Mas, ASP.NET configura o contexto por requisição e cuida da conexão e fim da conexão
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=localhost;Database=Bankokeo;User Id=sa
// virou um serviço
// a partir disso, posso usar injeção de dependência no HomeController
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");
// faz o mesmo que o app.MapGet do inicio da aplicação
app.MapControllers();

app.Run();
