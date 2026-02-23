using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Commands.UpdateTodo;

public sealed class UpdateTodoCommandHandler(IAppDbContext context) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

        if (todo is null)

            throw new NotFoundException(nameof(Todo), request.Id);
        todo.Title = request.Title;
        todo.Completed = request.Completed;

        await context.SaveChangesAsync(cancellationToken);
    }
}