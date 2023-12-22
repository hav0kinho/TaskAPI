using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Data;

public class TaskDb : DbContext
{
    public TaskDb(DbContextOptions<TaskDb> options) : base(options){}

    public DbSet<TaskModel> Tasks => Set<TaskModel>(); // Criando a "Tabela" no Banco!
}
