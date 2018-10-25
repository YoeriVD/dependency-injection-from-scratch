using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InjectionByExample
{
    public class ContainerBuilder
    {
        private List<InjectionFactory> _registeredTypes = new List<InjectionFactory>();

        public void Register<T>(Lifetime lifetime = Lifetime.NewInstance) where T : class
        {
            this.Register<T, T>(lifetime);
        }
        public void Register<ToResolve, ToCreate>(Lifetime lifetime = Lifetime.NewInstance) where ToCreate : class
        {
            var factory = new InjectionFactory(typeof(ToResolve), typeof(ToCreate), lifetime);
            this._registeredTypes.Add(factory);
        }

        public Container Build()
        {
            Task.WhenAll(this._registeredTypes.Select(f => f.AnalyzeDependencies())).Wait();
            return new Container(this._registeredTypes.ToDictionary(f => f.RegisteredType));
        }
    }
}