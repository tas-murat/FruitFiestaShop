using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Exceptions
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(object key) : base($"{key} idisi ile herhangi bir ürün bulunamadı.")
        {

        }
    }
}
