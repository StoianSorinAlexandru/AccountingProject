using System;
using System.Threading.Tasks;
using ProiectConta.DetailedEntries;
using ProiectConta.DetailedExits;
using ProiectConta.Entries;
using ProiectConta.Exits;
using ProiectConta.Gestions;
using ProiectConta.Partners;
using ProiectConta.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProiectConta
{
    public class ProiectContaSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Gestion, Guid> _gestionRepository;
        private readonly IRepository<Partner, Guid> _partnerRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Entry, Guid> _entryRepository;
        private readonly IRepository<Exit, Guid> _exitRepository;
        private readonly IRepository<DetailedEntry, Guid> _detailedEntryRepository;
        private readonly IRepository<DetailedExit, Guid> _detailedExitRepository;
        private readonly ProductManager _productManager;
        private readonly PartnerManager _partnerManager;
        private readonly GestionManager _gestionManager;

        public ProiectContaSeedContributor(
            IRepository<Gestion, Guid> gestionRepository,
            IRepository<Partner, Guid> partnerRepository,
            IRepository<Product, Guid> productRepository,
            IRepository<Entry, Guid> entryRepository,
            IRepository<Exit, Guid> exitRepository,
            IRepository<DetailedEntry, Guid> detailedEntryRepository,
            IRepository<DetailedExit, Guid> detailedExitRepository,
            ProductManager productManager,
            PartnerManager partnerManager,
            GestionManager gestionManager
        )
        {
            _gestionRepository = gestionRepository;
            _partnerRepository = partnerRepository;
            _productRepository = productRepository;
            _entryRepository = entryRepository;
            _exitRepository = exitRepository;
            _detailedEntryRepository = detailedEntryRepository;
            _detailedExitRepository = detailedExitRepository;
            _productManager = productManager;
            _partnerManager = partnerManager;
            _gestionManager = gestionManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            //Seed Products
            if (await _productRepository.GetCountAsync() <= 0)
            {
                await _productRepository.InsertAsync(
                    await _productManager.CreateAsync("Product A", 10.5f)
                    );

                await _productRepository.InsertAsync(
                    await _productManager.CreateAsync("Product B", 15.75f)
                    );

                await _productRepository.InsertAsync(
                    await _productManager.CreateAsync("Product C", 7.30f)
                    );
            }

            // Seed Partners
            if (await _partnerRepository.GetCountAsync() <= 0)
            {

                await _partnerRepository.InsertAsync(
                    await _partnerManager.CreateAsync("Partner A", "123456", "123 Street A", PartnerType.Client)
                    );

                await _partnerRepository.InsertAsync(
                    await _partnerManager.CreateAsync("Partner B", "654321", "456 Street B", PartnerType.Client)
                    );

            }

            // Seed Gestions
            if (await _gestionRepository.GetCountAsync() <= 0)
            {
                //await _gestionRepository.InsertAsync(
                //    new Gestion { Name = "Gestion A" },
                //    autoSave: true
                //);

                //await _gestionRepository.InsertAsync(
                //    new Gestion { Name = "Gestion B" },
                //    autoSave: true
                //);
                await _gestionRepository.InsertAsync(
                    await _gestionManager.CreateAsync("Gestion A")
                    );

                await _gestionRepository.InsertAsync(
                    await _gestionManager.CreateAsync("Gestion B")
                    );
            }

            // Seed Entries
            //if (await _entryRepository.GetCountAsync() <= 0)
            //{
            //    var partnerA = await _partnerRepository.FirstOrDefaultAsync(p => p.Name == "Partner A");
            //    var partnerB = await _partnerRepository.FirstOrDefaultAsync(p => p.Name == "Partner B");
            //    var gestionA = await _gestionRepository.FirstOrDefaultAsync(g => g.Name == "Gestion A");
            //    var gestionB = await _gestionRepository.FirstOrDefaultAsync(g => g.Name == "Gestion B");

            //    await _entryRepository.InsertAsync(
            //        new Entry
            //        {
            //            Date = DateTime.Parse("2023-01-01"),
            //            PartnerId = partnerA.Id,
            //            GestionId = gestionA.Id
            //        },
            //        autoSave: true
            //    );

            //    await _entryRepository.InsertAsync(
            //        new Entry
            //        {
            //            Date = DateTime.Parse("2023-02-01"),
            //            PartnerId = partnerB.Id,
            //            GestionId = gestionB.Id
            //        },
            //        autoSave: true
            //    );
            //}

            // Seed Detailed Entries
            //if (await _detailedEntryRepository.GetCountAsync() <= 0)
            //{
            //    var entryA = await _entryRepository.FirstOrDefaultAsync(e => e.Date == DateTime.Parse("2023-01-01"));
            //    var entryB = await _entryRepository.FirstOrDefaultAsync(e => e.Date == DateTime.Parse("2023-02-01"));
            //    var productA = await _productRepository.FirstOrDefaultAsync(p => p.Name == "Product A");
            //    var productB = await _productRepository.FirstOrDefaultAsync(p => p.Name == "Product B");
            //    var productC = await _productRepository.FirstOrDefaultAsync(p => p.Name == "Product C");

            //    await _detailedEntryRepository.InsertAsync(
            //        new DetailedEntry { EntryId = entryA.Id, ProductId = productA.Id, Quantity = 10 },
            //        autoSave: true
            //    );

            //    await _detailedEntryRepository.InsertAsync(
            //        new DetailedEntry { EntryId = entryA.Id, ProductId = productB.Id, Quantity = 5 },
            //        autoSave: true
            //    );

            //    await _detailedEntryRepository.InsertAsync(
            //        new DetailedEntry { EntryId = entryB.Id, ProductId = productC.Id, Quantity = 7 },
            //        autoSave: true
            //    );
            //}

            // Seed Exits
            //if (await _exitRepository.GetCountAsync() <= 0)
            //{
            //    var gestionA = await _gestionRepository.FirstOrDefaultAsync(g => g.Name == "Gestion A");
            //    var gestionB = await _gestionRepository.FirstOrDefaultAsync(g => g.Name == "Gestion B");
            //    var partnerA = await _partnerRepository.FirstOrDefaultAsync(p => p.Name == "Partner A");
            //    var partnerB = await _partnerRepository.FirstOrDefaultAsync(p => p.Name == "Partner B");

            //    await _exitRepository.InsertAsync(
            //        new Exit
            //        {
            //            Date = DateTime.Parse("2023-03-01"),
            //            GestionId = gestionA.Id,
            //            PartnerId = partnerA.Id // Set the PartnerId
            //        },
            //        autoSave: true
            //    );

            //    await _exitRepository.InsertAsync(
            //        new Exit
            //        {
            //            Date = DateTime.Parse("2023-04-01"),
            //            GestionId = gestionB.Id,
            //            PartnerId = partnerB.Id // Set the PartnerId
            //        },
            //        autoSave: true
            //    );
            //}


            // Seed Detailed Exits
            //if (await _detailedExitRepository.GetCountAsync() <= 0)
            //{
            //    var exitA = await _exitRepository.FirstOrDefaultAsync(e => e.Date == DateTime.Parse("2023-03-01"));
            //    var exitB = await _exitRepository.FirstOrDefaultAsync(e => e.Date == DateTime.Parse("2023-04-01"));
            //    var productA = await _productRepository.FirstOrDefaultAsync(p => p.Name == "Product A");
            //    var productC = await _productRepository.FirstOrDefaultAsync(p => p.Name == "Product C");

            //    await _detailedExitRepository.InsertAsync(
            //        new DetailedExit { ExitId = exitA.Id, ProductId = productA.Id, Quantity = 10 },
            //        autoSave: true
            //    );

            //    await _detailedExitRepository.InsertAsync(
            //        new DetailedExit { ExitId = exitB.Id, ProductId = productC.Id, Quantity = 4 },
            //        autoSave: true
            //    );
            //}
        }
    }
}
