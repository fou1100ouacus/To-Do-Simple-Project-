using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Queries.GetTodoById;

public sealed class GetTodoByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetTodoByIdQuery, Todo?>
{
    public async Task<Todo?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Todos.FindAsync([request.Id], cancellationToken);
    }
}