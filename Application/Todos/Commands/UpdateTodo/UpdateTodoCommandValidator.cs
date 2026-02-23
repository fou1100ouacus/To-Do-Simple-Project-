using FluentValidation;

namespace Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandValidator: AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title should not be empty");
    }
}