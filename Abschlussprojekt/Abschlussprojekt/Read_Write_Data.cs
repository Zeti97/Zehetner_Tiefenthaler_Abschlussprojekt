using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//erstellt Markus Zehetner
namespace Abschlussprojekt
{
    public class Read_Write_Data
    {
        #region 
        List<Person> personList = new List<Person>();
        #endregion

        #region methods
        public void LoadFile(string dataPath, char seperator, out int error) 
        {
            error = 0;
            int counter = 0;
            int failedEvents = 0;

            try
            {
                using (StreamReader reader = new StreamReader(dataPath, Encoding.GetEncoding("iso-8859-1")))
                {
                    
           
                    while (reader.Peek() != -1)
                    {
                        string line = reader.ReadLine();
                        if (counter > 0)
                        {
                            Person newPersonDataLine = Person.ReadPersonFromcsv(line, seperator);
                            Event newEventDataLine = Event.ReadDataLine(line, seperator);

                            int personNumber = CheckIfPersonExists(newPersonDataLine);
                            int lengthOfList = personList.ToArray().Length;

                            if (newPersonDataLine != null && newEventDataLine != null && personNumber == -1)
                            {
                                personList.Add(newPersonDataLine);
                                personList[lengthOfList].AddEventtoPerson(newEventDataLine,out int error1);
                            }
                            if (newPersonDataLine != null && newEventDataLine != null && personNumber != -1)
                            {
                                personList[personNumber].AddEventtoPerson(newEventDataLine, out int error2);
                            }
                        }
                        counter++;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                error = 1;
            }
            catch (DirectoryNotFoundException)
            {
                error = 3;
            }
            catch (FileNotFoundException)
            {
                error = 8;
            }
            catch (IOException)
            {
                error = 5;
            }
            catch (ArgumentException)
            {
                error = 6;
            }
            catch (Exception)
            {
                error = 99;
            }
        }

        private int CheckIfPersonExists(Person person)
        {
            int existingPerson = -1;
            for (int i = 0; i < personList.ToArray().Length; i++)
            {
                if (personList[i].LjID == person.LjID)
                {
                    existingPerson = i;
                    break;
                }
            }
            return existingPerson;
        }
        #endregion
    }
}