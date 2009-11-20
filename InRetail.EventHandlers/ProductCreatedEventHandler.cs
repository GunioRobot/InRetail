using System;
using InRetail.Events.Products;
using InRetail.Reporting;
using InRetail.Reporting.Dto;

namespace InRetail.EventHandlers
{
    public class ProductCreatedEventHandler : IEventHandler<ProductCreatedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ProductCreatedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ProductCreatedEvent e)
        {
            var productReport = new ProductReport(e.ProductId, e.Name);

            _reportingRepository.Save(productReport);
        }
    }
}