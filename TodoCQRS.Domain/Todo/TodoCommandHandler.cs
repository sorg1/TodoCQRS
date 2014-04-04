using System;
using TodoCQRS.Domain.Todo.Commands;
using TodoCQRS.Infrastructure.EventStore;
using TodoCQRS.Infrastructure.Snapshots;
using Paralect.ServiceBus;

namespace TodoCQRS.Domain.Todo
{
    public class TodoCommandHandler :
        IMessageHandler<Todo_UpdateCommand>,
        IMessageHandler<Todo_RemoveCommand>,
        IMessageHandler<Todo_CreateCommand>
    {
        private readonly ISnapshotRepository _snapshotRepository;
        private readonly IEventStore _eventStore;
        private const int SnapshotInterval = 100;

        public TodoCommandHandler(ISnapshotRepository snapshotRepository, IEventStore eventStore)
        {
            _snapshotRepository = snapshotRepository;
            _eventStore = eventStore;
        }

        public void Handle(Todo_RemoveCommand message)
        {
            Update(message.Id, (todo) => todo.Delete());
        }

        public void Handle(Todo_CreateCommand message)
        {
            var user = new TodoAR(message.Id, message.Name);
            _eventStore.AppendToStream(message.Id, 0, user.Changes);
        }

        public void Handle(Todo_UpdateCommand message)
        {
            Update(message.Id, (todo) => todo.Update(message.Name));
        }

        private void Update(string todoId, Action<TodoAR> updateAction)
        {
            var snapshot = _snapshotRepository.Load(todoId);
            var startVersion = snapshot == null ? 0 : snapshot.StreamVersion + 1;
            var stream = _eventStore.OpenStream(todoId, startVersion, int.MaxValue);
            var todo = new TodoAR(snapshot, stream);
            updateAction(todo);
            var originalVersion = stream.GetVersion();
            _eventStore.AppendToStream(todoId, originalVersion, todo.Changes);
            var newVersion = originalVersion + 1;
            if (newVersion % SnapshotInterval == 0)
            {
                _snapshotRepository.Save(new Snapshot(todoId, newVersion, todo.State));
            }
        }
    }
}