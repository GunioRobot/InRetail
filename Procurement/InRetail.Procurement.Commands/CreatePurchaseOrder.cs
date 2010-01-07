using System;
using System.Collections.Generic;
using NServiceBus;

namespace InRetail.Procurement.Commands
{
    public class CreatePurchaseOrder : IMessage
    {
        public CreatePurchaseOrder()
        {
            Lines=new List<AddLineToPurchaseOrder>();
        }
        public Guid OrderId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid WhereHouseId { get; set; }
        public IList<AddLineToPurchaseOrder> Lines { get; set; }

        #region Equality for tests

        public bool Equals(CreatePurchaseOrder other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Lines.Count != other.Lines.Count) return false;
            for (int i = 0; i < Lines.Count; i++)
                if(!Lines[i].Equals(other.Lines[i]))
                    return false;
            return other.OrderId.Equals(OrderId) && other.SupplierId.Equals(SupplierId) && other.WhereHouseId.Equals(WhereHouseId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (CreatePurchaseOrder)) return false;
            return Equals((CreatePurchaseOrder) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = OrderId.GetHashCode();
                result = (result*397) ^ SupplierId.GetHashCode();
                result = (result*397) ^ WhereHouseId.GetHashCode();
                return result;
            }
        }

        #endregion

    }
}