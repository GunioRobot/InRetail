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
        private readonly List<IScreen> _screens = new List<IScreen>();

        public ScreenCollection(IRegion mainRegion)
        {
            _mainRegion = mainRegion;
        }

        public void Start()
        {
        }

        public IScreen Active
        {
            get
            {
                var view = _mainRegion.ActiveViews.FirstOrDefault();
                if (view == null)
                    return null;
                return _screens.First(x => x.View == view);

            }
        }

        public IEnumerable<IScreen> AllScreens
        {
            get
            {
                return _screens.AsReadOnly();
            }
        }

        public void Add(IScreen screen)
        {
            _screens.Add(screen);
            _mainRegion.Add(screen.View);
        }

        public void ClearAll()
        {
            _screens.Clear();
            _mainRegion.Views.ToList().ForEach(_mainRegion.Remove);
        }

        public void Remove(IScreen screen)
        {
            _mainRegion.Remove(_screens.Find(x => x == screen).View);
            _screens.Remove(screen);
        }

        public void RenameTab(IScreen screen, string name)
        {
            throw new NotImplementedException();
        }

        public void Show(IScreen screen)
        {
            IScreen find = _screens.Find(x => x == screen);
            _mainRegion.Activate(find.View);
        }
    }
}