using System;
using System.Linq;

namespace InjectionByExample
{
    public class Registration
    {
        public Registration(Type registeredType, Type concreteType, Lifetime lifetime = Lifetime.NewInstance)
        {
            RegisteredType = registeredType;
            ConcreteType = concreteType;
            Lifetime = lifetime;
        }

        public Type RegisteredType { get; }
        public Type[] ConstructorDepencencies { get; private set; }
        public Type ConcreteType { get; }
        public Lifetime Lifetime { get; }
        public object SingleInstance { get; set; }

        internal void AnalyzeDependencies()
        {
            var firstConstructor = ConcreteType.GetConstructors().FirstOrDefault();
            ConstructorDepencencies = firstConstructor == null
                ? new Type[0]
                : firstConstructor.GetParameters().Select(pm => pm.ParameterType).ToArray();
            Console.WriteLine($"Done analyzing the dependencies for {ConcreteType.Name}");
        }
    }
}