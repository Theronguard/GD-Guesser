using Godot;
using Quiztomize.Scripts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiztomize.Scripts.Data_Access
{
    internal static class PackDA
    {
        const string FilePath = "user://Packs//";
        const string FileExtension = ".tres";
        public static Pack Get(string packName) 
        {
            DirAccess currentDir = DirAccess.Open(FilePath);
            if (currentDir == null) return null;

            string[] files = currentDir.GetFiles();
            if (files == null) return null;

            foreach (string fileName in files)
            {
                string packFileName = Path.GetFileNameWithoutExtension(fileName);

                if (packFileName != packName) continue;

                return ResourceLoader.Load(FilePath + packName + FileExtension) as Pack;
            }

            return null;
        }

        public static List<Pack> GetAll()
        {
            List<Pack> packs = new List<Pack>();

            DirAccess currentDir = DirAccess.Open(FilePath);
            if (currentDir == null) return packs;

            string[] files = currentDir.GetFiles();
            if (files == null) return packs;

            foreach (string fileName in files)
            {
                string packName = Path.GetFileNameWithoutExtension(fileName);
                Pack pack = ResourceLoader.Load(FilePath + packName + FileExtension) as Pack;

                if (pack == null) continue;

                packs.Add(pack);
            }
            packs.Sort((x1, x2) => x1.SaveTime.CompareTo(x2.SaveTime));
            return packs;
        }
        
        public static void Delete(string packName)
        {
            DirAccess currentDir = DirAccess.Open(FilePath);
            currentDir.Remove(packName + FileExtension);
        }
        
        public static Error Save(Pack pack)
        {
            DirAccess dir = DirAccess.Open(FilePath);
            if (dir == null)
            {
                dir = DirAccess.Open("user://");
                dir.MakeDir("Packs");
            }

            if (!pack.Name.IsValidFileName()) return Error.FileCantWrite;

            pack.SaveTime = DateTime.Now.Ticks;

            Error error = ResourceSaver.Save(pack, FilePath + pack.Name + FileExtension);
            return error;
        }
    }
}
