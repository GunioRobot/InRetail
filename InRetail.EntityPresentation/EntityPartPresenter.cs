using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InRetail.EntityPresentation
{
    public class EntityPartPresenter
    {
        public EntityPartPresenter()
        {
        }

        public EntityPartPresenter(string title, IEnumerable<IEntityFieldPresenter> entityFieldPresenters,
                                   IEnumerable<IEntityMessagePresenter> messagePresenters)
        {
            Title = title;

            EntityFields = new ObservableCollection<IEntityFieldPresenter>();
            entityFieldPresenters.Run(EntityFields.Add);

            EntityMessages = new ObservableCollection<IEntityMessagePresenter>();
            messagePresenters.Run(EntityMessages.Add);
        }

        public string Title { get; private set; }

        public IList<IEntityFieldPresenter> EntityFields { get; private set; }
        public IList<IEntityMessagePresenter> EntityMessages { get; private set; }
    }
}