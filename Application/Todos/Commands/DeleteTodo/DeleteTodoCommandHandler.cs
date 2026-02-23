using System.Linq.Expressions;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Commands.DeleteTodo;

public sealed class DeleteTodoCommandHandler(IAppDbContext context) : IRequestHandler<DeleteTodoCommand>
{
    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

        if (todo is null)
            throw new NotFoundException(nameof(Todo), request.Id);

        context.Todos.Remove(todo);

        await context.SaveChangesAsync(cancellationToken);
    }
}