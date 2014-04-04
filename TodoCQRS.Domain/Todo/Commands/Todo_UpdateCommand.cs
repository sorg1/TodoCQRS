using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Commands
{
    public class Todo_UpdateCommand : Command
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}