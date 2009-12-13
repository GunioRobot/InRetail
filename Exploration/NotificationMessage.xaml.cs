namespace Exploration
{
    public class NotificationMessage

    {
        public override string ToString()
        {
            return string.Format("Message: {0}", Message);
        }

        public string Message { get; set; }
    }
}