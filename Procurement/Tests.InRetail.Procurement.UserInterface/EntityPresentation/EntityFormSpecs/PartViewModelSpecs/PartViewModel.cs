namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.PartViewModelSpecs
{
    public class PartViewModel
    {
        private readonly IPartView _view;

        public PartViewModel(IPartView view)
        {
            _view = view;
        }

        public IPartView View
        {
            get { return _view; }
        }
    }
}