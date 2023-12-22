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

    private static IResult GetTasks()
    {
        var listaTasks = TaskStore.taskList;
        return Results.Ok(listaTasks);
    }

    private static IResult GetTask(int id)
    {
        var taskRequisitada = TaskStore.taskList.FirstOrDefault(task => task.Id == id);
        return Results.Ok(taskRequisitada);
    }

    private static IResult PostTask(TaskModel task)
    {
        TaskStore.AddTask(task);
        return Results.Created($"/task/{task.Id}", task);
    }
}
