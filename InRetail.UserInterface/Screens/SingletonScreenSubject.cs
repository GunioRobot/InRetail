namespace InRetail.UserInterface.Screens
{
    public class SingletonScreenSubject<SCREEN> : IScreenSubject where SCREEN : IScreen
    {
        #region IScreenSubject Members

        public bool Matches(IScreen screen)
        {
            return screen is SCREEN;
        }

        public IScreen CreateScreen(IScreenFactory factory)
        {
            return factory.Build<SCREEN>();
        }

        #endregion
    }
}