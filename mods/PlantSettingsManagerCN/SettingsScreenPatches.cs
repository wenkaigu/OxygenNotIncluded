using System;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wenkaigu.PlantSettingsManagerCN
{
	/// <summary>
	/// Runtime string replacement for Plant Settings Manager's custom settings UI.
	/// Targets are resolved by name so we do not reference the upstream DLL at compile time.
	/// </summary>
	internal static class SettingsScreenPatches
	{
		private static readonly string[] SettingsMethods =
		{
			"BuildUI",
			"BuildFields",
			"BuildMutationFields",
			"PopulatePlantList",
			"SelectPlant",
			"Show",
			"ShowRestartNotice"
		};

		public static void Apply(Harmony harmony)
		{
			var settingsType = AccessTools.TypeByName("PlantSettingsManager.SettingsScreen");
			if (settingsType == null)
			{
				Debug.LogWarning("[PlantSettingsManagerCN] PlantSettingsManager.SettingsScreen not found — is Plant Settings Manager enabled and loaded before this mod?");
				return;
			}

			var postfix = new HarmonyMethod(typeof(SettingsScreenPatches), nameof(TranslateInstancePostfix));
			foreach (var methodName in SettingsMethods)
			{
				var method = AccessTools.Method(settingsType, methodName);
				if (method == null)
					continue;
				harmony.Patch(method, postfix: postfix);
				Debug.Log($"[PlantSettingsManagerCN] Patched SettingsScreen.{methodName}");
			}

			// Config button on the Mods list is created in ModsScreen_Patch.BuildDisplay postfix.
			var modsPatch = AccessTools.TypeByName("PlantSettingsManager.ModsScreen_Patch");
			if (modsPatch != null)
			{
				var buildDisplayPostfix = AccessTools.Method(modsPatch, "Postfix");
				if (buildDisplayPostfix != null)
				{
					harmony.Patch(buildDisplayPostfix,
						postfix: new HarmonyMethod(typeof(SettingsScreenPatches), nameof(TranslateModsScreenPostfix)));
					Debug.Log("[PlantSettingsManagerCN] Patched ModsScreen_Patch.Postfix");
				}
			}
		}

		private static void TranslateInstancePostfix(object __instance)
		{
			try
			{
				if (__instance is Component component && component != null)
					UiTextTranslator.TranslateHierarchy(component.transform);
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[PlantSettingsManagerCN] Translate settings UI failed: {ex}");
			}
		}

		private static void TranslateModsScreenPostfix()
		{
			try
			{
				var modsScreen = UnityEngine.Object.FindObjectOfType<ModsScreen>();
				if (modsScreen != null)
					UiTextTranslator.TranslateHierarchy(modsScreen.transform);
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[PlantSettingsManagerCN] Translate ModsScreen failed: {ex}");
			}
		}
	}

	internal static class UiTextTranslator
	{
		public static void TranslateHierarchy(Transform root)
		{
			if (root == null)
				return;

			foreach (var loc in root.GetComponentsInChildren<LocText>(true))
			{
				if (loc == null)
					continue;
				var next = ZhStrings.Translate(loc.text);
				if (!string.Equals(next, loc.text, StringComparison.Ordinal))
					loc.text = next;
			}

			foreach (var tmp in root.GetComponentsInChildren<TMP_Text>(true))
			{
				if (tmp == null || tmp is LocText)
					continue;
				var next = ZhStrings.Translate(tmp.text);
				if (!string.Equals(next, tmp.text, StringComparison.Ordinal))
					tmp.text = next;
			}

			foreach (var uiText in root.GetComponentsInChildren<Text>(true))
			{
				if (uiText == null)
					continue;
				var next = ZhStrings.Translate(uiText.text);
				if (!string.Equals(next, uiText.text, StringComparison.Ordinal))
					uiText.text = next;
			}
		}
	}
}
