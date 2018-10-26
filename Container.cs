using System;
using System.Collections.Generic;
using System.Linq;

namespace InjectionByExample
{
    public class Container
    {
        private readonly IDictionary<Type, Registration> _registrations;
        public Container Parent { get; private set; }
        private IActivator[] _activators = new IActivator[] {
            new NewInstanceActivator(),
            new InstancePerContainerActivator(),
            new SingleInstanceActivator()
            };

        public Container(IDictionary<Type, Registration> registrations, Container parent = null)
        {
            _registrations = registrations;
            this.Parent = parent;
        }
        public object Resolve(Type t)
        {
            if (!_registrations.ContainsKey(t)) throw new Exception($"Type {t.Name} is not registered.");
            var registration = _registrations[t];
            var activator = _activators.First(act => act.ForLifeTime == registration.Lifetime);
            var instance = activator.CreateInstance(this, registration);
            return instance;
        }

        public T Resolve<T>()
        {
            var t = typeof(T);
            return (T)this.Resolve(t);
        }

        public Container CreateChild()
        {
            return new Container(this._registrations, this);
        }
        public bool HasInstance(Type t)
        {
            return this._activators
            .OfType<InstancePerContainerActivator>()
            .SingleOrDefault()
            ?.HasInstance(t)
            ?? false;
        }
    }
}