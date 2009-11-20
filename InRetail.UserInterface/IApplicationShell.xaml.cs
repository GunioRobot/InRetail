namespace InRetail.UserInterface
{
    public interface IApplicationShell {
        void RemoveFromExplorerPane(object view);
        void PlaceInExplorerPane(object view, string header);
    }
}