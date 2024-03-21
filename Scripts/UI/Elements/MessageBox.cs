using Godot;
using System;

public partial class MessageBox : Node
{
    [Export] private Button YesButton;
    [Export] private Button NoButton;
    private Action Confirm;
    private Action Cancel;
    void OnPressedYes()
	{
        Confirm?.Invoke();
        QueueFree();
	}
    void OnPressedNo()
    {
        Cancel?.Invoke();
        QueueFree();
    }
    
    public static void Message(Node parent, Action onYes, Action onNo)
    {
        MessageBox msgBox = ReferenceManager.Instance.MessageBox.Instantiate<MessageBox>();
        msgBox.Confirm += onYes;
        msgBox.Cancel += onNo;
        parent.AddChild(msgBox);
    }
    public static MessageBox Message(Node parent, Action onYes)
    {
        MessageBox msgBox = ReferenceManager.Instance.MessageBox.Instantiate<MessageBox>();
        msgBox.Confirm += onYes;
        parent.AddChild(msgBox);
        return msgBox;
    }
}
