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
        public List<Person> filteredPerPointsToList(List<Person> toFilterPersonList, double pointLimit)
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
        public List<Person> filteredPerOnTopPointsToList(List<Person> toFilterPersonList, double pointLimit, string markerYear)
        {
            List<Person> filteredPerson = new List<Person>();
            for (int i = 0; i < toFilterPersonList.ToArray().Length; i++)
            {
                for (int j = 0; j < toFilterPersonList[i].OnTopPointsperYear.ToArray().Length; j++)
                {
                    if(toFilterPersonList[i].OnTopPointsperYear[j].Marker == markerYear)
                    {
                        if(toFilterPersonList[i].OnTopPointsperYear[j].Points >= pointLimit)
                        {
                            filteredPerson.Add(toFilterPersonList[i]);
                        }
                    }
                }
            }
            return filteredPerson;
        }
        #endregion
    }
}
