using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.Domain
{
    internal interface IAccountRepository
    {
        void Withdraw(decimal amount, string owner);
        void Deposit(decimal amount, string owner);
    }
}
