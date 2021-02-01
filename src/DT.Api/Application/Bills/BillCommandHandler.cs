using AutoMapper;
using DT.Api.Application.Base;
using DT.Api.Responses;
using DT.Domain.Entities;
using DT.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DT.Api.Application.Bills
{
    public class BillCommandHandler : CommandHandler,
        IRequestHandler<RegisterBillCommand, CommandResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IBillRepository _billRepository;

        public BillCommandHandler(
            IServiceProvider serviceProvider, 
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IBillRepository billRepository)
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _mapper = mapper;
            _billRepository = billRepository;
        }
        public async Task<CommandResponse> Handle(RegisterBillCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid(_serviceProvider)) return new CommandResponse { ValidationResult = command.ValidationResult, Sucess = false };

            var user = await _userManager.FindByIdAsync(command.UserId);

            if(user == null)
                return new CommandResponse { ValidationResult = command.ValidationResult, Sucess = false , Message = "Usuario não encontrado"};

            var bill =  _mapper.Map<Bill>(command);

            bill.AddUser(user);

            var result = await _billRepository.AddAsync(bill);

            if (!result)
            {
                AddError("Houve um erro ao persistir os dados");
                return new CommandResponse { ValidationResult = ValidationResult, Sucess = false };
            }

            return new CommandResponse
            {
                Message = "Conta registrada com sucesso",
                Sucess = true
            };
        }
    }
}
