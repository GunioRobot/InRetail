using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;

namespace InRetail.Services
{
    public class ServicesRegister : Registry
    {
        public ServicesRegister()
        {
            //ForRequestedType<IReceiveMoneyTransfers>().TheDefault.Is.OfConcreteType<MoneyReceiveService>();
            //ForRequestedType<ISendMoneyTransfer>().TheDefault.Is.OfConcreteType<MoneyTransferService>();
        }
    }
}
