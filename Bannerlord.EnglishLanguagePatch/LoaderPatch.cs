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
            //var type = AccessTools.FirstInner(typeof(LocalizedTextManager), t => t.Name.Contains("LocalizedTextManager"));
            return AccessTools.FirstMethod(typeof(LocalizedTextManager), method => method.Name.Contains("LoadLanguage") && method.IsPrivate);
            //var type = new Type[] { AccessTools.TypeByName("TaleWorlds.Localization.LanguageData") };
            //return typeof(LocalizedTextManager)
            //    .GetMethod("LoadLanguage",
            //               BindingFlags.NonPublic | BindingFlags.Static)
            //    .MakeGenericMethod(type);
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
        //static bool Prefix(object language) {
        //    MBTextManager.ResetFunctions();
            
        //    string stringId = (string)Traverse.Create(language).Property("StringId").GetValue();
        //    bool flag = true;
        //    foreach (string text in (IReadOnlyList<string>)Traverse.Create(language).Property("XmlPaths").GetValue()) {
        //        XmlDocument xmlDocument = Traverse.Create(typeof(LocalizedTextManager)).Method("LoadXmlFile", text).GetValue<XmlDocument>();
        //        if (xmlDocument != null) {
        //            for (XmlNode xmlNode = xmlDocument.ChildNodes[1].FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling) {
        //                if (xmlNode.Name == "strings" && xmlNode.HasChildNodes) {
        //                    if (flag) {
        //                        for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling) {
        //                            if (xmlNode2.Name == "string" && xmlNode2.NodeType != XmlNodeType.Comment) {
        //                                Traverse.Create(typeof(LocalizedTextManager)).Method("DeserializeStrings", new object[] { xmlNode2, stringId }).GetValue();
        //                            }
        //                        }
        //                    }
        //                } else if (xmlNode.Name == "functions" && xmlNode.HasChildNodes) {
        //                    for (XmlNode xmlNode3 = xmlNode.FirstChild; xmlNode3 != null; xmlNode3 = xmlNode3.NextSibling) {
        //                        if (xmlNode3.Name == "function" && xmlNode3.NodeType != XmlNodeType.Comment) {
        //                            string value = xmlNode3.Attributes["functionName"].Value;
        //                            string value2 = xmlNode3.Attributes["functionBody"].Value;
        //                            MBTextManager.SetFunction(value, value2);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    Debug.Print("Loading localized text xml.", 0, Debug.DebugColor.White, 17592186044416UL);
        //    return false;
        //}


    }
}
