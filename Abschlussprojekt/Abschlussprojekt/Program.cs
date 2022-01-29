﻿using System;
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
            ReadData(personList);

            Console.ReadLine();
            
        }

        static void ReadData(Read_Write_Data personList)
        {
            string dataPath = ReadPath();
            personList.LoadFile(dataPath, ';', out int error);

            ErrorHandlingStream(error, "Einlesen der Daten");
        }
        static string ReadPath()
        {
            string dataPath = "";

            Console.WriteLine("Bitte geben Sie den Pfad der Datei ein: ");
            dataPath = Console.ReadLine();

            return dataPath;
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
    }
}
