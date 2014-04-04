using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Events
{
    public class Todo_CreatedEvent : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
