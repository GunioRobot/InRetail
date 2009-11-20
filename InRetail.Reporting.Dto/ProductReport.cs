using System;

namespace InRetail.Reporting.Dto
{
    public class ProductReport
    {
        public ProductReport(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}