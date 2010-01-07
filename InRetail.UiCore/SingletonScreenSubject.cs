using System;
using InRetail.UiCore.Screens;

namespace InRetail.UiCore
{
    public class ScreenSubject<T> : IScreenSubject<T>
    {
        private readonly T _subject;

        public ScreenSubject(T subject)
        {
            _subject = subject;
        }

        #region IScreenSubject<T> Members

        public bool Matches(IScreen screen)
        {
            var specific = screen as IScreen<T>;
            if (specific == null) return false;

            return specific.Subject.Equals(_subject);
        }

        public IScreen CreateScreen(IScreenFactory factory)
        {
            return factory.Build(_subject);
        }

        #endregion

        public bool Equals(ScreenSubject<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._subject, _subject);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ScreenSubject<T>)) return false;
            return Equals((ScreenSubject<T>)obj);
        }

        public override int GetHashCode()
        {
            return _subject.GetHashCode();
        }
    }
    public class SingletonScreenSubject<TScreen> : IScreenSubject where TScreen : IScreen
    {
        private Type _screenType;

        public SingletonScreenSubject()
        {
            _screenType = typeof (TScreen);
        }

        public bool Matches(IScreen screen)
        {
            return screen is TScreen;
        }

        public IScreen CreateScreen(IScreenFactory factory)
        {
            return factory.Build<TScreen>();
        }

        #region Equality for testing

        public bool Equals(SingletonScreenSubject<TScreen> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._screenType, _screenType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SingletonScreenSubject<TScreen>)) return false;
            return Equals((SingletonScreenSubject<TScreen>)obj);
        }

        public override int GetHashCode()
        {
            return _screenType.GetHashCode();
        }

        #endregion
    }
}