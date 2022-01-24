using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt
{
    public class Person //Tiefenthaler
    {
        #region private members
        private string _ljID;
        private string _salutation;
        private string _firstname;
        private string _surname;
        private readonly Address _address;
        private string _locationGroup;
        private string _districtOfVillage;
        private List<Event> _attendedEvents;
        private int _totalpoints;
        private List<OnTop> _onTopPointsperYear;
        private int _totalpointsGeneralEducation;
        private int _totalpointsAgricultureAndEnvironment;
        private int _totalpointsSportAndSociety;
        private int _totalpointsCultureAndTradition;
        private int _totalpointsSeviceAndOrganisation;
        private int _totalpointsYouthAndInternationality;
        #endregion

        #region public properties
        public string LjID
        {
            get
            {
                return _ljID;
            }
            private set
            {
                if (CheckLjID(value))
                {
                    _ljID = value;
                }
                else
                {
                    new Exception("Invalid Input of LjID.");
                }
            }
        }
        public string Salutation
        {
            get
            {
                return _salutation;
            }
            private set
            {
                if (CheckStringInput(value))
                {
                    _salutation = value;
                }
                else
                {
                    new Exception("Invalid Input of Salutation.");
                }
            }
        }
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
        public Address Address
        {
            get
            {
                return _address;
            }
        }
        public string LocationGroup
        {
            get
            {
                return _locationGroup;
            }
            private set //Überprüfungen ergänzen
            {
                _locationGroup = value;
            }
        }
        public string DistrictOfVillage
        {
            get
            {
                return _districtOfVillage;
            }
            private set //Überprüfungen ergänzen
            {
                _districtOfVillage = value;
            }
        }
        public List<Event> AttendedEvents
        {
            get
            {
                return _attendedEvents;
            }
        }
        public int TotalPoints
        {
            get
            {
                return _totalpoints;
            }
            private set
            {
                _totalpoints = value;
            }
        }
        public List<OnTop> OnTopPointsperYear
        {
            get
            {
                return _onTopPointsperYear;
            }
        }
        public int TotalPointsGeneralEducation
        {
            get
            {
                return _totalpointsGeneralEducation;
            }
            private set
            {
                _totalpointsGeneralEducation = value;
            }
        }
        public int TotalPointsAgricultureAndEnvironment
        {
            get
            {
                return _totalpointsAgricultureAndEnvironment;
            }
            private set
            {
                _totalpointsAgricultureAndEnvironment = value;
            }
        }
        public int TotalPointsSportAndSociety
        {
            get
            {
                return _totalpointsSportAndSociety;
            }
            private set
            {
                _totalpointsSportAndSociety = value;
            }
        }
        public int TotalPointsCultureAndTradition
        {
            get
            {
                return _totalpointsCultureAndTradition;
            }
            private set
            {
                _totalpointsCultureAndTradition = value;
            }
        }
        public int TotalPointsSeviceAndOrganisation
        {
            get
            {
                return _totalpointsSeviceAndOrganisation;
            }
            private set
            {
                _totalpointsSeviceAndOrganisation = value;
            }
        }
        public int TotalPointsYouthAndInternationality
        {
            get
            {
                return _totalpointsYouthAndInternationality;
            }
            private set
            {
                _totalpointsYouthAndInternationality = value;
            }
        }

        #endregion

        #region constructor
        public Person() : this("ErrorName", "ErrorName",  new Address())
        {

        }
        public Person(string firstName, string surName, Address address)
        {
            FirstName = firstName;
            Surname = surName;
            _address = address;
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
        public static bool CheckLjID(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString) || inputString.Length == 8)
            {
                return false;
            }
            for (int i = 0; i < inputString.Length; i++)
            {
                if (!char.IsDigit(inputString[i]))
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
                case "PLZ":
                    {
                        return Address.CheckZipCode(inputStringformConsole);
                    }
                case "Ort":
                    {
                        return Address.CheckStreetorCity(inputStringformConsole);
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        public string ToCsvString(char seperator)
        {
            return FirstName + seperator + Surname + seperator + Address.ToCsvSring(seperator) + seperator;
        }
        public static Person ReadPersonFromcsv(string csvline, char seperator)
        {
            string[] parts = csvline.Split(seperator);
            Person readPerson = new Person(parts[0], parts[1], new Address(parts[2], parts[3], parts[4]));
            return readPerson;
        }
        #endregion

        #region override mehtods
        public override string ToString()
        {
            return "Vorname: " + _firstname + "Familienname: " + _surname + Address.ToString();
        }
        #endregion
    }
}
