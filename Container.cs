using System;
using System.Collections.Generic;
using System.Linq;

namespace InjectionByExample
{
    public class Container
    {
        private readonly IDictionary<Type, InjectionFactory> _factories;
        private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();
        public Container Parent { get; private set; }

        public Container(IDictionary<Type, InjectionFactory> factories, Container parent = null)
        {
            _factories = factories;
            this.Parent = parent;
        }
        public object Resolve(Type t)
        {
            if (this.Parent != null && this.Parent.HasInstance(t))
            {
                Console.WriteLine($"Container: Type already created by Parent");
                var parentInstance = this.Parent.Resolve(t);
                return parentInstance;
            }
            if (this._instances.ContainsKey(t))
            {
                var existing = this._instances[t];
                Console.WriteLine($"Container: Type already created, returning instance {existing}");
                return existing;
            }

            Console.WriteLine($"Container: resolving instance for {t.Name}");
            if (!_factories.ContainsKey(t)) throw new Exception($"Type {t.Name} is not registered.");

            var factory = _factories[t];
            Console.WriteLine($"Container: Concrete type for {t.Name} is {factory.ConcreteType.Name} with lifetime {factory.Lifetime}");

            var instance = factory.CreateInstance(this);
            Console.WriteLine($"Container: instance created {instance}");
            if (factory.Lifetime == Lifetime.InstancePerContainer) this._instances.Add(t, instance);
            return instance;
        }

        public T Resolve<T>()
        {
            var t = typeof(T);
            return (T)this.Resolve(t);
        }

        public Container CreateChild()
        {
            return new Container(this._factories, this);
        }
        public bool HasInstance(Type t)
        {
            return this._instances.ContainsKey(t);
        }
    }
}