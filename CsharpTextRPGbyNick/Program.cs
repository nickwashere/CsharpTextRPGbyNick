using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace CsharpTextRPGbyNick
{
    enum Door : uint {arch=0, unlocked=1, locked=2}
    enum Direction : uint {north=0, northeast=1, east=2, southeast=3, south=4, southwest=5, west=6, northwest=7, up=8, down=9}
    class Program
    {
        static void Main(string[] args)
        {
            WriteTitle("SAMPLE TITLE");
            
            //define scenes within list of scenes
            List<Scene> gameScenes = new List<Scene>();

            gameScenes.Add(new Scene() {name="Laboratory", description = "There's a lot of stuff here."});
            gameScenes.Add(new Scene() {name="Bedroom", description = "Hello World"});

            //Assign unique sceneID to each scene
            for(int i=0; i<gameScenes.Count; i++) { gameScenes[i].sceneID = i; }

            //add exits to scenes, including itself
            foreach(Scene s in gameScenes) { s.exits.Add(new Connection() { nextScene = s }); }
            gameScenes[0].exits.Add(new Connection() {nextScene = gameScenes[1]});
            gameScenes[0].exits.Add(new Connection() { nextScene = gameScenes[1] });
            gameScenes[0].exits.Add(new Connection() { nextScene = gameScenes[1] });
            gameScenes[0].exits.Add(new Connection() { nextScene = gameScenes[1] });
            gameScenes[1].exits.Add(new Connection() {nextScene = gameScenes[0]});

            //define player character
            Character player = new Character();
            player.name = "Bob";
            player.EnterScene(gameScenes[0]);

            //Prevent console from closing when program finishes running
            bool gameRunning = true;
            while(gameRunning)
            {
                Console.Write(">>> ");
                string input = Console.ReadLine();
                if (input == "QUIT") gameRunning = false;
                else if (input == "SAVE") player.SaveFile("Game Profile.xml");
                else if (input == "LOAD") player = player.LoadFile("Game Profile.xml");
                else ParsePlayerInput(input, player, gameScenes);
            }
        }
        static void ParsePlayerInput(string input, Character player, List<Scene> scenes)
        {
            int room = ExitRoom(input);
            //Count returns as highest index + 1, so sub 1
            if (room >= 0 && room <= scenes[player.currentSceneID].exits.Count-1) player.EnterScene(scenes[player.currentSceneID].exits[room].nextScene);
            player.DisplayPartyStatus(scenes);
        }
        static int ExitRoom(string input)
        {
            int i;
            if (int.TryParse(input, out i)) return i;
            else return -1;
        }
        static void WriteTitle(string title)
        {
            Console.WriteLine(title);
            foreach(char c in title) { Console.Write('-'); }
            Console.Write('\n');
            Console.Write('\n'); //put here twice for blank line between title underline and name of first scene
        }
    }
}
