using System;
using System.Linq;

namespace InjectionByExample
{

    public class Registration
    {
        public Type RegisteredType { get; private set; }
        public Type[] ConstructorDepencencies { get; private set; }
        public Type ConcreteType { get; private set; }
        public Lifetime Lifetime { get; private set; }
        public object SingleInstance { get; set; }

        public Registration(Type registeredType, Type concreteType, Lifetime lifetime = Lifetime.NewInstance)
        {
            this.RegisteredType = registeredType;
            this.ConcreteType = concreteType;
            this.Lifetime = lifetime;
        }

        internal void AnalyzeDependencies()
        {
            var firstConstructor = this.ConcreteType.GetConstructors().FirstOrDefault();
            this.ConstructorDepencencies = firstConstructor == null
            ? new Type[0]
            : firstConstructor.GetParameters().Select(pm => pm.ParameterType).ToArray();
            Console.WriteLine($"Done analyzing the dependencies for {this.ConcreteType.Name}");
        }
    }
}