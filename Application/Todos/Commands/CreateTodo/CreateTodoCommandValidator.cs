using FluentValidation;

namespace Application.Todos.Commands.CreateTodo;

public class CreateTodoCommandValidator: AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title should not be empty");
    }
}