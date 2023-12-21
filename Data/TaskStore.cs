using TasksAPI.Models;

namespace TasksAPI.Data;

public static class TaskStore
{
    public static List<TaskModel> taskList = new List<TaskModel>
    {
        new TaskModel{Id=1, Title="Limpar a casa", IsCompleted=false},
        new TaskModel{Id=2, Title="Fazer comida", IsCompleted=true},
    };
}
