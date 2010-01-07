using System.Collections.Generic;
using NServiceBus;
using System;

namespace Tests.InRetail.Procurement.UserInterface.EntityModel
{
    public interface IEntityPartProvider
    {
        void GetParts();
        IPart GetEntityConstructionPart();
    }

    public interface IPart
    {
        IMessage GetMessage();
        IObservable<Unit> Confirmed { get; set; }
    }
}