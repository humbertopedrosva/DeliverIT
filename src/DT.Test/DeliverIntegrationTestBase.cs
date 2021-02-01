using DT.Api;
using DT.Api.Configuration.AutoMapper;
using DT.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace DT.Test
{
    public class DeliverIntegrationTestBase
    {
        private IServiceCollection _services { get; }
        protected ServiceProvider _serviceProvider { get; }
        protected DTContext _context { get; set; }

        public DeliverIntegrationTestBase()
        {
            var options = new DbContextOptionsBuilder<DTContext>();

            _services = new ServiceCollection();

            _services.AddSetupAutoMapper();

            _services.AddDbContext<DTContext>();

            _services.AddEntityFrameworkInMemoryDatabase().AddDbContext<DTContext>((_serviceProvider, options) =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString().Replace("-", ""))
                .UseInternalServiceProvider(_serviceProvider);
            });

            _services.RegisterServices();
            _serviceProvider = _services.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<DTContext>();
        }

        protected T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
        protected T ResolveCollection<T>() => ActivatorUtilities.CreateInstance<T>(_serviceProvider);

        protected void PersistEntities(params object[] entities)
        {
            foreach (var entity in entities)
            {
                _context.Add(entity);
            }
            _context.SaveChanges();
        }
    }
}
