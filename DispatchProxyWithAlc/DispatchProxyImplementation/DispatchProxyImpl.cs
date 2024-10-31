using System.Reflection;

namespace DispatchProxyImplementation
{
    public class DispatchProxyImpl : DispatchProxy
    {
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"Method {targetMethod.Name} is being called");
            // Return a default value if the method has a return type
            if (targetMethod.ReturnType == typeof(void))
            {
                return null;
            }
            else
            {
                return Activator.CreateInstance(targetMethod.ReturnType);
            }
        }
    }
}



