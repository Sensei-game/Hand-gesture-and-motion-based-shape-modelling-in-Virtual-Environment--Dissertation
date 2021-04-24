using System;

namespace Gesture_Recognition
{
    public class Point
    {
        public float X, Y;
        public int ID;      

        public Point(float x, float y, int id)
        {
            this.X = x;
            this.Y = y;
            this.ID = id;
        }
    }
}
