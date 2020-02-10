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
            // get the registration first
            var registration = _registrations[t];
            // determine the activator based on the lifetime of the registration
            var activator = _activators.First(act => act.ForLifeTime == registration.Lifetime);
            // create an instance
            var instance = activator.CreateInstance(this, registration);
            // return the instance
            return instance;
        }

        public T Resolve<T>()
        {
            var t = typeof(T);
            return (T)this.Resolve(t);
        }

        /*
        Very popular feature is to create child containers. We can do this by handing down the existing registrations
        and of course a reference to the parent (this).
         */
        public Container CreateChild()
        {
            return new Container(this._registrations, this);
        }

        /*
        This method is only for the InstancePerContainerActivator.
        This is used to ask the parent container if an instance already exists.
         */
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