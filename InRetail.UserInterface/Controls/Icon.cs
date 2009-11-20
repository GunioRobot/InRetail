using System;
using System.IO;
using System.Reflection;
using InRetail.UserInterface.Icons;

namespace InRetail.UserInterface.Controls
{
    public class Icon
    {
        public static Icon Unknown = new Icon("statusUnknown.png", 6);
        public static Icon Close= new Icon("delete.png", 6);
        private readonly int _order;
        private readonly string _path;

        private Icon(string path, int order)
        {
            _path = path;
            _order = order;
        }
        public Stream ImageStream()
        {
            return IconMarker.GetImage(_path);
        }
    }
    
}