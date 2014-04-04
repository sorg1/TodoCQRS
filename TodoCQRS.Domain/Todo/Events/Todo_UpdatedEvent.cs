using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Events
{
    public class Todo_UpdatedEvent : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
