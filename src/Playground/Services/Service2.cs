namespace Playground.Services
{
    public class Service2
    {
        public readonly Service1 Service1;

        public Service2(Service1 service1)
        {
            Service1 = service1;
            Service1.Name = "Test";
        }

        public string GetName()
        {
            return Service1.Name;
        }
    }
}
