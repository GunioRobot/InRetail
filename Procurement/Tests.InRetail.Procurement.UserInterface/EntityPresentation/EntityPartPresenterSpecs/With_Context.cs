using System.Collections.Generic;
using InRetail.EntityPresentation;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs
{
    public class With_Context : Specification
    {
        #region fields

        protected IEntityPartView partView;
        protected IList<IMessageMap> messageMaps;
        protected IPart part;
        protected EntityPartPresenter partPresenter;

        #endregion

        public With_Context()
        {
            #region part setup

            part = Moq.Mock<IPart>();
            var messageMap = Moq.Mock<IMessageMap>();
            messageMap.SetupGet(x => x.Name).Returns("SomeMessage Name");
            messageMaps = new[] { messageMap };
            part.SetupGet(x => x.MessageMaps).Returns(messageMaps);

            #endregion

            partView = Moq.Mock<IEntityPartView>();
        }
    }
}