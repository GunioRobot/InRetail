using System;
using Moq;

namespace Tests.InRetail.Procurement
{
    public static class Moq
    {
        public static T Mock<T>() where T:class 
        {
            T mock = new Mock<T>().Object;
            return mock;
        }

        public static T Stub<T>() where T : class
        {
            T mock = new Mock<T>().Object;
            return mock;
        }
    }
}