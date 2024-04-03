using System.Windows.Forms;


namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";
        public int GetDrawingPriority() => 0;
        public bool DeadInConflict(ICreature conflictedObject) => true;
        public CreatureCommand Act(int x, int y) => new CreatureCommand { };
    }

    public class Player : ICreature
    {
        public bool IsSack(int x, int y) => Game.Map[x, y] is Sack;
        public string GetImageFileName() => "Digger.png";
        public int GetDrawingPriority() => 1;
        public bool DeadInConflict(ICreature opponentObject)
        {
            if (opponentObject.GetImageFileName() == "Gold.png") Game.Scores += 10;
            return opponentObject.GetImageFileName() == "Sack.png";
        }

        public CreatureCommand Act(int x, int y)
        {
            var key = Game.KeyPressed;
            var step = 1;

            if ((key == Keys.Down) && y < Game.MapHeight - step && !IsSack(x, y + step))
                return new CreatureCommand { DeltaX = 0, DeltaY = step };
            if ((key == Keys.Up) && y >= step && !IsSack(x, y - step))
                return new CreatureCommand { DeltaX = 0, DeltaY = -step };
            if ((key == Keys.Right) && x < Game.MapWidth - step && !IsSack(x + step, y))
                return new CreatureCommand { DeltaX = step, DeltaY = 0 };
            if ((key == Keys.Left) && x >= step && !IsSack(x - step, y))
                return new CreatureCommand { DeltaX = -step, DeltaY = 0 };
            else
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }
    }

    public class Sack : ICreature
    {
        public string GetImageFileName() => "Sack.png";
        public int GetDrawingPriority() => 2;
        public bool DeadInConflict(ICreature conflictedObject) => false;

        public int Counter = 0;

        public CreatureCommand Command(string cmd)
        {
            switch (cmd)
            {
                case "toGold":
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
                case "stay":
                    Counter = 0;
                    return new CreatureCommand { };
                case "fall":
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                default:
                    return new CreatureCommand { };
            }
        }

        public CreatureCommand Act(int x, int y)
        {
            while (y != Game.MapHeight - 1)
            {
                bool isNull = Game.Map[x, y + 1] is null;
                bool isPlayer = Game.Map[x, y + 1] is Player;

                if (isNull || (isPlayer && Counter > 0))
                {
                    Counter++;
                    return Command("fall");
                }
                else if (Counter > 1)
                    return Command("toGold");
                else
                    return Command("stay");
            }
            return Counter > 1 ? Command("toGold") : Command("stay"); ;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";
        public int GetDrawingPriority() => 3;
        public bool DeadInConflict(ICreature conflictedObject) => true;
        public CreatureCommand Act(int x, int y) => new CreatureCommand { };
    }
}