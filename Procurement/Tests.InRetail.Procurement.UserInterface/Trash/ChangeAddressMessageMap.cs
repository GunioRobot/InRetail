namespace Tests.InRetail.Procurement
{
    public class ChangeAddressMessageMap : ModelToMessageMap<PersonModel, ChangeAddressMessage>
    {
        public ChangeAddressMessageMap()
        {
            Map(x => x.Address1).To(x => x.Address);
            Map(x => x.City).To(x => x.City);
        }
    }
}