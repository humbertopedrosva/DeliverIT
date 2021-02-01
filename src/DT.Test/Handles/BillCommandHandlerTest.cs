using AutoMapper;
using DT.Api.Application.Bills;
using DT.Api.Responses;
using DT.Domain.Entities;
using DT.Domain.Interfaces;
using DT.Test.Fakes;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DT.Test.Handles
{
    public class BillCommandHandlerTest : DependencyInjectionTest
    {
        private BillCommandHandler _commandHandler;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IMapper _mapper;
        private readonly IBillRepository _billRepository;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly IEnumerable<IUserValidator<IdentityUser>> _userValidators;
        private readonly IEnumerable<IPasswordValidator<IdentityUser>> _passwordValidators;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IdentityErrorDescriber _errors;
        private readonly ILogger<UserManager<IdentityUser>> _logger;

        public BillCommandHandlerTest()
        {
            _userStore = Substitute.For<IUserStore<IdentityUser>>();
            _optionsAccessor = Substitute.For<IOptions<IdentityOptions>>();
            _passwordHasher = Substitute.For<IPasswordHasher<IdentityUser>>();
            _userValidators = Substitute.For<IEnumerable<IUserValidator<IdentityUser>>>();
            _passwordValidators = Substitute.For<IEnumerable<IPasswordValidator<IdentityUser>>>();
            _keyNormalizer = Substitute.For<ILookupNormalizer>();
            _errors = Substitute.For<IdentityErrorDescriber>();
            _logger = Substitute.For<ILogger<UserManager<IdentityUser>>>();
            _optionsAccessor = Substitute.For<IOptions<IdentityOptions>>();
            _userManager = new UserManager<IdentityUser>
                (_userStore, 
                _optionsAccessor, 
                _passwordHasher, 
                _userValidators, 
                _passwordValidators, 
                _keyNormalizer, 
                _errors, 
                _serviceProvider, 
                _logger);
            _mapper = Resolve<IMapper>();
            _billRepository = Substitute.For<IBillRepository>();
            _commandHandler = new BillCommandHandler(_serviceProvider, _userManager, _mapper, _billRepository);
        }


        [Fact]
        public async Task Should_register_bill()
        {
            var command = RegisterBillCommandFake.Default();

            var defaultUser = new IdentityUser
            {
                UserName = "teste@gmail.com",
                Email = "teste@gmail.com",
                EmailConfirmed = true
            };

             _userManager.FindByIdAsync(Arg.Any<string>()).Returns(defaultUser);

            _billRepository.AddAsync(Arg.Any<Bill>()).Returns(true);

            var cancelationToken = CancellationToken.None;

            var expected = new CommandResponse
            {
                Message = "Conta registrada com sucesso",
                Sucess = true
            };

            var result =  await _commandHandler.Handle(command, cancelationToken);

            result.Sucess.Should().Be(expected.Sucess);
        }

    }
}
