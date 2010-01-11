namespace InRetail.EntityPresentation
{
    public interface IEntityPartView
    {
        void SwitchToEditMode(IMessageView messageView);
        void SwitchToViewMode();
    }
}