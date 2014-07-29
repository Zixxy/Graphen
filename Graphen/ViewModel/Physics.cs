using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graphen.ViewModel
{
    public class Physics // im wondering if this should be in ViewModel or Model? Anyway its movable.
    {
        //for now i do every operation in o(n^2). In the future we will partition graph into sectors so that we can calulate it in o(n).
        private static const double repelRange = 40; 
        public static Vector CalculateStrengthVector(Circle c, ICollection<Circle> circles)
        {
            Vector accidentalStrength = new Vector();
            foreach(Circle i in circles){
                double distance = Circle.CountDistance(i, c);
                if (repelRange >= distance)
                {
                    accidentalStrength += new Vector(c.Position.X- i.Position.X, c.Position.Y - i.Position.Y);
                }
            }
            return accidentalStrength;
        }
    }
}
