using Volo.Abp.Settings;

namespace ProiectConta.Settings;

public class ProiectContaSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ProiectContaSettings.MySetting1));
    }
}
