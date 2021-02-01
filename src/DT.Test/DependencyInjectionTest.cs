using DT.Api;
using DT.Api.Configuration.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DT.Test
{
    public class DependencyInjectionTest
    {
        private IServiceCollection _services { get; }
        protected ServiceProvider _serviceProvider { get; }

        public DependencyInjectionTest()
        {
            _services = new ServiceCollection();

            _services.AddSetupAutoMapper();

            _services.RegisterServices();
            _serviceProvider = _services.BuildServiceProvider();
        }

        protected T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
        protected T ResolveCollection<T>() => ActivatorUtilities.CreateInstance<T>(_serviceProvider);
     
    }
}
