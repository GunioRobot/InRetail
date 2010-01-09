using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public class EntityPresenter<T> where T : IEntity
    {
        public EntityPresenter(IEntityPartProvider<T> entityPartProvider, T purchaseOrder)
        {
            EntityParts = new ObservableCollection<EntityPartPresenter>();
            Title = purchaseOrder.GetEntityScreenName();
            entityPartProvider.GetEntityParts().Run(x => EntityParts.Add(x));
        }

        public string Title { get; set; }


        public IList<EntityPartPresenter> EntityParts { get; set; }
    }
}