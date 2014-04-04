using System.Collections.Generic;
using System.Security.Authentication;
using TodoCQRS.Domain.Todo.Events;
using TodoCQRS.Infrastructure.Snapshots;
using Paralect.Domain;
using Paralect.Transitions;

namespace TodoCQRS.Domain.Todo
{
    public class TodoAR
    {
        private readonly List<IEvent> _changes = new List<IEvent>();

        public List<IEvent> Changes
        {
            get { return _changes; }
        }

        private readonly TodoState _state;

        internal TodoState State
        {
            get { return _state; }
        }

        public TodoAR(string todoId, string name)
        {
            _state = new TodoState();
            Apply(new Todo_CreatedEvent
            {
                Id = todoId,
                Name = name
            });
        }

        public TodoAR(Snapshot snapshot, TransitionStream stream)
        {
            _state = snapshot != null ? (TodoState) snapshot.Payload : new TodoState();
            foreach (var transition in stream.Read())
            {
                foreach (var @event in transition.Events)
                {
                    _state.Mutate((IEvent) @event.Data);
                }
            }
        }

        public void Update(string name)
        {
            Apply(new Todo_UpdatedEvent
            {
                Id = _state.Id,
                Name = name
            });
        }

        public void Delete()
        {
            Apply(new Todo_RemovedEvent
            {
                Id = _state.Id
            });
        }

        private void Apply(IEvent evt)
        {
            State.Mutate(evt);
            Changes.Add(evt);
        }
    }
}
