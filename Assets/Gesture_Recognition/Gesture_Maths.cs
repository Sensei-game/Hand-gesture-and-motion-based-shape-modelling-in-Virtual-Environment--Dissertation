using System;

namespace Gesture_Recognition
{

    public class Gesture_Maths
    {
        public Point[] Points = null;            
        public string Name = "";                 
        private const int Sample = 32;
    
        //Constructor
        public Gesture_Maths(Point[] points, string gestureName = "")
        {
            this.Name = gestureName;
            this.Points = Scale(points);
            this.Points = Translate(Points, Centre_Point(Points));
            this.Points = Resize(Points, Sample);
        }

        // Increase the MAX and MIN of a Gesture
        private Point[] Scale(Point[] points)
        {
            float Min_X = float.MaxValue, Min_Y = float.MaxValue, Max_X = float.MinValue, Max_Y = float.MinValue;

            for (int i = 0; i < points.Length; i++)
            {
                if (Min_X > points[i].X)
                { 
                    Min_X = points[i].X;
                }
                if (Min_Y > points[i].Y)
                { 
                    Min_Y = points[i].Y;
                }
                if (Max_X < points[i].X)
                { 
                    Max_X = points[i].X; 
                }
                if (Max_Y < points[i].Y)
                {
                    Max_Y = points[i].Y; 
                }
            }

            Point[] New_points = new Point[points.Length];

            float scale = Math.Max(Max_X - Min_X, Max_Y - Min_Y);

            for (int i = 0; i < points.Length; i++)
            {
                New_points[i] = new Point((points[i].X - Min_X) / scale, (points[i].Y - Min_Y) / scale, points[i].ID);
            }

            return New_points;
        }
        
        // One Point made out of one position modifies all the positions of the whole GEsture
        private Point[] Translate(Point[] points, Point One_Point)
        {
            Point[] newPoints = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                newPoints[i] = new Point(points[i].X - One_Point.X, points[i].Y - One_Point.Y, points[i].ID);
            }
                
            return newPoints;
        }

        //Create a New Point made out of just one Position set
        private Point Centre_Point(Point[] points)
        {
            float cx = 0, cy = 0;

            for (int i = 0; i < points.Length; i++)
            {
                cx += points[i].X;
                cy += points[i].Y;
            }

            return new Point(cx / points.Length, cy / points.Length, 0);
        }

        //Go throught the gestures postions and resize it up to the n position(Imagine a TRIM)
        public Point[] Resize(Point[] points, int n)
        {
            Point[] newPoints = new Point[n];

            newPoints[0] = new Point(points[0].X, points[0].Y, points[0].ID);

            int Points_Number = 1;

            float length = PathLength(points) / (n - 1); 

            float MAX_Distance = 0;

            for (int i = 1; i < points.Length; i++)
            {

                if (points[i].ID == points[i - 1].ID)
                {
                    float distance = DIstance.Distance(points[i - 1], points[i]);


                    if (MAX_Distance + distance >= length)
                    {

                        Point firstPoint = points[i - 1];

                        while (MAX_Distance + distance >= length)
                        {

                            float MIN_Max = Math.Min(Math.Max((length - MAX_Distance) / distance, 0.0f), 1.0f);

                            if (float.IsNaN(MIN_Max))
                            {
                                MIN_Max = 0.5f;

                                newPoints[Points_Number++] = new Point((1.0f - MIN_Max) * firstPoint.X + MIN_Max * points[i].X, (1.0f - MIN_Max) * firstPoint.Y + MIN_Max * points[i].Y, points[i].ID);
                            }


                            distance = MAX_Distance + distance - length;

                            MAX_Distance = 0;

                            firstPoint = newPoints[Points_Number - 1];
                        }

                        MAX_Distance = distance;
                    }
                    else
                    {
                        MAX_Distance += distance;
                    }
                }
            }

            if (Points_Number == n - 1)
            {
                newPoints[Points_Number++] = new Point(points[points.Length - 1].X, points[points.Length - 1].Y, points[points.Length - 1].ID);
            }

            return newPoints;
        }

        //Calculate the length of the gesture
        private float PathLength(Point[] points)
        {
            float length = 0;

            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].ID == points[i - 1].ID)
                {
                    length += DIstance.Distance(points[i - 1], points[i]);
                }
                    
            }
                
            return length;
        }

    }
}