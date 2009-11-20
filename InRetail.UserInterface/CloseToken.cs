using System.Collections.Generic;

namespace InRetail.UserInterface
{
    public class CloseToken
    {
        private readonly List<string> _messages = new List<string>();

        public string[] Messages { get { return _messages.ToArray(); } }

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }
}