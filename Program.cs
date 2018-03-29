using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game app = new Game();
            Room baseStart = new Room("The BASE",
            @"It scortching out. You been traveling for 6 months.
And on the last leg of your journey you need to climb Sharper Ridge.
On foot will bring you death by buzzards.
Get you and your donkey across.
And remember to keep your donkey happy.");
            Room stroll = new Room("The STROLL",
            @"You start the climb.
Beautiful silver catuses along the road.
You have a small sweat glisining on your face.
The donkey's breaths are even.");
            Room gap = new Room("The GAP",
            @"The trail begins to steepen. 
You are along the edge.
The sure footedness of the donkey allows your mind to wander.
As you continue on you notice a section of trail has given away.
This will require the donkey to jump.
You alone cannot jump the gap.");
            Room finish = new Room("The TOP", "Winner");

            Item cactus = new Item("Cactus", "A prickly son of a bitch that holds very minimal amounts of minerals.");
            
            stroll.Items.Add(cactus);

            baseStart.Directions.Add("up", stroll);
            stroll.Directions.Add("down", baseStart);
            stroll.Directions.Add("up", gap);
            gap.Directions.Add("down", stroll);
            gap.Directions.Add("up", finish);
            finish.Directions.Add("down", gap);

            app.AllRooms.Add(baseStart);
            app.AllRooms.Add(stroll);
            app.AllRooms.Add(gap);
            app.AllRooms.Add(finish);
            
            // baseStart.Directions.Add("up", stroll);

            // stroll.Directions.Add("down", baseStart);
            // stroll.Directions.Add("up", gap);

            // gap.Directions.Add("down", stroll);
            // gap.Directions.Add("up", finish);

            // finish.Directions.Add("down", gap);

            
            app.Setup();

        }
    }
}
