using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Exits
{
    public class GetExitListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
