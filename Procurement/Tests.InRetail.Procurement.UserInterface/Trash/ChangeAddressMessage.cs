using NServiceBus;

namespace Tests.InRetail.Procurement
{
    public class ChangeAddressMessage : IMessage
    {
        public string Address1 { get; set; }
        public string City { get; set; }
    }
}