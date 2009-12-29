using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using InRetail.UiCore.Extensions;
using NUnit.Framework;




namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class SerializationFixture
    {
        [Test]
        public void EqualityTest()
        {
            var money1 = new TestMoneyType(10, TestCurrencyEnum.Gel);
            var money2 = new TestMoneyType(10, TestCurrencyEnum.Gel);
            Assert.AreEqual(money1,money2);

            var command1 = new TestCommand(money1, "Georgial lari");
            var command2 = new TestCommand(money2, "Georgial lari");
            Assert.AreEqual(command1, command2);
        }

        [Test]
        public void SerializeDeserializeTest()
        {
            var money1 = new TestMoneyType(10, TestCurrencyEnum.Gel);
            var command1 = new TestCommand(money1, "Georgial lari");


            var formatter = new SoapFormatter();

            Stream strem = new MemoryStream();
            
            formatter.Serialize(strem, command1);
            strem.Seek(0, SeekOrigin.Begin);
            byte[] buf = new byte[strem.Length];
            var s = strem.Read(buf, 0, (int)strem.Length);

            var command2 = formatter.Deserialize(strem).As<TestCommand>();
            
            strem.Close();

            Assert.AreEqual(command1, command2);
        }
    }

    [Serializable]
    public class TestCommand
    {
        private readonly TestMoneyType _money;
        private readonly string _name;

        public TestCommand(TestMoneyType money, string name)
        {
            _money = money;
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
        public TestMoneyType Money
        {
            get { return _money; }
        }

        public bool Equals(TestCommand other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._money, _money) && Equals(other._name, _name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TestCommand)) return false;
            return Equals((TestCommand) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_money.GetHashCode()*397) ^ _name.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("Money: {0}, Name: {1}", _money, _name);
        }
    }

    [Serializable]
    public class TestMoneyType
    {
        private readonly decimal _amount;
        private readonly TestCurrencyEnum _currency;

        public TestMoneyType(decimal amount, TestCurrencyEnum currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public decimal Amount
        {
            get { return _amount; }
        }
        public TestCurrencyEnum Currency
        {
            get { return _currency; }
        }

        public bool Equals(TestMoneyType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._amount == _amount && Equals(other._currency, _currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TestMoneyType)) return false;
            return Equals((TestMoneyType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_amount.GetHashCode()*397) ^ _currency.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("Currency: {0}, Amount: {1}", _currency, _amount);
        }
    }

    public enum TestCurrencyEnum
    {
        Gel
    }
}