using DT.Api.Application.Base;
using DT.Api.Application.Bills;
using DT.Api.Authorizations;
using DT.Domain.Interfaces;
using DT.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DT.Api
{
    public static class Bootstrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthUser, AuthUser>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IHttpAcessorService, HttpAcessorService>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.RegisterBillDI();

        }
    }
}
