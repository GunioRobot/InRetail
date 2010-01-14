using System;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs
{
    public class EntityForm<T> : IEntityForm<T>
    {
        private readonly T _subject;
        private readonly IEntityView _view;
        private IPresentationModel _presentationModel;

        public EntityForm()
        {

        }

        public EntityForm(IPresentationModel subject, IEntityView view)
        {
            _presentationModel = subject;
            _view = view;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object View
        {
            get { return _view; }
        }
        public string Title
        {
            get { return _presentationModel.Title; }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
            throw new NotImplementedException();
        }

        public bool CanClose()
        {
            throw new NotImplementedException();
        }

        public T Subject
        {
            get { return _subject; }
        }
    }

    public interface IEntityForm<T> : IScreen<T> { }
}