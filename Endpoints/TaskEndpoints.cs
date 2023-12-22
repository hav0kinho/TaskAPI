using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;

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
    }

    private static async Task<List<TaskModel>> GetTasks(TaskDb db)
    {
        return await db.Tasks.ToListAsync();
    }

    private static async Task<IResult> GetTask(int id, TaskDb db)
    {
        var taskRequisitado = await db.Tasks.FindAsync(id);
        return taskRequisitado != null ? Results.Ok(taskRequisitado) : Results.NotFound();
    }

    private static async Task<IResult> PostTask(TaskModel task, TaskDb db)
    {
        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();
        return Results.Created($"/task/{task.Id}", task);
    }
}
