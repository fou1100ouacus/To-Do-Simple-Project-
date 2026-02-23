namespace Domain.Todos;

public class Todo
{
    public Guid Id {get; set;}
    public string Title {get; set;} = default!;
    public bool Completed {get; set;} 
}