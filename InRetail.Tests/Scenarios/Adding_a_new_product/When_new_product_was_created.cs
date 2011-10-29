using System;
using InRetail.EventHandlers;
using InRetail.Events.Products;
using InRetail.Reporting;
using InRetail.Reporting.Dto;
using Moq;

namespace Tests.InRetail.Scenarios.Adding_a_new_product
{
    public class When_new_product_was_created:EventTestFixture<ProductCreatedEvent,ProductCreatedEventHandler>
    {
        private Guid _productId;
        private ProductReport SavedProductObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ProductReport>()))
                .Callback<ProductReport>(a => SavedProductObject = a);


        }
        protected override ProductCreatedEvent When()
        {
            _productId = Guid.NewGuid();
            return new ProductCreatedEvent(_productId, "Test product name");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_product_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ProductReport>()));
        }

        [Then]
        public void Then_the_client_report_will_be_updated_with_the_expected_details()
        {
            SavedProductObject.Id.WillBe(_productId);
            SavedProductObject.Name.WillBe("Test product name");
        }
    }
}