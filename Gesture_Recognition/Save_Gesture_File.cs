using System.IO;
using System.Collections.Generic;
using System.Xml;

using UnityEngine;

namespace Recognizer
{

    public class Save_Gesture_File
    {

        public static Gesture_Maths From_xml(string xml)
        {

            XmlTextReader re_XML = null;
            Gesture_Maths gesture = null;

            try
            {

                re_XML = new XmlTextReader(new StringReader(xml));
                gesture = Load_Gesture(re_XML);

            }
            finally
            {

                if (re_XML != null)
                {
                    re_XML.Close();
                }

            }

            return gesture;
        }

        public static Gesture_Maths From_file(string fileName)
        {

            XmlTextReader re_XML = null;
            Gesture_Maths gesture = null;

            try
            {

                re_XML = new XmlTextReader(File.OpenText(fileName));
                gesture = Load_Gesture(re_XML);

            }
            finally
            {

                if (re_XML != null)
                {
                    re_XML.Close();
                }

            }

            return gesture;
        }

        private static Gesture_Maths Load_Gesture(XmlTextReader re_XML)
        {
            List<Point> points = new List<Point>();
            int gesture_Number = -1;

            string gestureName = "";

            try
            {
                while (re_XML.Read())
                {
                    if (re_XML.NodeType != XmlNodeType.Element)
                        continue;

                    switch (re_XML.Name)
                    {
                        case "Gesture":

                            gestureName = re_XML["Name"];
                            //XML files have this simbol somtimes
                            if (gestureName.Contains("~"))
                            {
                                gestureName = gestureName.Substring(0, gestureName.LastIndexOf('~'));
                            }
                            //XML files have this simbol somtimes
                            if (gestureName.Contains("_"))
                            {
                                gestureName = gestureName.Replace('_', ' ');
                            }

                            break;

                        case "ID":
                            gesture_Number++;
                            break;

                        case "Point":
                            points.Add(new Point(float.Parse(re_XML["X"]), float.Parse(re_XML["Y"]), gesture_Number));
                            break;
                    }
                }
            }
            finally
            {
                if (re_XML != null)
                {
                    re_XML.Close();
                }

            }
            return new Gesture_Maths(points.ToArray(), gestureName);
        }

        public static void Save_Gesture(Point[] points, string gestureName, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                sw.WriteLine("<Gesture Name = \"{0}\">", gestureName);

                int currentID = -1;

                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].ID != currentID)
                    {
                        if (i > 0)
                        {
                            sw.WriteLine("\t</ID>");
                        }

                        sw.WriteLine("\t<ID>");
                        currentID = points[i].ID;
                    }

                    sw.WriteLine("\t\t<Point X = \"{0}\" Y = \"{1}\" T = \"0\" Pressure = \"0\" />", points[i].X, points[i].Y);
                }
                sw.WriteLine("\t</ID>");
                sw.WriteLine("</Gesture>");
            }
        }
    }
}
