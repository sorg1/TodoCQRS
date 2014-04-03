using Paralect.Domain;

namespace TodoCQRS.Domain.Todo.Events
{
    class Todo_RemovedEvent : Event
    {
        public string Id { get; set; }
    }
}
