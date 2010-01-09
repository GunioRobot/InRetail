using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace InRetail.EntityPresentation
{
    public class EntityPresenter<T> : IScreen<T> where T : IEntity
    {
        private readonly IEntityView<T> _view;
        private readonly T _subject;

        public EntityPresenter(IEntityPartProvider<T> entityPartProvider,IEntityView<T> view, T subject)
        {
            _view = view;
            _subject = subject;
            EntityParts = new ObservableCollection<EntityPartPresenter>();
            Title = subject.GetEntityScreenName();
            entityPartProvider.GetEntityParts().Run(x => EntityParts.Add(x));
        }

        public IList<EntityPartPresenter> EntityParts { get; set; }   
        
        
        public object View
        {
            get { return _view;}
        }

        public string Title { get; set; }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
            
        }

        public bool CanClose()
        {
            return true;
        }

        public void Dispose()
        {
            
        }

        public T Subject
        {
            get { return _subject; }
        }
    }
}