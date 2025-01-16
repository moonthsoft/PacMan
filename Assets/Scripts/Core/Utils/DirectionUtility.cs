namespace Moonthsoft.Core.Utils.Direction
{
    using Moonthsoft.Core.Definitions.Direction;
    using NUnit.Framework;
    using System.Collections.Generic;

    public static class DirectionUtility
    {
        public static Direction ReverseDirection(int index)
        {
            return ReverseDirection((Direction)index);
        }

        public static Direction ReverseDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
            }

            return Direction.Up;
        }

        public static List<Direction> GetNeighborDirections(Direction dir)
        {
            var dirAux = new List<Direction>() { dir };

            if (dir == Direction.Up || dir == Direction.Down)
            {
                dirAux.Add(Direction.Left); 
                dirAux.Add(Direction.Right);
            }
            else
            {
                dirAux.Add(Direction.Up);
                dirAux.Add(Direction.Down);
            }

            return dirAux;
        }
    }
}