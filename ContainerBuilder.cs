using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InjectionByExample
{
    public class ContainerBuilder
    {
        private List<Registration> _registeredTypes = new List<Registration>();

        public void Register<T>(Lifetime lifetime = Lifetime.NewInstance) where T : class
        {
            this.Register<T, T>(lifetime);
        }
        public void Register<ToResolve, ToCreate>(Lifetime lifetime = Lifetime.NewInstance) where ToCreate : class
        {
            var factory = new Registration(typeof(ToResolve), typeof(ToCreate), lifetime);
            this._registeredTypes.Add(factory);
        }

        public Container Build()
        {
            // run this multithreaded to speed op the process
            this._registeredTypes.AsParallel().ForAll(f => f.AnalyzeDependencies());
            // create a dictionary for fast lookups
            var registrations = this._registeredTypes.ToDictionary(f => f.RegisteredType);
            return new Container(registrations);
        }
    }
}