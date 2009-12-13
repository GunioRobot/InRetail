using System;
using System.Collections.Generic;

namespace Tests.InRetail.Exploration
{
    public class ChangeSet
    {
        public IEnumerable<Row> Inserts { get; set; }
        public IEnumerable<Row> Updates { get; set; }
        public IEnumerable<Row> Deletes { get; set; }
    }
}