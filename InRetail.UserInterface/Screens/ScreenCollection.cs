using System.Collections.Generic;
using System.Windows.Controls;
using InRetail.UserInterface.Controls;
using InRetail.UserInterface.Eventing;
using InRetail.UserInterface.Extensions;
using InRetail.UserInterface.Messages;

namespace InRetail.UserInterface.Screens
{
    public class ScreenCollection : IScreenCollection
    {
        private readonly Cache<IScreen, InRetailTabItem> _tabItems = new Cache<IScreen, InRetailTabItem>();
        private readonly TabControl _tabs;

        public ScreenCollection(TabControl tabs, IEventAggregator events)
        {
            _tabs = tabs;
            _tabItems.OnMissing = screen => new InRetailTabItem(screen, events);

            // Sends a message when the user select a different tab on the screen
            _tabs.SelectionChanged += (s, c) => events.SendMessage<UserScreenActivation>();
        }

        #region IScreenCollection Members

        public void ClearAll()
        {
            _tabs.Items.Clear();
        }

        public IScreen Active
        {
            get
            {
                // If there is a selected tab, this 
                // will return the IScreen object
                // matching the selected tab
                if (_tabs.SelectedItem != null)
                {
                    return toScreen(_tabs.SelectedItem);
                }
                ;

                return null;
            }
        }

        public void Show(IScreen screen)
        {
            _tabs.SelectedItem = _tabItems[screen];
        }

        public void Add(IScreen screen)
        {
            // Add a new screen to the tabbed display
            InRetailTabItem cache = _tabItems[screen];
            _tabs.Items.Add(cache);
        }

        public void Remove(IScreen screen)
        {
            TabItem tabItem = _tabItems[screen];
            _tabItems.Remove(screen);
            _tabs.Items.Remove(tabItem);
        }

        public void RenameTab(IScreen screen, string name)
        {
            _tabItems[screen].HeaderText = name;
        }

        public IEnumerable<IScreen> AllScreens { get { return new List<IScreen>(_tabItems.Keys()); } }

        public void Start()
        {
            // no-op
        }

        #endregion

        private IScreen toScreen(object tab)
        {
            return tab.As<TabItem>().Tag.As<IScreen>();
        }
    }
}