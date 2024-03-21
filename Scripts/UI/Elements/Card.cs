using Godot;
using System;
using System.Diagnostics;

public partial class Card : Control
{
    [Export] Label PromptText;

    public delegate void OnCardSlide(bool wasSlidRight);
    public OnCardSlide OnCardPressAndSlide;
    public Action OnDisappeared;

    private Vector2 StartPressPos;
    private Vector2 EndPressPos;
    private Stopwatch DragTime;
    private float AcceptableDragTime = 1f;
    public override void _Ready()
    {
        InitAnimation();
    }
    
    public void SetPrompt(string prompt)
	{
        PromptText.Text = prompt;
    }

	public void InitAnimation()
	{
        Scale = new Vector2(0f, 0f);
        Tween tween = CreateTween();
        tween.SetTrans(Tween.TransitionType.Quart);
        tween.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(this, (string)PropertyName.Scale, new Vector2(1f, 1f), 0.5f);
    }

    public void Exit(bool correct)
    {
        Scale = new Vector2(1f, 1f);
        Tween tween = CreateTween();
        tween.SetTrans(Tween.TransitionType.Quart);
        tween.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(this, (string)PropertyName.Scale, new Vector2(0f, 0f), 0.5f);
        tween.Finished += () => {
            OnDisappeared?.Invoke();
            QueueFree();
        };
        if (correct)
            Modulate = new Color(0.3f, 1f, 0.3f);
        else
            Modulate = new Color(1f, 0.3f, 0.3f);

        GD.Print(correct);
    }


    #region Signals
    public void OnButtonDown()
    {
        DragTime = new Stopwatch();
        DragTime.Start();
        StartPressPos = GetViewport().GetMousePosition();
    }

    public void OnButtonUp()
    {
        DragTime.Stop();
        EndPressPos = GetViewport().GetMousePosition();
        if (DragTime.ElapsedMilliseconds > AcceptableDragTime * 1000f) return;

        Vector2 dir = (EndPressPos - StartPressPos).Normalized();

        OnCardPressAndSlide?.Invoke(dir.Dot(new Vector2(1f, 0f)) > 0f);
    }
    #endregion
}
