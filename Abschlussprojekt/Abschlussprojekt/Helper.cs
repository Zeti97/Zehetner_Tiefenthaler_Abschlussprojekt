using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt
{
    public class Helper //Tiefenthaler
    {
        #region methods
        public static List<Person> filteredPerPointsToList(List<Person> toFilterPersonList, double pointLimit)
        {
            List<Person> filteredPerson = new List<Person>();
            for (int i = 0; i < toFilterPersonList.ToArray().Length; i++)
            {
                if (toFilterPersonList[i].TotalPoints >= pointLimit)
                {
                    filteredPerson.Add(toFilterPersonList[i]);
                }
            }
            return filteredPerson;
        }
        public static List<Person> filteredPerOnTopPointsToList(List<Person> toFilterPersonList, double pointLimit, string markerYear)
        {
            List<Person> filteredPerson = new List<Person>();
            for (int i = 0; i < toFilterPersonList.ToArray().Length; i++)
            {
                for (int j = 0; j < toFilterPersonList[i].OnTopPointsperYear.ToArray().Length; j++)
                {
                    if(toFilterPersonList[i].OnTopPointsperYear[j].Marker.Remove(0,6).Trim() == markerYear)
                    {
                        if(toFilterPersonList[i].OnTopPointsperYear[j].Points >= pointLimit)
                        {
                            OnTop ontop = toFilterPersonList[i].OnTopPointsperYear[j];
                            toFilterPersonList[i].OnTopPointsperYear[j] = toFilterPersonList[i].OnTopPointsperYear[0];
                            toFilterPersonList[i].OnTopPointsperYear[0] = ontop;
                            filteredPerson.Add(toFilterPersonList[i]);
                        }
                    }
                }
            }
            return filteredPerson;
        }
        public static string CreateLineForConsolePoints(Person personForConsole)
        {
            string lineForconsole = personForConsole.LjID.PadRight(10) + personForConsole.Salutation.PadRight(8) +
                                    personForConsole.FirstName.PadRight(15) + personForConsole.Surname.PadRight(25);
            return lineForconsole;
        }
        #endregion
    }
}
