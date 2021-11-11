using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.DependencyInjection
{
    internal class DependencyInjection
    {
        private static bool _builed = false;
        private static object _mutex=new object();
        private static readonly IServiceCollection _services=new ServiceCollection();
        public static IServiceProvider? ServiceProvider { get;private set; }

        protected DependencyInjection()
        {
        }

        internal static void BuildServiceProvider()
        {
            lock (_mutex)
            {
                if(!_builed)
                {
                    _builed = true;
                    ServiceProvider = _services.BuildServiceProvider();
                }
            }
        }

        internal IServiceCollection AddSigleton<T>() where T : class
        {
            return _services.AddSingleton<T>();
        }

        internal IServiceCollection AddTransient<T>() where T : class
        {
            return _services.AddTransient<T>();
        }
    }
}
