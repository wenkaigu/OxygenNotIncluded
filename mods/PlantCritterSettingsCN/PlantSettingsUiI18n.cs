using System;
using HarmonyLib;
using UnityEngine;

namespace Wenkaigu.PlantCritterSettingsCN
{
	/// <summary>
	/// Runtime string replacement for Plant Settings Manager's custom settings UI.
	/// Targets are resolved by name so we do not reference the upstream DLL at compile time.
	/// </summary>
	internal static class PlantSettingsUiI18n
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
				Debug.LogWarning("[PlantCritterSettingsCN] Plant Settings Manager not found — UI i18n skipped");
				return;
			}

			var postfix = new HarmonyMethod(typeof(PlantSettingsUiI18n), nameof(TranslateInstancePostfix));
			foreach (var name in SettingsMethods)
			{
				var method = AccessTools.Method(settingsType, name);
				if (method == null)
					continue;
				harmony.Patch(method, postfix: postfix);
			}

			var modsPatch = AccessTools.TypeByName("PlantSettingsManager.ModsScreen_Patch");
			var buildDisplay = modsPatch != null ? AccessTools.Method(modsPatch, "Postfix") : null;
			if (buildDisplay != null)
				harmony.Patch(buildDisplay, postfix: new HarmonyMethod(typeof(PlantSettingsUiI18n), nameof(TranslateModsScreenPostfix)));

			Debug.Log("[PlantCritterSettingsCN] Plant Settings UI i18n armed");
		}

		private static void TranslateInstancePostfix(object __instance)
		{
			try
			{
				if (__instance is Component c && c != null)
					UiTextTranslator.TranslateHierarchy(c.transform);
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[PlantCritterSettingsCN] Plant UI translate failed: {ex}");
			}
		}

		private static void TranslateModsScreenPostfix()
		{
			try
			{
				var screen = UnityEngine.Object.FindObjectOfType<ModsScreen>();
				if (screen != null)
					UiTextTranslator.TranslateHierarchy(screen.transform);
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[PlantCritterSettingsCN] ModsScreen translate failed: {ex}");
			}
		}
	}
}
