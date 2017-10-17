//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoConverter.ViewModel
//{
//    class TempRandom
//    {

//        static void Main(string[] args)
//        {
//            //string[] inputs = Console.ReadLine().Split(' ');
//            //int lightX = int.Parse(inputs[0]); // the X position of the light of power
//            //int lightY = int.Parse(inputs[1]); // the Y position of the light of power
//            //int initialTX = int.Parse(inputs[2]); // Thor's starting X position
//            //int initialTY = int.Parse(inputs[3]); // Thor's starting Y position

//            int lightX = 35; // the X position of the light of power
//            int lightY = 15; // the Y position of the light of power
//            int initialTX = 5; // Thor's starting X position
//            int initialTY = 5; // Thor's starting Y position

//            // game loop
//            while (true)
//            {
//                int remainingTurns = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.

//                // Write an action using Console.WriteLine()
//                // To debug: Console.Error.WriteLine("Debug messages...");

//                string direct = "";

//                int deltaY = lightY - initialTY;
//                int deltaX = initialTX - lightX;

//                double angle = (Math.Atan2(deltaY, deltaX)) * (180.0 / Math.PI);

//                if (angle < 0)
//                {
//                    angle += 360.0;
//                }

//                if (angle > 22.5 && angle <= 67.5)
//                {
//                    direct = "NE";
//                }
//                else if (angle > 67.5 && angle <= 112.5)
//                {
//                    direct = "N";
//                }
//                else if (angle > 112.5 && angle <= 157.5)
//                {
//                    direct = "NV";
//                }
//                else if (angle > 157.5 && angle <= 202.5)
//                {
//                    direct = "V";
//                }
//                else if (angle > 202.5 && angle <= 247.5)
//                {
//                    direct = "SV";
//                }
//                else if (angle > 247.5 && angle <= 292.5)
//                {
//                    direct = "S";
//                }
//                else if (angle > 292.5 && angle <= 337.5)
//                {
//                    direct = "SE";
//                }
//                else if (angle > 337.5 || angle <= 22.5)
//                {
//                    direct = "E";
//                }

//                // A single line providing the move to be made: N NE E SE S SW W or NW
//                Console.WriteLine(direct);
//            }
//        }

//    }
//}
