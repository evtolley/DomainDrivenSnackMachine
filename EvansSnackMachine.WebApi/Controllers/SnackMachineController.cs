using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Logic.ValueObjects;
using EvansSnackMachine.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EvansSnackMachine.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnackMachineController : ControllerBase
    {
        private readonly ILogger<SnackMachineController> _logger;
        private readonly ISnackMachineRepository _snackMachineRepository;

        public SnackMachineController(
            ILogger<SnackMachineController> logger, 
            ISnackMachineRepository snackMachineRepository)
        {
            _logger = logger;
            _snackMachineRepository = snackMachineRepository;
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSnackMachine(string id)
        {
            try
            {
                var snackMachine = _snackMachineRepository.GetSnackMachine(id);

                if (snackMachine == null)
                {
                    return new NotFoundResult();
                }

                return Ok(snackMachine);
            }
            catch
            {
                return new BadRequestResult();
            }
        }



        [HttpPost]
        [Route("{id}/BuySnack")]
        public IActionResult BuySnack(string id, [FromBody]BuySnackViewModel model)
        {
            try
            {
                var snackMachine = _snackMachineRepository.GetSnackMachine(id);

                if (snackMachine == null)
                {
                    return new NotFoundResult();
                }

                snackMachine.BuySnack(model.Position);

                return Ok(_snackMachineRepository.UpdateSnackMachine(snackMachine));
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("{id}/ReturnMoney")]
        public IActionResult ReturnMoney(string id)
        {
            try
            {
                var snackMachine = _snackMachineRepository.GetSnackMachine(id);

                if (snackMachine == null)
                {
                    return new NotFoundResult();
                }

                snackMachine.ReturnMoney();

                return Ok(_snackMachineRepository.UpdateSnackMachine(snackMachine));
            }
            catch
            {
                return new BadRequestResult();
            }
        }


        [HttpPost]
        [Route("{id}/InsertMoney")]
        public IActionResult InsertMoney(string id, [FromBody] MoneyViewModel money)
        {
            try
            {
                var snackMachine = _snackMachineRepository.GetSnackMachine(id);

                if(snackMachine == null)
                {
                    return new NotFoundResult();
                }

                snackMachine.InsertMoney(money.ConvertToMoney());
                
                return Ok(_snackMachineRepository.UpdateSnackMachine(snackMachine));
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("{id}/LoadSnacks")]
        public IActionResult LoadSnacks(string id, [FromBody]LoadSnackViewModel model)
        {
            try
            {
                var snackMachine = _snackMachineRepository.GetSnackMachine(id);

                if (snackMachine == null)
                {
                    return new NotFoundResult();
                }

                snackMachine.LoadSnacks(model.Position, new SnackPile(new Snack(model.SnackName), model.Quantity, model.Price));

                return Ok(_snackMachineRepository.UpdateSnackMachine(snackMachine));
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("{id}/LoadMoney")]
        public IActionResult LoadMoney(string id, [FromBody]MoneyViewModel money)
        {
            try
            {
                var snackMachine = _snackMachineRepository.GetSnackMachine(id);

                if (snackMachine == null)
                {
                    return new NotFoundResult();
                }

                snackMachine.LoadMoney(money.ConvertToMoney());

                return Ok(_snackMachineRepository.UpdateSnackMachine(snackMachine));
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create()
        {
            try
            {
                return Ok(_snackMachineRepository.CreateSnackMachine());
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}
