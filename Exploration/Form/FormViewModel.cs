using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Exploration.Form
{
    public class FormViewModel
    {
        private readonly IFormView _view;

        public FormViewModel(IFormView view)
        {
            _view = view;
            _view.Bind(this);
            Parts = new ObservableCollection<PartViewModel>();
        }

        public IList<PartViewModel> Parts { get; private set; }

        public void AddPart(PartViewModel model)
        {
            Parts.Add(model);
        }
    }

    public class PartViewModel
    {
        public int ColSpan { get; set; }
    }
}