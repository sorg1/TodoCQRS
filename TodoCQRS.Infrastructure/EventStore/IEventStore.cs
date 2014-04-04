using System.Collections.Generic;
using Paralect.Domain;
using Paralect.Transitions;
//using Paralect.Transitions.Mongo;

namespace TodoCQRS.Infrastructure.EventStore
{
    public interface IEventStore
    {
       // MongoTransitionRepository CreateTransitionRepository();

        TransitionStream OpenStream(string id);
        TransitionStream OpenStream(string id, int fromVersion, int toVersion);
        void AppendToStream(string id, int originalVersion, ICollection<IEvent> events);
    }
}
