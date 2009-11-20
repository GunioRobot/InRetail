using System;
using InRetail.Events;
using InRetail.Reporting;
using InRetail.Reporting.Dto;

namespace InRetail.EventHandlers
{
    public class CategoryCreatedEventHandler:IEventHandler<CategoryCreatedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public CategoryCreatedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(CategoryCreatedEvent e)
        {
            var report = new CategoryReport(e.CategoryId, Guid.Empty, e.Name);
            _reportingRepository.Save(report);
        }
    }
}