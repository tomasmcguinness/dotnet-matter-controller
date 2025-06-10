using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Models.Base;
using Microsoft.AspNetCore.Components;

namespace Matter.Controller.Components
{
    public partial class DeviceNodeDetails : IDisposable
    {
        private MatterNode _selectedNode;

        [CascadingParameter]
        public Diagram Diagram { get; set; }

        public void Dispose()
        {
            Diagram.SelectionChanged -= Diagram_SelectionChanged;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Diagram.SelectionChanged += Diagram_SelectionChanged;
        }

        private void Diagram_SelectionChanged(SelectableModel model)
        {
            if (model is MatterNode tm)
            {
                _selectedNode = model.Selected ? tm : null;
                StateHasChanged();
            }
        }

    }
}
