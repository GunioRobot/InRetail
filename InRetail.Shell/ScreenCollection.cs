using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.UiCore;
using Microsoft.Practices.Composite.Regions;

namespace InRetail.Shell
{
    public class ScreenCollection : IScreenCollection
    {
        private readonly IRegion _mainRegion;

        public ScreenCollection(IRegion mainRegion)
        {
            _mainRegion = mainRegion;
        }

        public void Start()
        {
        }

        public IScreen Active
        {
            get { return (IScreen) _mainRegion.ActiveViews.First(); }
        }

        public IEnumerable<IScreen> AllScreens
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(IScreen screen)
        {
            _mainRegion.Add(screen);
            _mainRegion.Activate(screen);
        }

        public void ClearAll()
        {
            _mainRegion.Views.ToList().ForEach(_mainRegion.Remove);
        }

        public void Remove(IScreen screen)
        {
            _mainRegion.Remove(screen);
        }

        public void RenameTab(IScreen screen, string name)
        {
            throw new NotImplementedException();
        }

        public void Show(IScreen screen)
        {
            _mainRegion.Activate(screen);
        }
    }
}