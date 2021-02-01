using DT.Api.Application.Base;
using DT.Api.Application.Bills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DT.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]/")]
    public class BillController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;
        private readonly IHttpAcessorService _httpAcessorService;
        private readonly IBillService _billService;

        public BillController(IMediatorHandler mediatorHandler, IHttpAcessorService httpAcessorService, IBillService billService)
        {
            _mediator = mediatorHandler;
            _httpAcessorService = httpAcessorService;
            _billService = billService;
        }

        [Authorize(Roles = "AdminMaster, User")]
        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> AddClinic([FromBody]RegisterBillCommand command)
        {
            command.UserId = _httpAcessorService.GetUserId().ToString();
            return CustomResponse(await _mediator.SendCommand(command));
        }

        [Authorize(Roles = "AdminMaster, User")]
        [HttpGet]
        [ActionName("")]
        public async Task<IActionResult> GetAllByUser()
        {
            return CustomResponse(await _billService.GetAllBillsByUser(_httpAcessorService.GetUserId().ToString()));
        }

        [Authorize(Roles = "AdminMaster")]
        [HttpGet]
        [ActionName("getallmaster")]

        public async Task<IActionResult> GetAll()
        {
            return CustomResponse(await _billService.GetAllBills());
        }

        [Authorize(Roles = "AdminMaster, User")]
        [HttpGet]
        [ActionName("bill")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            return CustomResponse(await _billService.GetByIdByUser(id, _httpAcessorService.GetUserId().ToString()));
        }
    }
}
