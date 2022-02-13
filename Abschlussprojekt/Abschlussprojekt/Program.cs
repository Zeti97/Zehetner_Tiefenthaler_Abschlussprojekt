using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Read_Write_Data personList = new Read_Write_Data();
            ReadData(personList, out string dataPath);

            while (true)
            {
                Console.Clear();
                MainMenue(personList, dataPath);
            }
            Console.ReadLine();
        }
        static void ReadData(Read_Write_Data personList, out string dataPath) //Zehetner
        {
            dataPath = ReadPath();
            personList.LoadFile(dataPath, ';', out int error);

            ErrorHandlingStream(error, "Einlesen der Daten");
        }
        static string ReadPath()//Zehetner
        {
            string dataPath = "";

            Console.WriteLine("Bitte geben Sie den Pfad der Datei ein: ");
            dataPath = Console.ReadLine();

            return dataPath;
        }
        static void MainMenue(Read_Write_Data personList, string dataPath)
        {
            WriteActualMenueToConsole(1);

            int menueNumber = AskAndCheckMenueNumber(2);

            switch (menueNumber)
            {
                case 1: //Filter per Points
                    {
                        double limitForFilter = AskForLimit();
                        List<Person> filteredList = Helper.filteredPerPointsToList(personList.PersonList, limitForFilter);
                        Console.WriteLine();
                        for (int i = 0; i < filteredList.ToArray().Length; i++)
                        {
                            Console.WriteLine(Helper.CreateLineForConsolePoints(filteredList[i]) + 
                                              filteredList[i].TotalPoints.ToString("0.0").PadRight(8));
                        }
                        bool decisioneToSaveData = DecisionQuestion("\nWollen Sie Ihre gefilterten Daten in eine Datei schreiben? (J/N)\n");
                        if(decisioneToSaveData)
                        {
                            string dataPathFilterPerPoints = Read_Write_Data.CreateNewDataPath(dataPath) + "Gesamtpunkt_mit_dem_Grenzwert_" + limitForFilter + ".csv";
                            Read_Write_Data.WritePersonTotalPointsToCSV(filteredList, dataPathFilterPerPoints, out int error);
                            ErrorHandlingStream(error, "Schreiben der Daten in Datei");
                        }
                        break;
                    }
                case 2: //Filter per OnTop
                    {
                        double limitForFilter = AskForLimit();
                        string askedMarker = AskForMarker();
                        List<Person> filteredList = Helper.filteredPerOnTopPointsToList(personList.PersonList, limitForFilter, askedMarker);
                        Console.WriteLine();
                        for (int i = 0; i < filteredList.ToArray().Length; i++)
                        {
                            Console.WriteLine(Helper.CreateLineForConsolePoints(filteredList[i]) + 
                                              filteredList[i].OnTopPointsperYear[0].Marker.PadRight(18) + 
                                              filteredList[i].OnTopPointsperYear[0].Points.ToString("0.0").PadRight(8));
                        }
                        bool decisioneToSaveData = DecisionQuestion("\nWollen Sie Ihre gefilterten Daten in eine Datei schreiben? (J/N)\n");
                        if (decisioneToSaveData)
                        {
                            string dataPathFilterOnTopPerPoints = Read_Write_Data.CreateNewDataPath(dataPath) + "OnTopPunkte_mit_dem_Grenzwert_" + limitForFilter + "_Jahr_" + askedMarker.Replace('/','-') + ".csv";
                            Read_Write_Data.WritePersonOnTOPPointsToCSV(filteredList, dataPathFilterOnTopPerPoints, out int error);
                            ErrorHandlingStream(error, "Schreiben der Daten in Datei");
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
        static int AskAndCheckMenueNumber(int highestNumber)//Zehetner
        {
            bool checkOfNumberOk;
            int menueNumber;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Bitte geben Sie die Nummer des Menüpunktes ein.");
                string unconvertedMenueNumber = Console.ReadLine();

                bool conversionOk = int.TryParse(unconvertedMenueNumber, out menueNumber);
                checkOfNumberOk = conversionOk && menueNumber >= 1 && menueNumber <= highestNumber;
                if (!checkOfNumberOk)
                {
                    Console.WriteLine("Die Nummer ist nich im vorgegeben Bereich!");
                    Console.WriteLine("Bitte wählen Sie eine Nummer zwischen 1 und " + highestNumber + ": ");
                }
            }
            while (!checkOfNumberOk);

            return menueNumber;
        }
        static void WriteActualMenueToConsole(int typeOfMenue)//Zehetner
        {
            //1....Filter Auswahl

            Console.WriteLine("Bitte wählen ein Menü:");

            if (typeOfMenue == 1)
            {
                Console.WriteLine("1...Filtere nach Gesamtpunkteanzahl");
                Console.WriteLine("2...Filtere nach OnTOP-Punkten");
            }
        }
        static void ErrorHandlingStream(int error, string nameOfAktion)//Zehetner
        {
            if (error != 0)
            {
                Console.WriteLine("Es ist ein unerwarteter Fehler beim " + nameOfAktion + " aufgetretten,\n" +
                    "Es konnten möglicherweise nicht alle Daten gelesen werden!\n" +
                    "Fehler: ");
                switch (error)
                {
                    case 1:
                        {
                            Console.WriteLine("Path was null (empty)!\n");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Not authorized!\n");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Folder not found!\n");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Path too Long!\n");
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("File interaction error!\n");
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Path invalid!\n");
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Security error!\n");
                            break;
                        }
                    case 8:
                        {
                            Console.WriteLine("File not found!\n");
                            break;
                        }
                    case 9:
                        {
                            Console.WriteLine("The access to the network wasn't available.\n");
                            break;
                        }
                    case 99:
                        {
                            Console.WriteLine("Unknown Error!\n");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Value of Error is invalid!\n");
                            break;
                        }
                }
            }
        }
        static double AskForLimit()
        {
            double limitPoints;
            bool checkOfLimitIsOk;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Bitte geben Sie einen minimalen Gernzwert der Punkte ein.");
                string limitString = Console.ReadLine();
                bool conversionOK = double.TryParse(limitString, out limitPoints);
                checkOfLimitIsOk = conversionOK && limitPoints >= 0;
                if (!checkOfLimitIsOk)
                {
                    Console.WriteLine("Das Limit muss größer 0 sein und darf nur Zahlen enthalten!");
                }
            }
            while (!checkOfLimitIsOk);

            return limitPoints;
        }
        public static bool DecisionQuestion(string askingText)
        {
            Console.Write(askingText);
            string decsionContinue = Console.ReadLine();
            bool decision = decsionContinue.ToLower().Equals("j");
            return decision;
        }
        public static string AskForMarker()
        {
            string askedMarker;
            bool checkedMarker;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Bitte geben Sie das Jahr ein, für welches Sie die OnTop-Punkte suchen wollen?\n(Format: JJJJ/JJ)\n");
                askedMarker = Console.ReadLine();
                checkedMarker = CheckMarker(askedMarker);

                if (!checkedMarker)
                {
                    Console.WriteLine("Ihre Eingabe war nicht korrekt. Bitte beachte das vorgegebene Format.");
                }
            }
            while (!checkedMarker);
            return (askedMarker);
        }
        public static bool CheckMarker(string inputMarker)
        {
            string removeSlashString = inputMarker.Remove(4, 1);
            if(!Equals(inputMarker[4], '/'))
            {
                return false;
            }
            for (int i = 0; i < removeSlashString.Length; i++)
            {
                if(!char.IsDigit(removeSlashString[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
