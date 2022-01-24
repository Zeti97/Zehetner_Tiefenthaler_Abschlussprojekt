using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt
{
    class Event
    {
        #region members
        string _name;
        Enums.emphasis _emphasis;
        string _category;
        double _points;
        string _marker; 
        #endregion

        #region constructor
        public Event()
        {
            _name = "Default";
            _emphasis = Enums.emphasis.Default;
            _category = "Default";
            _points = 0;
            _marker = "";
        }
        public Event(string name, Enums.emphasis emphasis, string category, double point, string marker)
        {
            _name = name;
            _emphasis = emphasis;
            _category = category;
            _points = point;
            _marker = marker;
        }
        #endregion

        #region methoden

        #endregion

    }
}
