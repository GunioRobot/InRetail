namespace InRetail.EntityPresentation
{
    public interface IEntityView<T> where T : IEntity
    {
        void Bind(IPartPresenter partPresenter);
    }
}