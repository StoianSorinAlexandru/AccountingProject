using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ProiectConta.Products
{
    public class ProductAlreadyExistsException : BusinessException
    {
        public ProductAlreadyExistsException(string name) : base(ProiectContaDomainErrorCodes.ProductAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
