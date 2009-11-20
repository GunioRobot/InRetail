using System;
using InRetail.EventHandlers;
using InRetail.Events;
using InRetail.Reporting;
using InRetail.Reporting.Dto;
using Moq;

namespace Tests.InRetail.Scenarios.Adding_a_new_category
{
    public class When_new_category_was_created : EventTestFixture<CategoryCreatedEvent, CategoryCreatedEventHandler>
    {
        private Guid _categoryId;
        private CategoryReport SavedCategoryReport;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<CategoryReport>()))
                .Callback<CategoryReport>(a => SavedCategoryReport = a);
        }

        protected override CategoryCreatedEvent When()
        {
            _categoryId = Guid.NewGuid();
            return new CategoryCreatedEvent(_categoryId, "Cat Name");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_category_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<CategoryReport>()));
        }

        [Then]
        public void Then_the_category_report_will_be_updated_with_the_expected_details()
        {
            SavedCategoryReport.Id.WillBe(_categoryId);
            SavedCategoryReport.Name.WillBe("Cat Name");
            SavedCategoryReport.CategoryReportId.WillBe(Guid.Empty);
        }
    }
}