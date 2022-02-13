﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

//erstellt Markus Zehetner
namespace Abschlussprojekt
{
    public class Read_Write_Data
    {
        #region methods
        public static List<Person> LoadFile(string dataPath, char seperator, out int error) 
        {
            error = 0;
            int counter = 0;
            int failedEvents = 0;
            List<Person> personList = new List<Person>();

            try
            {
                using (StreamReader reader = new StreamReader(dataPath, Encoding.GetEncoding("iso-8859-1")))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = reader.ReadLine();
                        if (counter > 0)
                        {
                            line = CheckAndChangeLine(line);

                            Person newPersonDataLine = Person.ReadPersonFromcsv(line, seperator);
                            Event newEventDataLine = Event.ReadDataLine(line, seperator);

                            int personNumber = CheckIfPersonExists(personList,newPersonDataLine);
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
            return personList;
        }
        private static int CheckIfPersonExists(List<Person> personList,Person person)
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
        public static void WritePersonTotalPointsToCSV(List<Person> personList, string dataPath, out int error)
        {
            error = 0;
            try
            {
                using (StreamWriter writer = new StreamWriter(dataPath, false, Encoding.GetEncoding("iso-8859-1")))
                {
                    writer.WriteLine("LJ-ID;Anrede;Vorname;Nachname;Punkte");
                    for (int i = 0; i < personList.ToArray().Length; i++)
                    {              
                            writer.WriteLine(personList[i].ToCsvStringTotalPoints(';'));
                    }
                }
            }
            catch (ArgumentNullException)
            {
                error = 1;
            }
            catch (UnauthorizedAccessException)
            {
                error = 2;
            }
            catch (DirectoryNotFoundException)
            {
                error = 3;
            }
            catch (PathTooLongException)
            {
                error = 4;
            }
            catch (IOException)
            {
                error = 5;
            }
            catch (ArgumentException)
            {
                error = 6;
            }
            catch (SecurityException)
            {
                error = 7;
            }
            catch (Exception)
            {
                error = 99;
            }
        }
        public static void WritePersonOnTOPPointsToCSV(List<Person> personList, string dataPath, out int error)
        {
            error = 0;
            try
            {
                using (StreamWriter writer = new StreamWriter(dataPath, false, Encoding.GetEncoding("iso-8859-1")))
                {
                    writer.WriteLine("LJ-ID;Anrede;Vorname;Nachname;Punkte;Marker;OnTop-Punkte");
                    for (int i = 0; i < personList.ToArray().Length; i++)
                    {
                        writer.WriteLine(personList[i].ToCsvStringOnTop(';'));
                    }
                }
            }
            catch (ArgumentNullException)
            {
                error = 1;
            }
            catch (UnauthorizedAccessException)
            {
                error = 2;
            }
            catch (DirectoryNotFoundException)
            {
                error = 3;
            }
            catch (PathTooLongException)
            {
                error = 4;
            }
            catch (IOException)
            {
                error = 5;
            }
            catch (ArgumentException)
            {
                error = 6;
            }
            catch (SecurityException)
            {
                error = 7;
            }
            catch (Exception)
            {
                error = 99;
            }
        }
        public static string CreateNewDataPath(string dataPath)
        {
            string dataPathWithoutName = null;
            string[] partsOfPath = dataPath.Split('\\');
            partsOfPath[partsOfPath.Length - 1] = "";
            dataPathWithoutName = string.Join("\\", partsOfPath);
            return dataPathWithoutName;
        }

        public static string CheckAndChangeLine(string line)
        {
            int index1 = 0;
            int startindex = 0;
            do
            {
                index1 = line.Substring(startindex, (line.Length - startindex)).IndexOf(";\"");
                int index2 = line.Substring(startindex, (line.Length - startindex)).IndexOf("\";");
                int length = index2 - index1;

                startindex += index1;

                if (index1 >= 0 && index2 >= 0)
                {
                    string toExchange = line.Substring(startindex + 1, length);
                    toExchange = toExchange.Replace(';', ',');
                    toExchange = toExchange.Replace('\"', ' ');
                    line = line.Remove(startindex + 1, length);
                    line = line.Insert(startindex + 1, toExchange);
                }
            }
            while (index1 > 0);

            return line;
        }
        #endregion
    }
}