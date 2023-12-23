using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Models.DTO;

namespace TasksAPI.Endpoints;

public static class TaskEndpoints
{
    private static RouteGroupBuilder _routeGroup;
    public static void Map(WebApplication app) // Adiciona os Endpoints configurados na classe
    {
        _routeGroup = app.MapGroup("/api");
        _routeGroup.MapGet("/task", GetTasks);
        _routeGroup.MapGet("/task/{id:int}", GetTask);
        _routeGroup.MapPost("/task", PostTask);
        _routeGroup.MapPut("/task/{id:int}", PutTask);
        _routeGroup.MapDelete("/task/{id:int}", DeleteTask);
    }

    private static async Task<Ok<TaskModel[]>> GetTasks(TaskDb db)
    {
        return TypedResults.Ok(await db.Tasks.ToArrayAsync());
    }

    private static async Task<Results<Ok<TaskModel>, NotFound>> GetTask(int id, TaskDb db)
    {
        var taskRequisitado = await db.Tasks.FindAsync(id);
        return taskRequisitado != null ? TypedResults.Ok(taskRequisitado) : TypedResults.NotFound();
    }

    private static async Task<Results<Created<TaskDTO>, BadRequest<string>>> PostTask(IMapper _mapper, IValidator<TaskDTO> _validation, TaskDTO taskDTO, TaskDb db)
    {
        var validationResult = await _validation.ValidateAsync(taskDTO);

        if(!validationResult.IsValid) // Verificando validação e retornando o primeiro erro;
        {
            return TypedResults.BadRequest(validationResult.Errors.FirstOrDefault().ToString());
        }

        TaskModel task = _mapper.Map<TaskModel>(taskDTO); // Converter o TaskDTO recebido para TaskModel
        task.Id = new Random().Next(1, 100000000); // Gerando um ID PseudoÚnico (PRECISO TROCAR DEPOIS)

        TaskDTO taskDTOCreated = _mapper.Map<TaskDTO>(task); // Converte a TaskModel em TaskDTO, para retornar ao usuário;

        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/task/{task.Id}", taskDTOCreated);
    }

    private static async Task<Results<NoContent, BadRequest<string>>> PutTask(IValidator<TaskDTO> _validation, IMapper _mapper, int id, TaskDTO taskDTO, TaskDb db)
    {
        var validationResult = await _validation.ValidateAsync(taskDTO);

        if(!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors.FirstOrDefault().ToString());
        }

        var task = _mapper.Map<TaskModel>(taskDTO);
        var taskRequisitada = await db.Tasks.FindAsync(id);

        if(taskRequisitada == null)
        {
            return TypedResults.BadRequest("A task não foi encontrada");
        }


        taskRequisitada.Title = task.Title;
        taskRequisitada.IsCompleted = task.IsCompleted;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, BadRequest<string>>> DeleteTask(int id, TaskDb db)
    {
        var taskRequisitada = await db.Tasks.FindAsync(id);
        if(taskRequisitada == null)
        {
            return TypedResults.BadRequest("Task não encontrada");
        }

        db.Tasks.Remove(taskRequisitada);

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
