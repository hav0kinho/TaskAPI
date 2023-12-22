// Deve ser uma API básica que permite que o usuário possa usar um CRUD de Tasks.
using Swashbuckle.AspNetCore; // Biblioteca para Swagger
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;


using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Endpoints;
using TasksAPI.Data;
using TasksAPI;


var builder = WebApplication.CreateBuilder(args);
// Services
builder.Services.AddEndpointsApiExplorer(); // Prepara o terreno para criação do Swagger e OpenAI (Deve ser usado junto com o AddSwaggerGen())
builder.Services.AddSwaggerGen(); // Permite a geração do Swagger

builder.Services.AddAutoMapper(typeof(MappingConfig)); // Adicionando o AutoMapper para aplicação
builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // Adicionando o FluentValidation na aplicação!

builder.Services.AddDbContext<TaskDb>(opt => opt.UseInMemoryDatabase("TaskList")); //Usando o TaskDb como base, e nas opt, estamos configurando um Banco baseado no InMemory! Colocando o nome dele como sendo "TaskList"


var app = builder.Build();
// Middlewares
app.UseSwagger(); // Adiciona o Middleware do Swagger, gerando o documento para a TasksAPI (Normalmente usado em conjunto do UseSwaggerUI)
app.UseSwaggerUI(); // Adiciona uma interface interativa do Swagger, permitindo visualizar melhor as rotas e endpoints na rota /swagger

// Rotas
app.MapGet("/", () => "Para acessar a API, você deve acessar '/api/<endpoint>'! Por exemplo, para acessar todas as tarefas, você pode utilizar o '/api/tasks'");

TaskEndpoints.Map(app);

/*
app.MapGet("/api/tasks", () => {
    var listaTasks = TaskStore.taskList;

    return Results.Ok(listaTasks);
});
*/

// Execução
app.Run();
