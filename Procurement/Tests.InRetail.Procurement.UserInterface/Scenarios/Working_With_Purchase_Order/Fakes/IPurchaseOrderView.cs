namespace Tests.InRetail.Procurement.Scenarios.Working_With_Purchase_Order.Fakes
{
    public interface IPurchaseOrderView
    {
        void ShowDialog(IPart constructionPart);
        void CloseDialog();
        void Busy();
    }
}