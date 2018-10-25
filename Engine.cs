namespace InjectionByExample
{
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