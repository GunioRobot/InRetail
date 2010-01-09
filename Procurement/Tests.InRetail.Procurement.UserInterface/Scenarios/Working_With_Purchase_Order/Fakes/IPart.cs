using System;
using NServiceBus;

namespace Tests.InRetail.Procurement.Scenarios.Working_With_Purchase_Order.Fakes
{
    public interface IPart
    {
        IMessage GetMessage();
        IObservable<Unit> Confirmed { get; set; }
    }
}