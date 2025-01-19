using Microsoft.EntityFrameworkCore;
using ProiectConta.DetailedEntries;
using ProiectConta.DetailedExits;
using ProiectConta.Entries;
using ProiectConta.Exits;
using ProiectConta.Gestions;
using ProiectConta.Partners;
using ProiectConta.Products;
using ProiectConta.Reports;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace ProiectConta.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ProiectContaDbContext :
    AbpDbContext<ProiectContaDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Gestion> Gestions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<Exit> Exits { get; set; }
    public DbSet<DetailedEntry> DetailedEntries { get; set; }
    public DbSet<DetailedExit> DetailedExits { get; set; }
    public DbSet<Report> Reports { get; set; }

    public ProiectContaDbContext(DbContextOptions<ProiectContaDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        
        builder.Entity<Gestion>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "Gestions",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        builder.Entity<Product>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "Products",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.HasIndex(x => x.Name);
        });

        builder.Entity<Partner>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "Partners",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        builder.Entity<Entry>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "Entries",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Date).IsRequired();
        });

        builder.Entity<Exit>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "Exits",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Date).IsRequired();
        });

        builder.Entity<DetailedEntry>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "DetailedEntries",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<DetailedExit>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "DetailedExits",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Report>(
            b =>
        {
            b.ToTable(ProiectContaConsts.DbTablePrefix + "Reports",
                ProiectContaConsts.DbSchema);
            b.ConfigureByConvention();
        });
    }
}
