using System.Reflection;
using System.Runtime.Loader;

namespace ConsoleApp
{
    internal class DisposableALC : AssemblyLoadContext, IDisposable
    {
        private bool _disposed = false;

        public DisposableALC(bool isCollectible = true) : base(isCollectible) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here
                }

                if (IsCollectible)
                {
                    Unload();
                }
                _disposed = true;
            }
        }

        ~DisposableALC()
        {
            Dispose(false);
        }
    }
}