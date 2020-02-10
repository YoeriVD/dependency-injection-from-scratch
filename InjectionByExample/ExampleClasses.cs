namespace InjectionByExample
{
    public interface ICar
    {
    }

    public class Car : ICar
    {
        private static int InstanceCount;
        private readonly IEngine engine;
        private readonly int _instanceId;

        public Car(IEngine engine)
        {
            InstanceCount++;
            _instanceId = InstanceCount;
            this.engine = engine;
        }

        public override string ToString()
        {
            return $"This car has id {_instanceId} and engine: {engine}";
        }
    }

    public class Driver
    {
        private static int InstanceCount;
        private readonly ICar car;
        private readonly int _instanceId;

        public Driver(ICar car)
        {
            InstanceCount++;
            _instanceId = InstanceCount;
            this.car = car;
        }

        public override string ToString()
        {
            return $"This driver with id {_instanceId} has car: {car}";
        }
    }

    public interface IEngine
    {
    }

    public class Engine : IEngine
    {
        private static int InstanceCount;
        private readonly int _instanceId;

        public Engine()
        {
            InstanceCount++;
            _instanceId = InstanceCount;
        }

        public override string ToString()
        {
            return $"Engine with id {_instanceId}";
        }
    }
}