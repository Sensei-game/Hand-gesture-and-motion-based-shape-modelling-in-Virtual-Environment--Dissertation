using System;

 namespace Recognizer
{
    public class DIstance
    {
        //Get the X and Y and calculate the distance of the Points
        public static float Square_Distance(Point a, Point b)
        {
            return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
        }
        //Calculate the square root of the Distances
        public static float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt(Square_Distance(a, b));
        }
    }
}

