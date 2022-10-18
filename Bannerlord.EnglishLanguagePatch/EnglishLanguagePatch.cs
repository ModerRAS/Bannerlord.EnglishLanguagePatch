using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TaleWorlds.Localization;

namespace Bannerlord.EnglishLanguagePatch {
    [HarmonyPatch(typeof(MBTextManager), "GetLocalizedText", new Type[] { typeof(string)})]
    public class EnglishLanguagePatch {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            var code = new List<CodeInstruction>(instructions);
            int insertionIndex = -1;
            for (int i = 0; i < code.Count - 1; i++) {
                if (code[i].opcode == OpCodes.Ldstr && (string)code[i].operand == "English") {
                    insertionIndex = i;
                    code[i].operand = string.Empty;
                }
            }


            return code;
        }
    }
}
