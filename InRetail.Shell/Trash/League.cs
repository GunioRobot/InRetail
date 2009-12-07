using System.Collections.Generic;

namespace InRetail.Shell.Trash
{
    public class League
    {
        public League(string name)
        {
            _name = name;
            _divisions = new List<Division>();
        }


        string _name;

        public string Name { get { return _name; } }

        List<Division> _divisions;
        public List<Division> Divisions { get { return _divisions; } }

    }
}