using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graphen.ViewModel
{
    public static class Physics // im wondering if this should be in ViewModel or Model? Anyway its movable.
    {
        //for now i do every operation in o(n^2). In the future we will partition graph into sectors so that we can calulate it in o(n).
        private const double repelRange = 60;
        private const double speedOfMoving = 0.1;
        public static Vector CalculateForceVector(Circle c, ICollection<Circle> circles)
        {
            Vector resultantForce = new Vector();
            foreach(Circle i in circles)
            {
                double distance = Circle.CountDistance(i, c);
                if (repelRange >= distance && distance != 0)
                {
                    Vector v = new Vector(c.Position.X - i.Position.X, c.Position.Y - i.Position.Y);
                    resultantForce += (v * 1 / distance); // inverserly to distance.
                }
            }
         //   System.Diagnostics.Debug.WriteLine("accidental strength "+accidentalStrength);
            return resultantForce;
        }

        public static void UpdateForceVector(Circle c, ICollection<Circle> circles)
        {
            c.StrengthVector = CalculateForceVector(c, circles);
        }
        
        public static Point CalculateCurrentCirclePosition(int secondsBefore, int milisecondsBefore, Circle c)
        {
            DateTime now = DateTime.Now;
            double timeElapsed;
            if (now.Second != secondsBefore)
            {
                timeElapsed =  (now.Millisecond + 1000 - milisecondsBefore);
            }
            else
            {
                timeElapsed =  (now.Millisecond - milisecondsBefore);
            }
            return c.Position + c.StrengthVector * timeElapsed * speedOfMoving;
        }

        public static void UpdateCurrentCirclePosition(int secondsBefore, int milisecondsBefore, Circle c)
        {
            c.Position = CalculateCurrentCirclePosition(secondsBefore, milisecondsBefore, c);
        }
    } 
}
