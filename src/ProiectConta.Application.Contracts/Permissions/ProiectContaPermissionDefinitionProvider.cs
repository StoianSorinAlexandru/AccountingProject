using ProiectConta.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ProiectConta.Permissions;

public class ProiectContaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProiectContaPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ProiectContaPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProiectContaResource>(name);
    }
}
