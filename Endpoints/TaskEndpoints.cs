using TasksAPI.Data;
using TasksAPI.Models;

namespace TasksAPI.Endpoints;

public static class TaskEndpoints
{
    private static RouteGroupBuilder _routeGroup;
    public static void Map(WebApplication app) // Adiciona os Endpoints configurados na classe1
    {
        _routeGroup = app.MapGroup("/api");
        _routeGroup.MapGet("/tasks", GetTaskModels);
    }

    private static List<TaskModel> GetTaskModels()
    {
        var listaTasks = TaskStore.taskList;
        return listaTasks;
    }
}
