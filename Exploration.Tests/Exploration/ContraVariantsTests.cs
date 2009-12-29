using NUnit.Framework;
namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class ContraVariantsTests
    {
        [Test]
        public void Covariants()
        {
            string[] sa=new string[5];
            object[] oa = sa;
            oa[0] = "Hello";
            oa[1] = 5;
        }
    }
}