using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpTextRPGbyNick
{
    public class Scene
    {
        public string name;
        public string description;
        public bool hasVisited = false;
        public int sceneID;

        public List<Connection> exits = new List<Connection>();

        public Scene()
        {
            //this.name = name;
            //this.doors.Add((int)Door.arch);
        }
        public void WriteSceneName()
        {
            Console.WriteLine(this.name);
            foreach (char c in this.name) { Console.Write('-'); }
            Console.Write('\n');
        }
        public void WriteSceneExits()
        {
            if(this.exits != null)
            {
                for(int i=0; i<this.exits.Count; i++)
                {
                    if (i > 0) { Console.WriteLine(i + ". " + this.exits[i].nextScene.name); }
                }
            }
        }
    }
}
