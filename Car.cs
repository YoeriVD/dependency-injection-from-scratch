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
}