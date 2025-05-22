using Matter.Core;
using Microsoft.AspNetCore.Mvc;

namespace Matter.Controller.Server.Controllers
{
    [ApiController]
    [Route("api/matter/nodes")]
    public class MatterNodesController : ControllerBase
    {
        private readonly IMatterController _matterController;
        private readonly ILogger<MatterController> _logger;

        public MatterNodesController(IMatterController matterController, ILogger<MatterController> logger)
        {
            _matterController = matterController;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<MatterNodeModel>> Get()
        {
            var nodes = await _matterController.GetNodesAsync();

            var models = nodes.Select(x => new MatterNodeModel()
            {
                Id = x.NodeId.ToString(),
                Position = new Position()
                {
                    X = 0,
                    Y = 0
                },
                Data = new()
                {
                    Label = x.NodeId.ToString()
                }
            });

            return models;
        }
    }
}
