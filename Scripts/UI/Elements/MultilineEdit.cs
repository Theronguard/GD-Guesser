using Godot;
using System;

[Tool]
public partial class MultilineEdit : Label
{
    [Export] LineEdit Parent;
    private StyleBoxFlat Normal;
    private StyleBoxFlat Focused;
	public override void _Ready()
	{
        Parent.AddThemeColorOverride("font_color", new Color(0f, 0f, 0f, 0f));
        AddThemeFontSizeOverride("font_size", Parent.GetThemeFontSize("font_size"));

        Normal = Parent.GetThemeStylebox("normal") as StyleBoxFlat;
        Focused = Parent.GetThemeStylebox("focus").Duplicate(true) as StyleBoxFlat;
        Parent.AddThemeStyleboxOverride("focus", Focused);
    }

	public override void _Process(double delta)
	{
		if (Parent == null) return;
        Text = Parent.Text;

        if (Engine.IsEditorHint()) return;

    }
}
