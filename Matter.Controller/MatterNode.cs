using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace Matter.Controller
{
    public class MatterNode : NodeModel
    {
        private readonly Matter.Core.Node _node;

        public MatterNode(Matter.Core.Node node, Point position) : base(position)
        {
            Title = "Device";
            _node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public string NodeId => _node.NodeId.ToString();

        public bool IsConnected => _node.IsConnected;
    }
}
