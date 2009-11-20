using System;

namespace InRetail
{
    public static class SystemDateTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static void Reset()
        {
            Now = () => DateTime.Now;
        }
    }
}