namespace Tests.InRetail.Procurement
{
    public class CreatePersonMessageMap : ModelToMessageMap<PersonModel, CreatePersonMessage>
    {
        public CreatePersonMessageMap()
        {
            Map(x => x.FName).To(x => x.FirstName);
            Map(x => x.LName).To(x => x.LastName);
        }
    }
}