namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.PresentationViewModel
{
    public class PresentationViewModel : IPresentationViewModel
    {
        private readonly IPresentationView _view;

        public PresentationViewModel(IPresentationView view)
        {
            _view = view;
        }

        public IPresentationView View
        {
            get { return _view; }
        }
    }
}