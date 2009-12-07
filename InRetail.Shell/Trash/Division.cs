using System.Collections.Generic;

namespace InRetail.Shell.Trash
{
    public class Division
    {
        public Division(string name)
        {
            _name = name;
            _teams = new List<Team>();

        }

        string _name;

        public string Name { get { return _name; } }

        List<Team> _teams;

        public List<Team> Teams { get { return _teams; } }

    }
}