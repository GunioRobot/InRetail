using System.Collections.Generic;
using System.Linq;
using InRetail.UiCore.Extensions;
using NServiceBus;
using Xunit;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement
{
    public class MessageBuilderWithNewModelTests
    {
        private PersonModel model;
        protected MessageBuilder<PersonModel> bulder;

        public MessageBuilderWithNewModelTests()
        {
            model = new PersonModel();

            bulder = new MessageBuilder<PersonModel>(model);

            bulder.Add(new CreatePersonMessageMap());
            bulder.Add(new ChangeAddressMessageMap());
        }

        [Fact]
        public void GetMessages()
        {
            model.FirstName = "Archil";
            model.LastName = "Bolkvadze";
            model.Address = "Digomi";
            model.City = "Tbilisi";

            List<IMessage> list = bulder.GetMessages().ToList();

            var message = list[0].As<CreatePersonMessage>();
            message.FName.ShouldEqual("Archil");
            message.LName.ShouldEqual("Bolkvadze");
            

            var addressMessage = list[1].As<ChangeAddressMessage>();
            addressMessage.Address1.ShouldEqual("Digomi");
            addressMessage.City.ShouldEqual("Tbilisi");
        }

        [Fact]
        public void GetMessages2()
        {
            List<IMessage> list = bulder.GetMessages().ToList();

            list.ShouldBeEmpty();
        }

    }
}