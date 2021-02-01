using DT.Api.Responses;
using DT.Domain.Interfaces;
using DT.Infra.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DT.Api.Application.Bills
{
    public static class BillRegisterDI
    {
        public static void RegisterBillDI(this IServiceCollection services)
        {
            
            services.AddScoped<IRequestHandler<RegisterBillCommand, CommandResponse>, BillCommandHandler>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillService, BillService>();
        }
    }
}
