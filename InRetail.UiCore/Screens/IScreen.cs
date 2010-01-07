using System;
using InRetail.UiCore.Actions;


namespace InRetail.UiCore.Screens
{
    public interface IScreen : IDisposable
    {
        // What's the actual UI control to display?
        object View { get; }
        string Title { get; }

        // Sets up the ancillary screen elements like menus and
        // extra controls in the explorer pane
        void Activate(IScreenObjectRegistry screenObjects);

        // Implements the traditional "I can't close because
        // "you have unsaved changes" functionality
        bool CanClose();
    }

    public interface IScreen<T> : IScreen
    {
        T Subject { get; }
    }
}