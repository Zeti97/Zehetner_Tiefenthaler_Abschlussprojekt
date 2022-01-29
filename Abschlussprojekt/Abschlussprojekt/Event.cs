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

        #region constructor
        public Event()
        {
            _name = "Default";
            _emphasis = Enums.emphasis.Default;
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
            string rowEmphasis = parts[20].Replace('&', '_').Trim();
            bool conversationOfEmphasisOk = checkIfEmphasisOk(parts[20], out Enums.emphasis emphasis);
            string categorie = parts[21];
            bool conversationOfPointsOk = checkIfCorrectNumber(parts[26], out double points);
            string marker = parts[28];

            readOfDataSuccesfull = conversationOfEmphasisOk && conversationOfPointsOk;

            //create new object of class AppData
            if (readOfDataSuccesfull)
            {
                readData = new Event(name,emphasis,categorie,points,marker);
            }
            return readData;
        }
        static bool checkIfEmphasisOk(string rowEmphasis, out Enums.emphasis emphasis)
        {
            bool correctEmphasisType = false;
            emphasis = Enums.emphasis.Default;

            correctEmphasisType = Enum.TryParse<Enums.emphasis>(rowEmphasis, out emphasis); ;

            return correctEmphasisType;
        }
        static bool checkIfCorrectNumber(string rowNumber, out double number)
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
