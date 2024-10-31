using DispatchProxyImplementation;
using System.Reflection;
using System.Runtime.Loader;

namespace ClientLibrary
{
    public class Worker
    {
        public Worker()
        {
        }

        public void DoSomething()
        {
            var thisAlcString = GetAlcString(this);
            Console.WriteLine("Worker type is from ALC " + thisAlcString);
            var proxy = DispatchProxy.Create(typeof(IWorker), typeof(DispatchProxyImpl));
            var proxyType = proxy.GetType();
            Console.WriteLine($"Proxy type: {proxyType.FullName}");
            var proxyAlcString = GetAlcString(proxy);
            if (proxyAlcString.Equals(thisAlcString, StringComparison.Ordinal))
            {
                Console.WriteLine("Proxy is from the same ALC as Worker type");
            }
            else
            {
                Console.WriteLine("Proxy is from a different ALC than Worker type: " + proxyAlcString);
            }
            var interfaces = proxyType.GetInterfaces();
            foreach(var i in interfaces)
            {
                Console.WriteLine($"\tImplements: {i.FullName}");
                var interfaceAlcString = GetAlcString(i);
                if (interfaceAlcString.Equals(thisAlcString, StringComparison.Ordinal))
                {
                    Console.WriteLine("\tInterface is from the same ALC as Worker type");
                }
                else
                {
                    // If the generic overload of DispatchProxy.Create is used, a type cast exception gets thrown
                    // as the interface implemented isn't the one requested.
                    Console.WriteLine("\tInterface is from a different ALC than Worker type: " + interfaceAlcString);
                }
            }
        }

        public static string GetAlcString(object obj )
        {
            return GetAlcString(obj.GetType());
        }

        public static string GetAlcString(Type type)
        {
            return AssemblyLoadContext.GetLoadContext(type.Assembly).ToString();
        }
    }
}
