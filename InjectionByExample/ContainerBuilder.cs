using System.Collections.Generic;
using System.Linq;

namespace InjectionByExample
{
    public class ContainerBuilder
    {
        private readonly List<Registration> _registeredTypes = new List<Registration>();

        public void Register<T>(Lifetime lifetime = Lifetime.NewInstance) where T : class
        {
            Register<T, T>(lifetime);
        }

        public void Register<ToResolve, ToCreate>(Lifetime lifetime = Lifetime.NewInstance) where ToCreate : class
        {
            var registration = new Registration(typeof(ToResolve), typeof(ToCreate), lifetime);
            _registeredTypes.Add(registration);
        }

        public Container Build()
        {
            // we use reflection to get the first constructor and see what parameters are needed
            // to create an instance. 
            // run this once, multithreaded, and store the conclusion in the registration.
            _registeredTypes.AsParallel().ForAll(f => f.AnalyzeDependencies());
            // create a dictionary for fast lookups
            var registrations = _registeredTypes.ToDictionary(f => f.RegisteredType);
            return new Container(registrations);
        }
    }
}