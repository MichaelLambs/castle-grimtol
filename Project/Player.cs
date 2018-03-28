using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public string Name {get; set;}
        public int Score { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();
        public void getName()
        {
            System.Console.WriteLine("You better give me your name before I kick you off.");
            Name = Console.ReadLine().ToUpper();
            Score = 0;
        }

        public void ShowInventory()
        {
            System.Console.WriteLine($@"
You have a {Inventory[0].Name} | {Inventory[0].Description}
           ");
        }

    }
}