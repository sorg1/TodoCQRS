using TodoCQRS.Domain.Todo.Events;
using Paralect.Domain;

namespace TodoCQRS.Domain.Todo
{
    public class TodoState
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public void On(Todo_UpdatedEvent todoUpdated)
        {
            Name = todoUpdated.Name;
        }

        public void On(Todo_RemovedEvent todoRemoved)
        {
            Id = todoRemoved.Id;
        }

        public void On(Todo_CreatedEvent todoCreated)
        {
            Id = todoCreated.Id;
            Name = todoCreated.Name;
        }

        public void Mutate(IEvent @event)
        {
            ((dynamic)this).On((dynamic)@event);
        }
    }
}
