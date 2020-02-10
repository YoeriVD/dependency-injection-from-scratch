using System;
using System.Collections.Generic;
using System.Linq;

namespace InjectionByExample
{
    public enum Lifetime
    {
        NewInstance = 0,
        InstancePerContainer = 1,
        SingleInstance = 2
    }

    public interface IActivator
    {
        Lifetime ForLifeTime { get; }
        object CreateInstance(Container container, Registration factory);
    }

    /**
    The easiest to implement. Just create a new instance for every dependency and then create the requested type.
     */
    public class NewInstanceActivator : IActivator
    {
        public Lifetime ForLifeTime { get; } = Lifetime.NewInstance;

        public object CreateInstance(Container container, Registration factory)
        {
            Console.WriteLine($"NewInstanceActivator: creating new instance for {factory.RegisteredType.Name}");
            var parameters = factory.ConstructorDepencencies.Select(dep => container.Resolve(dep)).ToArray();
            var instance = Activator.CreateInstance(factory.ConcreteType, parameters);
            return instance;
        }
    }

    /**
    Same logic as above, but keep a reference to every created instance. This way we can return the same instance as before.
    */
    public class InstancePerContainerActivator : IActivator
    {
        public Lifetime ForLifeTime { get; } = Lifetime.InstancePerContainer;
        private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();

        public object CreateInstance(Container container, Registration factory)
        {
            if (container.Parent.HasInstance(factory.RegisteredType)) return container.Parent.Resolve(factory.RegisteredType);
            if (_instances.ContainsKey(factory.RegisteredType)) return _instances[factory.RegisteredType];
            Console.WriteLine($"InstancePerContainerActivator: creating new instance for {factory.RegisteredType.Name}");
            var parameters = factory.ConstructorDepencencies.Select(container.Resolve).ToArray();
            var instance = Activator.CreateInstance(factory.ConcreteType, parameters);
            this._instances.Add(factory.RegisteredType, instance);
            return instance;
        }

        internal bool HasInstance(Type t) => this._instances.ContainsKey(t);
    }

    /*
    Small difference with above: an activator is unique per container. So for single instance the above logic won't work.
    That's why we store the instance in the (shared) InjectionFactory. This way every container can access the instance.
     */
    public class SingleInstanceActivator : IActivator
    {
        public Lifetime ForLifeTime { get; } = Lifetime.SingleInstance;

        public object CreateInstance(Container container, Registration factory)
        {
            if (factory.SingleInstance != null) return factory.SingleInstance;
            Console.WriteLine($"SingleInstanceActivator: creating new instance for {factory.RegisteredType.Name}");
            var parameters = factory.ConstructorDepencencies.Select(container.Resolve).ToArray();
            var instance = Activator.CreateInstance(factory.ConcreteType, parameters);
            factory.SingleInstance = instance;
            return instance;
        }
    }
}
