using System.Threading.Tasks;
using Application.Items.Commands.CreateItem;
using Application.Items.Queries.GetItemsList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.Abstracts;

namespace WebApi.Controllers
{
    public class ItemsController : BaseController
    {
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            _logger.LogInformation("Get all items");
            var response = await Mediator.Send(new GetItemsListQuery());

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command)
        {
            _logger.LogInformation("Create item");

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
