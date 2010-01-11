using System.Windows.Input;

namespace InRetail.EntityPresentation
{
    public class MessageCommandViewModel
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }
}