using FluentValidation;
using TasksAPI.Models.DTO;

namespace TasksAPI.Validation;

public class TaskModelValidation : AbstractValidator<TaskDTO>
{
    public TaskModelValidation()
    {
        RuleFor(task => task.Title)
        .NotEmpty()
        .NotNull()
        .Length(1, 100);
        
        RuleFor(task => task.IsCompleted).NotNull();
    }
}
