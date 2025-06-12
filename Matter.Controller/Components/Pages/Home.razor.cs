using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Matter.Core;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Utilities.Encoders;

namespace Matter.Controller.Components.Pages
{
    public partial class Home : ComponentBase
    {
        private readonly IMatterController _controller;
        private readonly NavigationManager _navigationManager;

        public Home(IMatterController controller, NavigationManager navigationManager)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _navigationManager = navigationManager;
        }

        private BlazorDiagram Diagram { get; set; } = null!;

        protected override async void OnInitialized()
        {
            var options = new BlazorDiagramOptions
            {
                AllowMultiSelection = true,
                Zoom = {
                    Enabled = false,
                },
                AllowPanning = true,
                Links = {
                    DefaultRouter = new NormalRouter(),
                    DefaultPathGenerator = new SmoothPathGenerator()
                },
            };

            Diagram = new BlazorDiagram(options);

            Diagram.RegisterComponent<MatterNode, DeviceNode>();

            Diagram.PointerDoubleClick += Diagram_PointerDoubleClick;

            foreach (var matterNode in await _controller.GetNodesAsync())
            {
                var device = Diagram.Nodes.Add(new MatterNode(matterNode, position: new Point(50, 50))
                {
                    Title = Hex.ToHexString(matterNode.NodeId.ToByteArrayUnsigned()),
                });
            }

            //var switchDevice = Diagram.Nodes.Add(new Device(position: new Point(50, 50))
            //{
            //    Title = "Switch 1",
            //});
            //var secondNode = Diagram.Nodes.Add(new NodeModel(position: new Point(200, 100))
            //{
            //    Title = "Node 2"
            //});

            //secondNode.AddPort();
            //secondNode.AddPort();
            //secondNode.AddPort();

            //var sourceAnchor = new ShapeIntersectionAnchor(switchDevice);

            //// The connection point will be the port's position
            ////
            //var targetAnchor = new SinglePortAnchor(leftPort);
            //var link = Diagram.Links.Add(new LinkModel(sourceAnchor, targetAnchor));
        }

        private void Diagram_PointerDoubleClick(Blazor.Diagrams.Core.Models.Base.Model? arg1, Blazor.Diagrams.Core.Events.PointerEventArgs arg2)
        {
            var node = arg1 as MatterNode;

            if (node is not null)
            {
                _navigationManager.NavigateTo($"/nodes/{node.NodeId}");
            }
        }

        private void NewTable(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
        }
    }
}
