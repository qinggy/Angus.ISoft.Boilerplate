using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Structuremapper
{
    public class ObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder;
        private static Container _container;

        static ObjectFactory()
        {
            _containerBuilder = new Lazy<Container>(new Func<Container>(defaultContainer), LazyThreadSafetyMode.ExecutionAndPublication);
            _container = new Container();
        }

        public static IContainer Container
        {
            get
            {
                return _containerBuilder.Value;
            }
        }

        private static Container defaultContainer()
        {
            return _container;
        }

        public static void Initialize<T>() where T : Registry, new()
        {
            _container.Configure(delegate(ConfigurationExpression x)
            {
                x.AddRegistry<T>();
            });
        }

    }
}
