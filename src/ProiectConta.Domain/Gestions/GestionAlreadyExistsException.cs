using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ProiectConta.Gestions
{
    public class GestionAlreadyExistsException : BusinessException
    {
        public GestionAlreadyExistsException(string name)
        {
            WithData("name", name);
        }
    }
}
