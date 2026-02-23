using MediatR;

namespace Application.Todos.Commands.UpdateTodo;

public sealed record UpdateTodoCommand(Guid Id, string Title, bool Completed) : IRequest;