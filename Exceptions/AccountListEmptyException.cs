using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAppWithList.Exceptions
{
    internal class AccountListEmptyException:Exception
    {
        public AccountListEmptyException(string message):base(message)
        {
            
        }
    }
}
