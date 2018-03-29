using System;
using System.Collections.Generic;
namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public List<Room> AllRooms { get; set; } = new List<Room>();
        public Player CurrentPlayer { get; set; } = new Player();

        private int _roomCounter { get; set; }

        private bool _playing { get; set; }

        private int _donkeyEnergy { get; set; }

        public void Reset()
        {
            Setup();
        }

        public void Setup()
        {
            Console.Clear();
            CurrentPlayer.getName();
            Console.Clear();
            System.Console.WriteLine($"I am still not happy with you {CurrentPlayer.Name}, but at least I am not the ass here.");
            ShowHelp();
            ShowContinue("Are you ready to start? (Y/N)");
        }

        public void ShowHelp()
        {
            System.Console.WriteLine($@"Here are your controls. I shouldn't be telling you this, but it is required...

    UP                  - moves the player up the mountain
    DOWN                - moves the player down the mountain
    TAKE (item name)    - grabs an item you can use
    USE (item name)     - uses item from inventory
    LOOK                - looks at your surroundings
    INVENTORY           - shows what items you are holding
    HELP                - shows this screen
    QUIT                - quits the game
      ");
        }
        public void ShowContinue(string str)
        {
            System.Console.WriteLine(str);
            var userInput = Console.ReadLine().ToLower();
            switch (userInput)
            {
                case "y":
                    System.Console.WriteLine("DONKEY: FINE. Lets go.");
                    BeginGame();
                    break;
                default:
                    System.Console.WriteLine("DONKEY: GET OFF!");
                    break;
            }
        }
        public void BeginGame()
        {
            _donkeyEnergy = 3;
            _playing = true;
            _roomCounter = 0;
            CurrentRoom = AllRooms[_roomCounter];
            Console.Clear();
            System.Console.WriteLine($@"-------------------
Donkey Energy: {_donkeyEnergy}");
            System.Console.WriteLine($@"--------
{CurrentRoom.Name}
--------
            ");
            System.Console.WriteLine($"{CurrentRoom.Description}");
            askQuestion("DONKEY: What do you want!!?");
            ContinueGame();
        }

        public void askQuestion(string str)
        {
            System.Console.WriteLine($@"
-{str}");
            var userInput = Console.ReadLine().ToLower();
            switch (userInput)
            {
                case "help":
                    ShowHelp();
                    askQuestion(str);
                    break;
                case "quit":
                    _playing = false;
                    break;
                case "up":
                    if (CurrentRoom.Name == "The TOP")
                    {
                        askQuestion(str);
                    }
                    else if (CurrentRoom.Name == "The GAP")
                    {
                        if (CurrentPlayer.Inventory.Count == 0)
                        {
                            System.Console.WriteLine("DONKEY: Really? With nothing in my belly. Ahhhhhh! Splat tatta tat tat...");
                            System.Console.WriteLine("NARRATOR: You lose");
                            _playing = false;

                        }
                        else
                        {
                            MoveRooms("up");
                            _donkeyEnergy--;
                        }
                    }
                    else
                    {
                        MoveRooms("up");
                        _donkeyEnergy--;
                    }
                    break;
                case "down":
                    if (CurrentRoom.Name == "The BASE")
                    {
                        askQuestion(str);
                    }
                    else
                    {
                        MoveRooms("down");
                        _donkeyEnergy--;
                    }
                    break;
                case "look":
                    Console.Clear();
                    System.Console.WriteLine($@"
------------------------------------------------------------
{CurrentRoom.Name}
{CurrentRoom.Description}
------------------------------------------------------------
");
                    break;
                case "take cactus":
                    if (CurrentRoom.Name == "The STROLL" && CurrentRoom.Items.Count > 0)
                    {
                        takeItem();
                    }
                    else
                    {
                        askQuestion(@"
----------------------------------------------------------
DONNKEY: Can't take that. I am a donkey and I know that.
----------------------------------------------------------
");
                        ContinueGame();
                    }
                    break;
                case "use cactus":
                    if (CurrentPlayer.Inventory.Count > 0)
                    {
                        UseItem("cactus");
                    }
                    else
                    {   
                        Console.Clear();
                        System.Console.WriteLine(@"---------------------------------------------
DONNKEY: You cannot use what you dont have...
---------------------------------------------");
                    }
                    break;
                case "inventory":
                    if (CurrentPlayer.Inventory.Count > 0)
                    {
                        System.Console.WriteLine($"{CurrentPlayer.Inventory[0].Name} | {CurrentPlayer.Inventory[0].Description}");
                    }
                    else
                    {
                        askQuestion("Nothing in your Inventory...");
                    }
                    break;
                default:
                    System.Console.WriteLine(@"
------------------------------------------------------------                   
DONNKEY: You are wasting my time. I am getting tired HUMAN!
------------------------------------------------------------
");
                    break;
            }
        }
        public void MoveRooms(string str)
        {

            if (_donkeyEnergy == 0)
            {
                _playing = false;
                System.Console.WriteLine("Donkey got too tired to hold you and bucked you off the cliff.");
            }
            else if (str == "up")
            {
                CurrentRoom = AllRooms[_roomCounter].Directions[str];
                _roomCounter++;
            }
            else if (str == "down")
            {
                CurrentRoom = AllRooms[_roomCounter].Directions[str];
                _roomCounter--;
            }
            else
            {
                askQuestion("DONNKEY: Wasting my friggin time...");
            }
        }

        public void ContinueGame()
        {

            while (_playing)
            {
                if (CurrentRoom.Name == "The TOP")
                {
                    System.Console.WriteLine(@"
**************************************
*                                    *
* DONNKEY: You Made it. Now get off! *
*                                    *
**************************************
                    ");
                    _playing = false;
                }
                else
                {
                    if (_donkeyEnergy == 0)
                    {
                        System.Console.WriteLine($@"----------------------------------------------------------------------------------------------
Donkey Energy: {_donkeyEnergy} - your donkey cannot move. Better find something for him to eat.");
                    }
                    else
                    {
                        System.Console.WriteLine($@"-------------------
Donkey Energy: {_donkeyEnergy}");
                    }
                    System.Console.WriteLine($@"--------
{CurrentRoom.Name}
--------
                    ");
                    System.Console.WriteLine(CurrentRoom.Description);
                    askQuestion("DONNKEY: What next?");
                }

            }
        }

        public void takeItem()
        {
            Console.Clear();
            var cactus = CurrentRoom.Items.Find(i => i.Name == "Cactus");
            if (cactus != null)
            {
                CurrentRoom.Items.Remove(cactus);
                CurrentPlayer.Inventory.Add(cactus);
                System.Console.WriteLine("You took a cactus!");
            }
        }

        public void UseItem(string itemName)
        {
            Console.Clear();
            System.Console.WriteLine($@"
DONKEY: Give me that!
NARRATOR: The donkey turns around and grabs the {itemName} out of your hands. He doesnt mind the spikes. He is that thirsty. You feel his body strengthen.
                ");
            _donkeyEnergy += 2;
        }
    }
}