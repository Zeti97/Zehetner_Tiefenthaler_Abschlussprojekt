using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiefenthaler_Zehetner_Webshop
{
    public class Address //Tiefenthaler
    {
        #region private members
        private string _street;
        private string _number;
        private string _countryCode;
        private string _zipcode;
        private string _city;
        #endregion

        #region public properties
        public string Street
        {
            get
            {
                return _street;
            }
            private set
            {
                if (CheckStreetorCity(value))
                {
                    _street = value;
                }
                else
                {
                    new Exception("Invalid Input of Street.");
                }
            }
        }
        public string Number
        {
            get
            {
                return _number;
            }
            private set
            {
                if (CheckNumber(value))
                {
                    _number = value;
                }
                else
                {
                    new Exception("Invalid Input of Number");
                }
            }
        }
        public string CountryCode
        {
            get
            {
                return _countryCode;
            }
            private set
            {
                if (CheckCountryCode(value))
                {
                    _countryCode = value;
                }
                else
                {
                    new Exception("Invalid Input of Countrycode.");
                }
            }
        }
        public string ZipCode
        {
            get
            {
                return _zipcode;
            }
            private set
            {
                if (CheckZipCode(value))
                {
                    _zipcode = value;
                }
                else
                {
                    new Exception("Invalid Input of Zipcode.");
                }
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            private set
            {
                if (CheckStreetorCity(value))
                {
                    _city = value;
                }
                else
                {
                    new Exception("Invalid input of City.");
                }
            }
        }
        #endregion

        #region constructor
        public Address(string street, string number, string countrycode, string zipcode, string city)
        {
            Street = street;
            Number = number;
            CountryCode = countrycode;
            ZipCode = zipcode;
            City = city;
        }
        public Address() : this("Musterstraße", "1", "A", "1234", "Musterstadt")
        {

        }
        #endregion

        #region public methods
        public static bool CheckStreetorCity(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString) || inputString.Length < 2 || !char.IsUpper(inputString[0])) // Inhalt, Länge und Großbuchstaben prüfen
            {
                return false;
            }
            for (int i = 0; i < inputString.Length; i++)
            {
                if (!(char.IsLetter(inputString[i]) || char.IsWhiteSpace(inputString[i]) || Equals(inputString[i], '-') || 
                    Equals(inputString[i], '.') || Equals(inputString[i], '/') || Equals(inputString[i], '&'))) // beinhalte Zeichen prüfen
                {
                    return false;
                }
                if (char.IsWhiteSpace(inputString[i]) || Equals(inputString[i], '-') || Equals(inputString[i], '.') || 
                    Equals(inputString[i], '/') || Equals(inputString[i], '&')) // keine hintereinanderfolgende Sonderzeichen
                {
                    if (Equals(inputString[i], inputString[i+1]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool CheckZipCode(string inputStringNumber)
        {
            if (!((inputStringNumber.Length == 4) || (inputStringNumber.Length == 5)) || 
                   string.IsNullOrWhiteSpace(inputStringNumber) || Equals(inputStringNumber[0], '0')) // Länge der Zahl prüfen
            {
                return false;
            }
            for (int i = 0; i < inputStringNumber.Length; i++) // alle Zeichen sind eine Ziffer
            {
                if (!(char.IsDigit(inputStringNumber[i])))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckNumber(string inputStringNumber)
        {
            if (!(inputStringNumber.Length <= 5) || Equals(inputStringNumber[0], '0') || string.IsNullOrWhiteSpace(inputStringNumber)) // Längenprüfung, startet nicht mit 0 und letztes Zeichen darf ein kleiner Buchstabe sein
            {
                return false;
            }
            int correctlength;
            if (char.IsLetter(inputStringNumber[inputStringNumber.Length - 1]) && char.IsLower(inputStringNumber[inputStringNumber.Length - 1]))
            {
                correctlength = inputStringNumber.Length - 1;
            }
            else
            {
                correctlength = inputStringNumber.Length;
            }
            for (int i = 0; i < correctlength; i++) // alle Zeichen sind eine Ziffer
            {
                if (!(char.IsDigit(inputStringNumber[i])))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckCountryCode(string inputCountrycode)
        {
            if (string.IsNullOrWhiteSpace(inputCountrycode) || inputCountrycode.Length > 3)
            {
                return false;
            }
            for (int i = 0; i < inputCountrycode.Length; i++)
            {
                if (!(char.IsLetter(inputCountrycode[i]) || char.IsUpper(inputCountrycode[i])))
                {
                    return false;
                }
            }
            return true;
        }
        public string ToCsvSring(char seperator)
        {
            return Street + seperator + Number + seperator + CountryCode + seperator + ZipCode + seperator + City;
        }
        #endregion

        #region override methods
        public override string ToString()
        {
            return "Straße: " + _street + "Hausnummer: " + _number + "Länderkennung: " + _countryCode + "PLZ: " + _zipcode + "Gemeinde: " + _city;
        }
        #endregion
    }
}
