using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;
using Microsoft.Practices.Composite.Regions;

namespace InRetail.EntityPresentation
{
    public class EntityPresenter<T> : IScreen<T>,INeedRegionManager where T : IEntity
    {
        private readonly IEntityView<T> _view;
        private readonly T _subject;
        private readonly IEnumerable<IPartPresenter> _partPresenters;

        public EntityPresenter(IEntityView<T> view, T subject, IEnumerable<IPartPresenter> partPresenters)
        {
            _view = view;
            _subject = subject;
            _partPresenters = partPresenters;
            Title = subject.GetEntityScreenName();
            _partPresenters.Run(view.Bind);
        }

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
            return !_partPresenters.Any(x => x.CanClose() == false);
        }

        public void Initialize(IRegionManager regionManager)
        {
            throw new NotImplementedException();
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