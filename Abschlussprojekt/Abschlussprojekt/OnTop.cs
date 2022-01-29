using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//erstellt von Zehetner
namespace Abschlussprojekt
{
    public class OnTop
    {
        #region members
        string _marker;
        double _points;
        #endregion

        #region properties
        public string Marker
        {
            get
            {
                return _marker;
            }
            set
            {
                if (checkIfOnTopMarker(value) == true)
                {
                    _marker = Marker;
                }
                else
                {
                    throw new Exception("Falscher Marker Typ");
                }
            }
        }
        public double Points
        {
            get
            {
                return _points;
            }
            set
            {
                if(checkIfCorrectNumber(value) == true)
                {
                    _points = Points;
                }
                   else
                {
                    throw new Exception("Falscher Punkte Typ");
                }

            }
        }
        #endregion

        #region constructor
        public OnTop()
        {
            _marker = "onTop Default";
            _points = 0;
        }
        public OnTop(string marker, double points)
        {
            Marker = marker;
            Points = points;
        }
        #endregion

        #region methods
         static bool checkIfOnTopMarker (string marker)
        {
            bool isOnTopMarker = false;
            if (marker.Length > 5)
            {
                if (marker.Substring(0, 5) == "onTop") isOnTopMarker = true;
            }
            return isOnTopMarker;
        }
        static bool checkIfCorrectNumber (double number)
        {
            bool correctNumerType = false;

            if (number >= 0.0) correctNumerType = true;

            return correctNumerType;
        }
        #endregion
    }
}
