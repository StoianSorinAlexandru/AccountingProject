using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ProiectConta.Entries
{
    public class EntryAlreadyExists : BusinessException
    {
        public EntryAlreadyExists(DateTime date) : base(ProiectContaDomainErrorCodes.EntryAlreadyExists)
        {
            WithData("date", date);
        }
    }
}
