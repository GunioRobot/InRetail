using System;
using System.Collections.Generic;

namespace InRetail.Reporting.Dto
{
    public class CategoryReport
    {
        public CategoryReport(Guid id, Guid categoryReportId, string name)
        {
            Id = id;
            CategoryReportId = categoryReportId;
            Name = name;
            Categories = new List<CategoryReport>();
        }

        public Guid Id { get; private set; }
        public Guid CategoryReportId { get; private set; }

        public string Name { get; private set; }
        public IEnumerable<CategoryReport> Categories { get; private set; }

        public override string ToString()
        {
            return string.Format("Name: {0}", Name);
        }
    }
}