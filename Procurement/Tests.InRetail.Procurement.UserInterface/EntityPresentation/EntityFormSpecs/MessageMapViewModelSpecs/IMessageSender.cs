using System;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.MessageMapViewModelSpecs
{
    public interface IMessageSender
    {
        void SendMessage(Type messageType);
    }
}