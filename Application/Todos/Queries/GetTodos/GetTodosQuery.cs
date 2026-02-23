using Domain.Todos;
using MediatR;

namespace Application.Todos.Queries.GetTodos;

public sealed record GetTodosQuery: IRequest<List<Todo>>;