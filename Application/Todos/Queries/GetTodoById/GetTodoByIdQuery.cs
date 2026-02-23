using Domain.Todos;
using MediatR;

namespace Application.Todos.Queries.GetTodoById;

public sealed record GetTodoByIdQuery(Guid Id): IRequest<Todo?>;