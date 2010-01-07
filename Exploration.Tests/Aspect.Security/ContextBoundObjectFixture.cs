using System;
using System.Diagnostics;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using Xunit;

namespace Exploration.Tests.Aspect.Security
{

    [Tracing()]
    [Security()]
    public class MyClass : ContextBoundObject
    {
        public int i;
        public int ProcessString(String s, out string outStr)
        {
            Console.WriteLine("Inside ProcessString");
            outStr = s.ToUpper();
            return outStr.Length;
        }

        public int I
        {
            get { return i; }
            set { i = value; }
        }
    }

    public class ContextBoundObjectFixture
    {
        [Fact]
        public void Ttt()
        {

            MyClass traceMe = new MyClass();
            traceMe.i = 15;
            traceMe.I++;
            string outstring;
            traceMe.ProcessString("Abhinaba", out outstring);
        }
    }



    internal class TracingAspect : IMessageSink
    {
        internal TracingAspect(IMessageSink next)
        {
            m_next = next;
        }

        #region Private Vars
        private IMessageSink m_next;
        private String m_typeAndName;
        #endregion // Private Vars

        #region IMessageSink implementation
        public IMessageSink NextSink
        {
            get { return m_next; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            Preprocess(msg);
            IMessage returnMethod = m_next.SyncProcessMessage(msg);
            PostProcess(msg, returnMethod);
            return returnMethod;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }
        #endregion //IMessageSink implementation

        #region Helper methods
        private void Preprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodMessage)) return;

            IMethodMessage call = msg as IMethodMessage;
            Type type = Type.GetType(call.TypeName);
            m_typeAndName = type.Name + "." + call.MethodName;
            Console.Write("PreProcessing: " + m_typeAndName + "(");

            // Loop through the [in] parameters
            for (int i = 0; i < call.ArgCount; ++i)
            {
                if (i > 0) Console.Write(", ");
                Console.Write(call.GetArgName(i) + " = " + call.GetArg(i));
            }
            Console.WriteLine(")");
        }

        private void PostProcess(IMessage msg, IMessage msgReturn)
        {
            // We only want to process method return calls
            if (!(msg is IMethodMessage) ||
                !(msgReturn is IMethodReturnMessage)) return;

            IMethodReturnMessage retMsg = (IMethodReturnMessage)msgReturn;
            Console.Write("PostProcessing: ");
            Exception e = retMsg.Exception;
            if (e != null)
            {
                Console.WriteLine("Exception was thrown: " + e);
                return;
            }

            // Loop through all the [out] parameters
            Console.Write(m_typeAndName + "(");
            if (retMsg.OutArgCount > 0)
            {
                Console.Write("out parameters[");
                for (int i = 0; i < retMsg.OutArgCount; ++i)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write(retMsg.GetOutArgName(i) + " = " +
                                  retMsg.GetOutArg(i));
                }
                Console.Write("]");
            }
            if (retMsg.ReturnValue.GetType() != typeof(void))
                Console.Write(" returned [" + retMsg.ReturnValue + "]");

            Console.WriteLine(")\n");
        }
        #endregion Helpers
    }

    public class TracingProperty : IContextProperty, IContributeObjectSink
    {
        #region IContributeObjectSink implementation
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new TracingAspect(next);
        }
        #endregion // IContributeObjectSink implementation

        #region IContextProperty implementation
        public string Name
        {
            get
            {
                return "CallTracingProperty";
            }
        }
        public void Freeze(Context newContext)
        {
        }
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }
        #endregion //IContextProperty implementation
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Class)]
    public class TracingAttribute : ContextAttribute
    {
        public TracingAttribute() : base("CallTracing") { }
        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new TracingProperty());
        }
    }




    internal class SecurityAspect : IMessageSink
    {
        internal SecurityAspect(IMessageSink next)
        {
            m_next = next;
        }

        #region Private Vars
        private IMessageSink m_next;
        #endregion // Private Vars

        #region IMessageSink implementation
        public IMessageSink NextSink
        {
            get { return m_next; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            Preprocess(msg);
            IMessage returnMethod = m_next.SyncProcessMessage(msg);
            return returnMethod;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }
        #endregion //IMessageSink implementation

        #region Helper methods
        private void Preprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodMessage)) return;

            IMethodMessage call = msg as IMethodMessage;
            Type type = Type.GetType(call.TypeName);
            string callStr = type.Name + "." + call.MethodName;
            Console.WriteLine("Security validating : {0} for {1}", callStr,
                Environment.UserName);
            // call some security validating code
        }

        #endregion Helpers
    }

    public class SecurityProperty : IContextProperty, IContributeObjectSink
    {
        #region IContributeObjectSink implementation
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new SecurityAspect(next);
        }
        #endregion // IContributeObjectSink implementation

        #region IContextProperty implementation
        public string Name
        {
            get { return "SecurityProperty"; }
        }
        public void Freeze(Context newContext)
        {
        }
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }
        #endregion //IContextProperty implementation
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SecurityAttribute : ContextAttribute
    {
        public SecurityAttribute() : base("Security") { }
        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new SecurityProperty());
        }
    }

}