using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//erstellt von Zehetner
namespace Abschlussprojekt
{
   public class Event
    {
        #region members
        string _name;
        Enums.emphasis _emphasis;
        string _category;
        double _points;
        string _marker;
        #endregion

        #region properties
        public string Marker
        {
            get
            {
                return _marker;
            }
           private set
            {

            }
        }
        public double Points
        {
            get
            {
                return _points;
            }
            private set
            {

            }
        }
        public Enums.emphasis Emphasis
        {
            get
            {
                return _emphasis;
            }
            private set
            {

            }
        }
        #endregion

        #region constructor
        public Event()
        {
            _name = "Default";
            _emphasis = Enums.emphasis.Unknown;
            _category = "Default";
            _points = 0;
            _marker = "";
        }
        private Event(string name, Enums.emphasis emphasis, string category, double point, string marker)
        {
            _name = name;
            _emphasis = emphasis;
            _category = category;
            _points = point;
            _marker = marker;
        }
        #endregion

        #region methoden
        public static Event ReadDataLine(string dataLine, char seperator)
        {
            bool readOfDataSuccesfull = false;
            Event readData = null;
            string[] parts = dataLine.Split(seperator);

            //convert stringdata to correct Datatype
            string name = parts[19];
            string rowEmphasis = parts[20].Replace(" ",String.Empty).Replace('&', '_');
            Enums.emphasis emphasis= ConvertEmphasis(rowEmphasis);
            string categorie = parts[21];
            bool conversationOfPointsOk = CheckIfCorrectNumber(parts[26], out double points);
            string marker = parts[28];

            readOfDataSuccesfull = conversationOfPointsOk;

            //create new object of class AppData
            if (readOfDataSuccesfull)
            {
                readData = new Event(name,emphasis,categorie,points,marker);
            }
            return readData;
        }
        static Enums.emphasis ConvertEmphasis(string rowEmphasis)
        {
            Enums.emphasis emphasis = Enums.emphasis.Unknown;

            bool correctEmphasisType = Enum.TryParse<Enums.emphasis>(rowEmphasis, out emphasis);

            return emphasis;
        }
        static bool CheckIfCorrectNumber(string rowNumber, out double number)
        {
            bool correctNumerType = false;
            number = 0.0;

            bool conversationOk = double.TryParse(rowNumber, out number);

            if (conversationOk && number >= 0.0) correctNumerType = true;

            return correctNumerType;
        }
        #endregion

    }
}
