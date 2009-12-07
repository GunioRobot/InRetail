namespace InRetail.Shell.Trash
{
    public class Team
    {
        public Team(string name)
        {
            _name = name;
        }

        string _name;

        public string Name { get { return _name; } }
    }
}