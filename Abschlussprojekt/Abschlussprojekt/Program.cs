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
            string dataPath = ReadPath();
            List<Person> personList = ReadData(dataPath);
            
            while (true)
            {
                Console.Clear();
                MainMenue(personList, dataPath);
            }
            Console.ReadLine();
        }
        static List<Person> ReadData(string dataPath) //Zehetner
        {  
            List<Person> personList = Read_Write_Data.LoadFile(dataPath, ';', out int error);

            ErrorHandlingStream(error, "Einlesen der Daten");

            return personList;
        }
        static string ReadPath()//Zehetner
        {
            string dataPath = "";

            Console.WriteLine("Bitte geben Sie den Pfad der Datei ein: ");
            dataPath = Console.ReadLine();

            return dataPath;
        }
        static void MainMenue(List<Person> personList, string dataPath)
        {
            WriteActualMenueToConsole(1);

            int menueNumber = AskAndCheckMenueNumber(2);

            switch (menueNumber)
            {
                case 1: //Filter per Points
                    {
                        FilterDataPerPointsOrOnTop(personList, dataPath, true);
                        break;
                    }
                case 2: //Filter per OnTop
                    {
                        FilterDataPerPointsOrOnTop(personList, dataPath, false);
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
        static double AskForLimit() //Tiefenthaler
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
        public static bool DecisionQuestion(string askingText) //Tiefenthaler
        {
            bool decision = false;
            string decsionContinue;
            do
            {
                Console.Write(askingText);
                decsionContinue = Console.ReadLine();
                if(decsionContinue.ToLower().Equals("j") || decsionContinue.ToLower().Equals("n"))
                {
                    decision = decsionContinue.ToLower().Equals("j");
                }
                else
                {
                    Console.WriteLine("Ihre Eingabe war nicht korrekt. Bitte geben Sie nur ein \"J\" oder ein \"N\" ein.");
                }
            } while (!(decsionContinue.ToLower().Equals("j")|| decsionContinue.ToLower().Equals("n")));
            return decision;
        }
        public static string AskForMarker() //Tiefenthaler
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
        public static bool CheckMarker(string inputMarker) //Tiefenthaler
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
        public static void FilterDataPerPointsOrOnTop(List<Person> toFilterPersonList, string dataPath, bool totalPointsOrOnTop) //Tiefenthaler
        {
            double limitForFilter = AskForLimit();
            string askedMarker = "";
            List<Person> filteredList = new List<Person> ();
            if (totalPointsOrOnTop)
            {
                filteredList = Helper.filteredPerPointsToList(toFilterPersonList, limitForFilter);
            }
            else
            {
                askedMarker = AskForMarker();
                filteredList = Helper.filteredPerOnTopPointsToList(toFilterPersonList, limitForFilter, askedMarker);
            }
            Console.WriteLine();
            WritePersonToConsole(filteredList, totalPointsOrOnTop);
            AskForDataSaving(filteredList, dataPath, limitForFilter, askedMarker, totalPointsOrOnTop);
            Console.ReadLine();
        }
        public static void WritePersonToConsole(List<Person> toWritePersons, bool totalPointsOrOnTop) //Tiefenthaler
        {
            for (int i = 0; i < toWritePersons.ToArray().Length; i++)
            {
                string textForConsole = Helper.CreateLineForConsolePoints(toWritePersons[i]);
                if(totalPointsOrOnTop)
                {
                    textForConsole += toWritePersons[i].TotalPoints.ToString("0.0").PadRight(8);
                }
                else
                {
                    textForConsole += toWritePersons[i].OnTopPointsperYear[0].Marker.PadRight(18) +
                                              toWritePersons[i].OnTopPointsperYear[0].Points.ToString("0.0").PadRight(8);
                }
                Console.WriteLine(textForConsole);
            }
        }
        public static void AskForDataSaving(List<Person> toSavePerons, string dataPath, double limitForFilter, string askedMarker, bool totalPointsOrOnTop) //Tiefenthaler
        {
            bool decisioneToSaveData = DecisionQuestion("\nWollen Sie Ihre gefilterten Daten in eine Datei schreiben? (J/N)\n");
            if (decisioneToSaveData)
            {
                int error = 0;
                if(totalPointsOrOnTop)
                {
                    string dataPathFilterPerPoints = Read_Write_Data.CreateNewDataPath(dataPath) + "Gesamtpunkt_mit_dem_Grenzwert_" + limitForFilter + ".csv";
                    Read_Write_Data.WritePersonTotalPointsToCSV(toSavePerons, dataPathFilterPerPoints, out error);
                }
                else
                {
                    string dataPathFilterOnTopPerPoints = Read_Write_Data.CreateNewDataPath(dataPath) + "OnTopPunkte_mit_dem_Grenzwert_" + limitForFilter + "_Jahr_" + askedMarker.Replace('/', '-') + ".csv";
                    Read_Write_Data.WritePersonOnTOPPointsToCSV(toSavePerons, dataPathFilterOnTopPerPoints, out error);
                }
                ErrorHandlingStream(error, "Schreiben der Daten in Datei");
                if(error == 0)
                {
                    Console.WriteLine("\nDas Speichern der Daten in eine Datei war erfolgreich.");
                }
            }
            else
            {
                Console.WriteLine("\nMit dem Drücken der Taste \"Enter\" kommen Sie ins Hauptmenü zurück.");
            }
        }
    }
}
