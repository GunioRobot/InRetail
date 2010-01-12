using System;
using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    internal class PresentationModel
    {
        IList<Part> _parts=new List<Part>();
        public IList<Part> Parts
        {
            get { return new List<Part>(_parts).AsReadOnly(); } 
        }
        public string Title { get; set; }

        public void AddPart(Part part)
        {
            _parts.Add(part);
        }
    }
}