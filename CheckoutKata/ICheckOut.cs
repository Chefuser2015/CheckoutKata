using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public interface ICheckOut
    {
         int GetTotalPrice();
         ICheckOut Scan(String scan);
    }
}
