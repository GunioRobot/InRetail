using System;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityScreenSpecs
{
    public class EntityScreen<T> : IScreen<T>
    {
        private readonly T _subject;
        private readonly IEntityView _view;
        private readonly IPresentationModel _presentationModel;

        public EntityScreen()
        {

        }

        public EntityScreen(T subject, IModelBuilder modelBuilder, IModelViewModelLocator viewModelLocator, IEntityView view)
        {
            _subject = subject;
            _view = view;
            _presentationModel = modelBuilder.Build(_subject);
            var presentationViewModel = viewModelLocator.BuildViewModel(_presentationModel);
            view.ShowModelView(presentationViewModel.View);
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
}