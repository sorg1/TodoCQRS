using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Commands
{
    public class Todo_RemoveCommand : Command
    {
        public string Id { get; set; }
    }
}