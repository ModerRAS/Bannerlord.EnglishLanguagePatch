using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Bannerlord.EnglishLanguagePatch {
    [HarmonyPatch]
    public class LoaderPatch {
        static MethodBase TargetMethod() {
            return AccessTools.FirstMethod(typeof(LocalizedTextManager), method => method.Name.Contains("LoadLanguage") && method.IsPrivate);

        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            var code = new List<CodeInstruction>(instructions);
            int insertionIndex = -1;
            for (int i = 0; i < code.Count - 1; i++) {
                if (code[i].opcode == OpCodes.Ldstr && (string)code[i].operand == "English") {
                    insertionIndex = i;
                    code[i].operand = string.Empty;
                    break;
                }
            }


            return code;
        }


    }
}
