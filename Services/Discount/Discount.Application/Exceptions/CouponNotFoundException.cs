using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Exceptions
{
    public class CouponNotFoundException : ApplicationException
    {
        public CouponNotFoundException(object key) : base($"{key} idisi ile herhangi bir kupon bulunamadı.")
        {

        }
    }
}
