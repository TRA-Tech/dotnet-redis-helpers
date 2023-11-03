namespace Playground.Services
{
    public class Service1 : IDisposable
    {
        public string Name { get; set; }

        private bool _disposed = false;

        public bool Disposed { get => _disposed; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
