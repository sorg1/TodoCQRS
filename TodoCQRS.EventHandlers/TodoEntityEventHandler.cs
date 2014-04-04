using System.Collections.Generic;
using System.Linq;
using TodoCQRS.Domain.Todo.Events;
using Paralect.ServiceBus;

namespace TodoCQRS.EventHandlers
{
    public class TodoEntityEventHandler : 
        IMessageHandler<Todo_CreatedEvent>, 
        IMessageHandler<Todo_UpdatedEvent>,
        IMessageHandler<Todo_RemovedEvent>
    {
        private static List<Todo> _todos = new List<Todo>();
        public static List<Todo> Todos
        {
            get { return _todos; }
            set { _todos = value; }
        }

        public void Handle(Todo_CreatedEvent message)
        {
            Todos.Add(new Todo
            {
                Id = message.Id,
                Name = message.Name
            });
        }

        public void Handle(Todo_UpdatedEvent message)
        {
            var todo = Todos.Single(x => x.Id == message.Id);
            todo.Name = message.Name;
        }

        public void Handle(Todo_RemovedEvent message)
        {
            Todos.RemoveAll(x => x.Id == message.Id);
        }
    }
}
