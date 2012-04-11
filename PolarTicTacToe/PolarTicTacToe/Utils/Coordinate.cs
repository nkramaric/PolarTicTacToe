using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Utils
{
    public class Coordinate
    {

        public int X;
        public int Y;

        public Coordinate()
        {
        }

        public Coordinate(int x, int y)
        {
            X = Rel(x);
            Y = y;
        }

        public static int Rel(int number)
        {
            return (8 + number) % 8;
        }


        public static Coordinate operator +(Coordinate one, Coordinate two)
        {
            Coordinate newCoordinate = new Coordinate();
            newCoordinate.X = Rel(one.X + two.X);
            newCoordinate.Y = one.Y + two.Y;

            return newCoordinate;
        }

        public static Coordinate operator -(Coordinate one, Coordinate two)
        {
            Coordinate newCoordinate = new Coordinate();
            newCoordinate.X = Rel(one.X - two.X);
            newCoordinate.Y = one.Y - two.Y;

            return newCoordinate;
        }
    }
}