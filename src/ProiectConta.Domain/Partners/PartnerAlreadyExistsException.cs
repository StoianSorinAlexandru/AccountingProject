using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ProiectConta.Partners
{
    public class PartnerAlreadyExistsException : BusinessException
    {
        public PartnerAlreadyExistsException(string name) : base(ProiectContaDomainErrorCodes.ProductAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
