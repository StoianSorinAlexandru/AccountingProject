using System.Threading.Tasks;
using ProiectConta.Localization;
using ProiectConta.MultiTenancy;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace ProiectConta.Blazor.Menus;

public class ProiectContaMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<ProiectContaResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ProiectContaMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "ProiectConta",
                l["Menu:ProiectConta"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "ProiectConta.Products",
                    l["Menu:Products"],
                    url: "/products"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "ProiectConta.Partners",
                    l["Menu:Partners"],
                    url: "/partners"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "ProiectConta.Gestions",
                    l["Menu:Gestions"],
                    url: "/gestions"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "ProiectConta.Entries",
                    l["Menu:Entries"],
                    url: "/entries"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "ProiectConta.Exits",
                    l["Menu:Exits"],
                    url: "/exits"
                )
            )
        );

        return Task.CompletedTask;
    }
}
