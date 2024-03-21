using Godot;
using Quiztomize.Scripts;

public partial class View : Control
{
    [Signal] public delegate void ViewSwitchedEventHandler(View view);

    [ExportGroup("View Navigation")]
    [Export] public View BackView;
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(Consts.Inputs.UI.Back))
        {
            if (!IsVisibleInTree()) return;

            GetViewport().SetInputAsHandled();
            Back();
        }
            
    }

    public virtual void Back()
    {
        if (BackView == null) return;
        EmitSignal(SignalName.ViewSwitched, BackView);
    }

    /// <summary>
    /// Called by the view switcher when the view is switched to
    /// </summary>
    public virtual void OnViewLoaded() 
    {
    }
}
