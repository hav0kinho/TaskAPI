﻿using AutoMapper;
using FluentValidation;
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

    private static async Task<IResult> GetTasks(TaskDb db)
    {
        return Results.Ok(await db.Tasks.ToListAsync());
    }

    private static async Task<IResult> GetTask(int id, TaskDb db)
    {
        var taskRequisitado = await db.Tasks.FindAsync(id);
        return taskRequisitado != null ? Results.Ok(taskRequisitado) : Results.NotFound();
    }

    private static async Task<IResult> PostTask(IMapper _mapper, IValidator<TaskDTO> _validation, TaskDTO taskDTO, TaskDb db)
    {
        var validationResult = await _validation.ValidateAsync(taskDTO);

        if(!validationResult.IsValid) // Verificando validação e retornando o primeiro erro;
        {
            return Results.BadRequest(validationResult.Errors.FirstOrDefault().ToString());
        }

        TaskModel task = _mapper.Map<TaskModel>(taskDTO); // Converter o TaskDTO recebido para TaskModel
        task.Id = TaskStore.taskList.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1; // Pega o maior ID que existe na lista, adiciona + 1 a nova task;

        TaskDTO taskDTOCreated = _mapper.Map<TaskDTO>(task); // Converte a TaskModel em TaskDTO, para retornar ao usuário;

        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();
        return Results.Created($"/task/{task.Id}", taskDTOCreated);
    }

    private static async Task<IResult> PutTask(int id, TaskModel task, TaskDb db)
    {
        var taskRequisitada = await db.Tasks.FindAsync(id);
        taskRequisitada.Title = task.Title;
        taskRequisitada.IsCompleted = task.IsCompleted;

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTask(int id, TaskDb db)
    {
        var taskRequisitada = await db.Tasks.FindAsync(id);
        db.Tasks.Remove(taskRequisitada);

        await db.SaveChangesAsync();

        return Results.NotFound();
    }
}
