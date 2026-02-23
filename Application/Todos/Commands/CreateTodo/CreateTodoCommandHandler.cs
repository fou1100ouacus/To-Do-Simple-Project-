using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Commands.CreateTodo;

public sealed class CreateTodoCommandHandler(IAppDbContext context) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Title = request.Title
        };

        context.Todos.Add(todo);

        await context.SaveChangesAsync(cancellationToken);

        return todo.Id;
    }
}