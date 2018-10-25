using System;
using System.Linq;
using System.Threading.Tasks;

namespace InjectionByExample
{
    public enum Lifetime
    {
        NewInstance = 0,
        InstancePerContainer = 1,
        SingleInstance = 3
    }
    public class InjectionFactory
    {
        public Type RegisteredType { get; private set; }
        public Type[] ConstructorDepencencies { get; private set; }
        public Type ConcreteType { get; private set; }
        public Lifetime Lifetime { get; private set; }
        private object _instance;

        public InjectionFactory(Type registeredType, Type concreteType, Lifetime lifetime = Lifetime.NewInstance)
        {
            this.RegisteredType = registeredType;
            this.ConcreteType = concreteType;
            this.Lifetime = lifetime;
        }
        internal Task AnalyzeDependencies() => Task.Run(() =>
              {
                  var firstConstructor = this.ConcreteType.GetConstructors().FirstOrDefault();
                  this.ConstructorDepencencies = firstConstructor == null
                  ? new Type[0]
                  : firstConstructor.GetParameters().Select(pm => pm.ParameterType).ToArray();
              });
        public object CreateInstance(Container container)
        {
            if (this._instance != null)
            {
                Console.WriteLine($"InjectionFactory: instance for {this.RegisteredType.Name} already exists, returning {this._instance}");
                return this._instance;
            }
            var parameters = this.ConstructorDepencencies.Select(dep => container.Resolve(dep)).ToArray();
            var instance = Activator.CreateInstance(this.ConcreteType, parameters);
            if (this.Lifetime == Lifetime.SingleInstance) this._instance = instance;
            return instance;
        }
    }
}