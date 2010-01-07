using System;
using NServiceBus;


namespace InRetail.Procurement.Commands
{
    public class AddLineToPurchaseOrder:IMessage
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid LineId { get; set; }

        #region Equality For Tests

        public bool Equals(AddLineToPurchaseOrder other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.LineId.Equals(LineId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AddLineToPurchaseOrder)) return false;
            return Equals((AddLineToPurchaseOrder) obj);
        }

        public override int GetHashCode()
        {
            return LineId.GetHashCode();
        }

        #endregion
    }
}