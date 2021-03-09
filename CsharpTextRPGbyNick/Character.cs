using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CsharpTextRPGbyNick
{
    public class Character
    {
        public string name;
        public int curHealth;
        public int maxHealth;
        public int curMana;
        public int maxMana;
        //inventory list
        //keychain list

        public int currentSceneID;
        public List<Character> party = new List<Character>();

        //Attributes
        public int strength;
        public int dexterity;
        //...

        //Equipment
        //eqWeapon
        //eqHelmet
        //eqChest
        //eqArms
        //eqShield
        public void DisplayPartyStatus(List<Scene> scenes)
        {
            this.DisplayCharacterStatus(scenes);
            foreach (Character c in this.party) { c.DisplayCharacterStatus(scenes); }
            Console.Write('\n');
        }
        public void DisplayCharacterStatus(List<Scene> scenes)
        {
            Console.WriteLine(this.name + " is currently in the " + scenes[this.currentSceneID].name);
        }
        public void EnterScene(Scene myScene)
        {
            this.currentSceneID = myScene.sceneID;
            myScene.WriteSceneName();
            if (!myScene.hasVisited)
            {
                myScene.hasVisited = true;
                Console.WriteLine(myScene.description);
                Console.Write('\n');
            }
            myScene.WriteSceneExits();
            Console.Write('\n');
        }
        public void SaveFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(Character));
                XML.Serialize(stream, this);
            }
        }

        public Character LoadFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer XML = new XmlSerializer(typeof(Character));
                return (Character)XML.Deserialize(stream);
            }
        }
    }
}
