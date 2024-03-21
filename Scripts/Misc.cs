using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiztomize.Scripts
{
    public static class Misc
    {
        public static List<T> GetRandomElements<T>(List<T> list,uint amountToPick)
        {
            amountToPick = (uint)Math.Clamp(amountToPick,0, list.Count);
            List<T> listCopy = new List<T>(list);
            List<T> results = new List<T>();
            int amountPicked = 0;

            for(int i = 0; i < amountToPick; i++)
            {
                if (amountPicked >= amountToPick)
                    break;

                uint randomIndex = GD.Randi() % ((uint)listCopy.Count);

                GD.Print($"index: {randomIndex}  count;{(uint)listCopy.Count}   amount:{amountToPick}");

                T item = listCopy[(int)randomIndex];
                listCopy.RemoveAt((int)randomIndex);

                results.Add(item);
                amountPicked++;
            }

            return results;
        }

        public static List<T> GetRandomElements<T>(List<T> list, int amountToPick)
        {
            return GetRandomElements<T>(list, (uint)amountToPick);
        }
    }
}
