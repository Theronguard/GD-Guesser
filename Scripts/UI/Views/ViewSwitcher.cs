using Godot;
using Quiztomize.Scripts;
using System;

public partial class ViewSwitcher : Node
{
    public override void _Ready()
    {
        foreach(Control node in GetChildren())
        {
            View view = node as View;
            if (node == null) continue;

            view.ViewSwitched += Switch;
        }
    }

    public void Switch(View viewToSwitch)
	{
        foreach (Control node in GetChildren())
        {
            View view = node as View;
            if (view == null) continue;

            bool isActive = (view == viewToSwitch);
            node.Visible = isActive;
			node.SetProcess(isActive);
            view.OnViewLoaded();
        }
	}
}
