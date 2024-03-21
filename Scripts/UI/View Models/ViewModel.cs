using Godot;
using Quiztomize.Scripts.Data_Access;
using System;

namespace Quiztomize.Scripts.View_Models
{
    public partial class ViewModel : Node
    {
        public Action OnRedraw;

        public override void _Ready()
        {
            UpdateUI();
        }

        public virtual void UpdateUI()
        {
            OnRedraw?.Invoke();
        }
    }
}
