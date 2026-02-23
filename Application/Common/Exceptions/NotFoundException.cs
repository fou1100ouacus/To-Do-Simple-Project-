namespace Application.Common.Exceptions;

public class NotFoundException(string entity, Guid id) : Exception($"the {entity} with id: '{id}' not found");