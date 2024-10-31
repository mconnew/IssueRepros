namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Show that the second ALC instance generates a proxy for the interface
            // from the wrong ALC instance, which would normally cause an invalid cast exception
            for (int i = 0; i < 2; i++)
            {
                CreateWorkerInAlcAndCleanup(collectable: false);
                Console.WriteLine();
            }

            // Show that you can't create a proxy when the base proxyType is in a non-collectable ALC
            // (in this case the default ALC) and the interface is in a collectable ALC.
            CreateWorkerInAlcAndCleanup(collectable: true);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void CreateWorkerInAlcAndCleanup(bool collectable)
        {
            using (var alc = new DisposableALC(collectable))
            {
                var thisAssemblyPath = typeof(Program).Assembly.Location;
                string appAssemblyFolder = Path.GetDirectoryName(thisAssemblyPath);
                string clientLibraryPath = Path.Combine(appAssemblyFolder, "ClientLibrary.dll");
                var assembly = alc.LoadFromAssemblyPath(clientLibraryPath);
                Type type = assembly.GetType("ClientLibrary.Worker");
                object instance = Activator.CreateInstance(type);
                type.GetMethod("DoSomething").Invoke(instance, null);
            }
        }
    }
}
