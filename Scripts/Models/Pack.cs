using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiztomize.Scripts.Models
{
    public partial class Pack : Resource
    {
        [Export] public string Name;
        [Export] public Godot.Collections.Array<string> Prompts = new Godot.Collections.Array<string>();
        [Export] public long SaveTime = 0;
        public Pack() : this("") { }

        public Pack(string name)
        {
            Name = name;
        }
        public Pack(string name, Godot.Collections.Array<string> prompts)
        {
            Name = name;
            Prompts = prompts;
        }
    }
}
