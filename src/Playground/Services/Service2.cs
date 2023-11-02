namespace Playground.Services
{
    public class Service2
    {
        private readonly Service1 _service1;

        public Service2(Service1 service1)
        {
            _service1 = service1;
        }
    }
}
