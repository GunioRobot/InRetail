using System;
using InRetail.EntityPresentation;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;
using Moq;
using StructureMap;

namespace Tests.InRetail.Procurement.EntityPresentation.Integration
{
    public class When_Activating_Screen_For_Entity:Specification
    {
        protected IContainer container;
        

        public When_Activating_Screen_For_Entity()
        {
            ObjectFactory.ResetDefaults();
            ObjectFactory.Configure(x=>x.AddRegistry<EntityPresentationRegistry>());
            container = ObjectFactory.Container;
        }

        public override void Given()
        {
        
        }

        public override void When()
        {
            
        }

        [It]
        public void aa()
        {
            
        }
    }
}