using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiefenthaler_Zehetner_Webshop
{
    public class Person //Tiefenthaler
    {
        #region private members
        private string _firstname;
        private string _surname;
        private DateTime _dayOfBirth;
        private readonly Address _address;
        private string _eMailadress;
        private string _password;
        private readonly ConsumerBasket _consumerBasket;
        #endregion

        #region public properties
        public string FirstName
        {

            get
            {
                return _firstname;
            }
            private set
            {
                if (CheckStringInput(value))
                {
                    _firstname = value;
                }
                else
                {
                    new Exception("Invalid Input of Name.");
                }
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            private set
            {
                if (CheckStringInput(value))
                {
                    _surname = value;
                }
                else
                {
                    throw new Exception("Invalid Input of Name.");
                }
            }
        }
        public DateTime DayOfBirth
        {
            get
            {
                return _dayOfBirth;
            }
            private set
            {
                if (value <= DateTime.Today && value >= new DateTime(1900, 01, 01))
                {
                    _dayOfBirth = value;
                }
                else
                {
                    throw new Exception("Invalid Input of Day of Birth.");
                }
            }
        }
        public Address Address
        {
            get
            {
                return _address;
            }
        }
        public string EMailAdress
        {
            get
            {
                return _eMailadress;
            }
            private set
            {
                if (CheckEmailadress(value))
                {
                    _eMailadress = value;
                }
                else
                {
                    new Exception("Invalid Input of emailadress.");
                }
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            private set
            {
                if (CheckPassword(value))
                {
                    _password = value;
                }
                else
                {
                    new Exception("Invalid Input of password");
                }
            }
        }
        public ConsumerBasket ConsumerBasket
        {
            get
            {
                return _consumerBasket;
            }
        }
        #endregion

        #region constructor
        public Person() : this("ErrorName", "ErrorName", new DateTime(1900, 01, 01), new Address(), "xxx@xxx.at", "1234", new ConsumerBasket())
        {

        }
        public Person(string firstName, string surName, DateTime dayOfBirth, Address address, string eMailadress, string password, ConsumerBasket consumerbasket)
        {
            FirstName = firstName;
            Surname = surName;
            DayOfBirth = dayOfBirth;
            _address = address;
            EMailAdress = eMailadress;
            Password = password;
            _consumerBasket = consumerbasket;
        }
        #endregion

        #region public methods
        public static bool CheckStringInput(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString) || inputString.Length < 2 || !char.IsUpper(inputString[0])) // Inhalt, Länge und Großbuchstaben prüfen
            {
                return false;
            }
            for (int i = 0; i < inputString.Length; i++)
            {
                if (!(char.IsLetter(inputString[i]) || Equals(inputString[i], '-') || Equals(inputString[i], '.') || char.IsWhiteSpace(inputString[i]))) // beinhalte Zeichen prüfen
                {
                    return false;
                }
                if (Equals(inputString[i], '-') || Equals(inputString[i], '.') || char.IsWhiteSpace(inputString[i])) // keine hintereinanderfolgende Sonderzeichen
                {
                    if (Equals(inputString[i], inputString[i+1]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool CheckEmailadress(string inputEmailadress)
        {
            if (string.IsNullOrWhiteSpace(inputEmailadress) || !inputEmailadress.Contains('@') || !(inputEmailadress.EndsWith(".at") || 
                inputEmailadress.EndsWith(".de") || inputEmailadress.EndsWith(".com"))) // Endungen und Inhalt prüfen
            {
                return false;
            }
            for (int i = 0; i < inputEmailadress.Length; i++)
            {
                if (!(char.IsLetterOrDigit(inputEmailadress[i]) || Equals(inputEmailadress[i], '-') || 
                      Equals(inputEmailadress[i], '.') || Equals(inputEmailadress[i], '@'))) // erlaubte Zeichen prüfen
                {
                    return false;
                }
                int counterdoubleSign = 0;
                if (Equals(inputEmailadress[i], '@')) // nur ein @-Zeichen
                {
                    counterdoubleSign++;
                }
                if (counterdoubleSign>1)
                {
                    return false;
                }
                if (Equals(inputEmailadress[i], '-') || Equals(inputEmailadress[i], '.') || 
                    Equals(inputEmailadress[i], '@') || char.IsWhiteSpace(inputEmailadress[i])) // keine Sonderzeichen können hintereinander folgen
                {
                    if (Equals(inputEmailadress[i], inputEmailadress[i+1]))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
        public static bool CheckPassword(string inputPassword) // 4-stelliger PIN-Code
        {
            if (string.IsNullOrWhiteSpace(inputPassword) || inputPassword.Length != 4)
            {
                return false;
            }
            for (int i = 0; i < inputPassword.Length; i++)
            {
                if(!char.IsDigit(inputPassword[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool GetCheckMethodeForInput(string inputStringformConsole, string variableName)
        {
            switch (variableName)
            {
                case "Vornamen":
                    {
                        return CheckStringInput(inputStringformConsole);
                    }
                case "Nachnamen":
                    {
                        return CheckStringInput(inputStringformConsole);
                    }
                case "Straße":
                    {
                        return Address.CheckStreetorCity(inputStringformConsole);
                    }
                case "Hausnummer":
                    {
                        return Address.CheckNumber(inputStringformConsole);
                    }
                case "Länderkennung":
                    {
                        return Address.CheckCountryCode(inputStringformConsole);
                    }
                case "PLZ":
                    {
                        return Address.CheckZipCode(inputStringformConsole);
                    }
                case "Ort":
                    {
                        return Address.CheckStreetorCity(inputStringformConsole);
                    }
                case "E-Mailadresse":
                    {
                        return CheckEmailadress(inputStringformConsole);
                    }
                case "PIN-Code":
                    {
                        return CheckPassword(inputStringformConsole);
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        public string ToCsvString(char seperator) // ohne Warenkorb wird nicht benötigt -> beim wieder einlesen der Daten muss ein neuer leerer Warenkorb erstellt werden
        {
            return FirstName + seperator + Surname + seperator + DayOfBirth.ToShortDateString() + seperator + Address.ToCsvSring(seperator) + seperator + EMailAdress + seperator + Password;
        }
        public static Person ReadPersonFromcsv(string csvline, char seperator)
        {
            string[] parts = csvline.Split(seperator);
            Person readPerson = new Person(parts[0], parts[1], DateTime.Parse(parts[2]), new Address(parts[3], parts[4], parts[5], parts[6], parts[7]), parts[8], parts[9], new ConsumerBasket());
            return readPerson;
        }
        #endregion

        #region override mehtods
        public override string ToString()
        {
            return "Vorname: " + _firstname + "Familienname: " + _surname + "Geburtsdatum: " + _dayOfBirth + Address.ToString() + "E-Mailadresse: " + _eMailadress + "Passwort: " + _password;
        }
        #endregion
    }
}
