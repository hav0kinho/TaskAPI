// Deve ser uma API básica que permite que o usuário possa cadastrar, buscar e modificar tarefas.


var builder = WebApplication.CreateBuilder(args);
// Services


var app = builder.Build();

// Middlewares


// Rotas
app.MapGet("/", () => "Hello World!");

// Execução
app.Run();
