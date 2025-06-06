using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Matter.Core;
using Microsoft.AspNetCore.Components;

namespace Matter.Controller.Components.Pages
{
    public partial class Home : ComponentBase
    {
        private readonly IMatterController _controller;

        public Home(IMatterController controller)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
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

            foreach (var matterNode in await _controller.GetNodesAsync())
            {
                var device = Diagram.Nodes.Add(new MatterNode(matterNode, position: new Point(50, 50))
                {
                    Title = matterNode.NodeId.ToString(),
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
        private void NewTable(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
        }
    }
}
