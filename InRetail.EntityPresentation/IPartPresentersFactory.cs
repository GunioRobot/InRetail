namespace InRetail.EntityPresentation
{
    public interface IPartPresentersFactory
    {
        IPartPresenter GetPartPresenter(IPart part);
    }

    public interface IPartPresenter {
        bool CanClose();
    }
}