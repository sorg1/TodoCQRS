using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Events
{
    class Todo_UpdatedEvent : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
