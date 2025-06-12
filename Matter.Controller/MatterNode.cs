using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Org.BouncyCastle.Utilities.Encoders;

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

        public string NodeId => Hex.ToHexString(_node.NodeId.ToByteArrayUnsigned().Reverse().ToArray()).ToUpper();

        public async Task InterrogateAsync()
        {
            await _node.FetchDescriptionsAsync();
        }

        public bool IsConnected => _node.IsConnected;
    }
}
