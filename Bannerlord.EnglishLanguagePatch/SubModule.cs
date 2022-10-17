using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.EnglishLanguagePatch {
    public class SubModule : MBSubModuleBase {
        protected override void OnSubModuleLoad() {
            base.OnSubModuleLoad();
            var harmony = new Harmony("Bannerlord.EnglishLanguagePatch");
            harmony.PatchAll();

        }

        protected override void OnSubModuleUnloaded() {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot() {
            base.OnBeforeInitialModuleScreenSetAsRoot();

        }
    }
}