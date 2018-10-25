namespace InjectionByExample
{
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
}