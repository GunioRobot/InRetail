using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Xml;
using Moq;
using Moq.Language.Flow;
using Xunit;
using Xunit.Sdk;

namespace Tests.InRetail.Procurement
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ItAttribute : FactAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            foreach (ITestCommand command in base.EnumerateTestCommands(method))
            {
                yield return new ObservationCommand(command, method);
            }
        }
    }

    public class ObservationCommand : ITestCommand
    {
        private readonly ITestCommand _innerCommand;
        private readonly IMethodInfo _methodInfo;

        public ObservationCommand(ITestCommand innerCommand, IMethodInfo methodInfo)
        {
            _innerCommand = innerCommand;
            _methodInfo = methodInfo;
        }

        [DebuggerNonUserCode]
        public MethodResult Execute(object testClass)
        {
            if (_methodInfo.MethodInfo.DeclaringType!=testClass.GetType())
                return new PassedResult(_methodInfo,null);

            if (testClass is Specification)
            {
                var specification = (Specification)testClass;
                specification.Given();
                specification.When();
            }
            var methodResult = _innerCommand.Execute(testClass);
            return methodResult;
        }

        public XmlNode ToStartXml()
        {
            return _innerCommand.ToStartXml();
        }

        public string DisplayName
        {
            get { return _innerCommand.DisplayName; }
        }

        public bool ShouldCreateInstance
        {
            get { return _innerCommand.ShouldCreateInstance; }
        }
    }

    public abstract class Specification
    {
        public virtual void Given()
        {
        }

        public virtual void When()
        {
        }


    }

    public static class MoqExtension
    {
        [DebuggerNonUserCode]

        public static void Verify<T>(this T o, Expression<Action<T>> a) where T : class
        {
            Mock.Get<T>(o).Verify(a);
        }
        [DebuggerNonUserCode]
        public static void Verify<T>(this T o, Expression<Action<T>> a, Times times) where T : class
        {
            Mock.Get<T>(o).Verify(a, times);
        }

        [DebuggerNonUserCode]
        public static ISetup<T> Setup<T>(this T o, Expression<Action<T>> a) where T : class
        {
            return Mock.Get<T>(o).Setup(a);
        }

        [DebuggerNonUserCode]
        public static ISetup<T, TResult> Setup<T, TResult>(this T o, Expression<Func<T, TResult>> expression) where T : class
        {
            return Mock.Get<T>(o).Setup(expression);
        }

        [DebuggerNonUserCode]
        public static ISetupGetter<T, TProperty> SetupGet<T, TProperty>(this T o, Expression<Func<T, TProperty>> expression) where T : class
        {
            return Mock.Get<T>(o).SetupGet(expression);
        }

        [DebuggerNonUserCode]
        public static Mock<T> Moq<T>(this T o) where T : class
        {
            return Mock.Get<T>(o);
        }
    }
}