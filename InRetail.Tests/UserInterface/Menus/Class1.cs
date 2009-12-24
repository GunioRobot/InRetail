using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

using Xunit;
using Xunit.Sdk;

namespace Tests.InRetail.UserInterface.Menus
{
    public abstract class Bihave_Context:Specification
    {
        protected Foo foo;

        public override void InitializeContext()
        {
            this.foo = new Foo();
        }

    }

    public class Foo
    {
        public bool IsActionCalled;

        public void Action()
        {
            IsActionCalled = true;
        }
    }

    public class Class1 : Bihave_Context
    {
        public override void Observe()
        {
            foo.Action();
        }

        [Observation]
        public void Test()
        {
            Assert.Equal(true, foo.IsActionCalled);
        }

        
    }

   
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ObservationAttribute : FactAttribute
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

        public ObservationCommand(ITestCommand innerCommand,IMethodInfo methodInfo)
        {
            _innerCommand = innerCommand;
            _methodInfo = methodInfo;
        }

        public MethodResult Execute(object testClass)    
        {        
            if(testClass is Specification)
            {
                var specification = (Specification)testClass;             
                specification.InitializeContext();          
                specification.Observe();
            }         return _innerCommand.Execute(testClass);   
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
        public abstract void InitializeContext();
        public abstract void Observe();
    }
}