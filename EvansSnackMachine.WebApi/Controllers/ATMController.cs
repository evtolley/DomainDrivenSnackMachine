using ATM.Logic.Interfaces;
using EvansSnackMachine.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EvansSnackMachine.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ATMController : ControllerBase
    {
        private readonly ILogger<ATMController> _logger;
        private readonly IATMRepository _atmRepository;

        public ATMController(
            ILogger<ATMController> logger,
            IATMRepository atmRepository)
        {
            this._logger = logger;
            this._atmRepository = atmRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetATM(string id)
        {
            try
            {
                var atm = _atmRepository.GetATM(id);

                if (atm == null)
                {
                    return new NotFoundResult();
                }

                return Ok(atm);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create()
        {
            try
            {
                return Ok(_atmRepository.CreateATM());
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("{id}/withdraw")]
        public IActionResult Withdraw(string id, [FromBody] AmountViewModel model)
        {
            try
            {
                var atm = _atmRepository.GetATM(id);

                if (atm == null)
                {
                    return new NotFoundResult();
                }

                atm.TakeMoney(model.Amount);

                return Ok(_atmRepository.UpdateATM(atm));
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("{id}/loadmoney")]
        public IActionResult LoadMoney(string id, [FromBody] MoneyViewModel model)
        {
            try
            {
                var atm = _atmRepository.GetATM(id);

                if (atm == null)
                {
                    return new NotFoundResult();
                }

                atm.LoadMoney(model.ConvertToMoney());

                return Ok(_atmRepository.UpdateATM(atm));
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}