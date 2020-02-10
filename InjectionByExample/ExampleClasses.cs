namespace InjectionByExample
{
    public interface ICar
    {
    }

    public class Car : ICar
    {
        private static int InstanceCount = 0;
        private readonly IEngine engine;
        private int _instanceId;

        public Car(IEngine engine)
        {
            InstanceCount++;
            _instanceId = InstanceCount;
            this.engine = engine;
        }

        public override string ToString() => $"This car has id {this._instanceId} and engine: {engine}";
    }

    public class Driver
    {
        private static int InstanceCount = 0;
        private readonly ICar car;
        private int _instanceId;

        public Driver(ICar car)
        {
            InstanceCount++;
            _instanceId = InstanceCount;
            this.car = car;
        }

        public override string ToString() => $"This driver with id {this._instanceId} has car: {this.car}";
    }

    public interface IEngine
    {
    }

    public class Engine : IEngine
    {
        private static int InstanceCount = 0;
        private int _instanceId;

        public Engine()
        {
            InstanceCount++;
            _instanceId = InstanceCount;
        }

        public override string ToString() => $"Engine with id {this._instanceId}";
    }
}