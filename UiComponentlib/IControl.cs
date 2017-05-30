namespace Rootech.UI.Component
{
    public interface IControl
    {
        int Depth { set; get; }
        MouseButtonState MouseButtonState { set; get; }

        ComponentVisualization Visualization { set; get; }
    }
}
