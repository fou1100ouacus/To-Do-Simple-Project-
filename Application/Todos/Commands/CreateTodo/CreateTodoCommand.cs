using MediatR;

namespace Application.Todos.Commands.CreateTodo;

public sealed record CreateTodoCommand(string Title) : IRequest<Guid>;