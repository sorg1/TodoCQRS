using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Events
{
    public class Todo_RemovedEvent : Event
    {
        public string Id { get; set; }
    }
}
