using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Commands
{
    class Todo_RemoveCommand : Command
    {
        public string Id { get; set; }
    }
}
