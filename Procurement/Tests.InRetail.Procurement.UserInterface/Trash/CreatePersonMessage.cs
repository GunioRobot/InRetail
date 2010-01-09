using NServiceBus;

namespace Tests.InRetail.Procurement
{
    public class CreatePersonMessage : IMessage
    {
        public string FName { get; set; }
        public string LName { get; set; }
    }
}