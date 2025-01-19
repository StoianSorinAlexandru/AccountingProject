using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.Gestions
{
    public class GestionManager : DomainService
    {
        private readonly IGestionRepository _gestionRepository;

        public GestionManager(IGestionRepository gestionRepository)
        {
            _gestionRepository = gestionRepository;
        }

        public async Task<Gestion> CreateAsync(
            string name)
        {
            var existingGestion = await _gestionRepository.FindByNameAsync(name);
            if (existingGestion != null)
            {
                throw new GestionAlreadyExistsException(name);
            }
            return new Gestion(
                GuidGenerator.Create(),
                name
            );     
        }

        public async Task ChangeNameAsync(Gestion gestion, string name)
        {
            var existingGestion = await _gestionRepository.FindByNameAsync(name);
            if (existingGestion != null && existingGestion.Id != gestion.Id)
            {
                throw new GestionAlreadyExistsException(name);
            }
            gestion.ChangeName(name);
        }
    }
}
