using Microsoft.AspNetCore.Components;

namespace Matter.Controller.Components
{
    public partial class DeviceNode
    {
        [Parameter]
        public MatterNode Node { get; set; }
    }
}