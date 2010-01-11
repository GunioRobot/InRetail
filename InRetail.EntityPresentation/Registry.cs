using StructureMap.Configuration.DSL;

namespace InRetail.EntityPresentation
{
    public class EntityPresentationRegistry : Registry
    {
        public EntityPresentationRegistry()
        {

            ForRequestedType(typeof(IEntityPartProvider<>)).TheDefaultIsConcreteType(typeof(EntityPartProvider<>));
            //ForRequestedType(typeof(IEntityView<>)).TheDefaultIsConcreteType(typeof(DefaultEntityView));
        }
    }

   
}