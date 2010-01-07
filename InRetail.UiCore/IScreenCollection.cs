using System.Collections.Generic;
using InRetail.UiCore.Screens;

namespace InRetail.UiCore
{
    public interface IScreenCollection : IStartable
    {
        // Which screen is active?
        IScreen Active { get; }

        // What screens are currently in the main tabbed area
        IEnumerable<IScreen> AllScreens { get; }

        // Remove all screens from the tabbed display

        // Add a new tab with this screen to the tabbed display
        void Add(IScreen screen);

        void ClearAll();

        // Remove this screen from the tabbed display
        void Remove(IScreen screen);

        void RenameTab(IScreen screen, string name);

        void Show(IScreen screen);
    }
}