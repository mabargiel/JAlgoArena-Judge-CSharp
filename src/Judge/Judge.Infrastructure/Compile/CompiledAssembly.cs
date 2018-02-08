using System;
using System.Reflection;

namespace Judge.Infrastructure.Compile
{
    public class CompiledAssembly : MarshalByRefObject
    {
        private readonly Assembly _assembly;

        public CompiledAssembly(Assembly assembly)
        {
            _assembly = assembly;
        }

        public object InvokeMethod(string className, string methodName, object[] parameters)
        {
            var method = _assembly.GetType(className).GetMethod(methodName);
            return method.Invoke(null, parameters);
        }
    }
}