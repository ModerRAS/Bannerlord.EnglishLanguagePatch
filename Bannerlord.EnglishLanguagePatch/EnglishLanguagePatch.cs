using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaleWorlds.Localization;

namespace Bannerlord.EnglishLanguagePatch {
    [HarmonyPatch(typeof(MBTextManager), "GetLocalizedText", new Type[] { typeof(string)})]
    public class EnglishLanguagePatch {
        private static string RemoveComments(string localizedText) {
            string text = "{%.+?}";
            foreach (object obj in Regex.Matches(localizedText, text)) {
                Match match = (Match)obj;
                localizedText = localizedText.Replace(match.Value, "");
            }
            return localizedText;
        }
        static bool Prefix(ref string __result, string text) {
            if (text != null && text.Length > 2 && text[0] == '{' && text[1] == '=') {
                var i = 2;
                for (; i < text.Length; i++) {
                    if (text[i] != '}') {
                        continue;
                    } else {
                        break;
                    }
                }
                var tmp = text.Substring(2, i - 2);
                var text2 = LocalizedTextManager.GetTranslatedText(MBTextManager.ActiveLanguage, tmp);
                if (string.IsNullOrEmpty(text2)) {
                    text2 = text.Substring(i + 1);
                }
                __result = RemoveComments(text2);
            }
            return false;
        }
        
    }
}
