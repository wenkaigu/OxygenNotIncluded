using System;
using HarmonyLib;
using UnityEngine;

namespace Wenkaigu.PlantCritterSettingsCN
{
	/// <summary>
	/// Runtime string replacement for Critter Settings Manager's custom settings UI.
	/// Targets are resolved by name so we do not reference the upstream DLL at compile time.
	/// Mirrors <see cref="PlantSettingsUiI18n"/>; method list filled from decompiled
	/// <c>CritterSettings.SettingsScreen</c> (KMonoBehaviour). ShowRestartNotice is
	/// not present on Critter — it is included in the list but null-skipped.
	/// </summary>
	internal static class CritterSettingsUiI18n
	{
		private static readonly string[] SettingsMethods =
		{
			"BuildUI",
			"BuildFields",
			"PopulateCritterList",
			"SelectCritter",
			"Show",
			"ShowRestartNotice"
		};

		public static void Apply(Harmony harmony)
		{
			var settingsType = AccessTools.TypeByName("CritterSettings.SettingsScreen");
			if (settingsType == null)
			{
				Debug.LogWarning("[PlantCritterSettingsCN] Critter Settings Manager not found — UI i18n skipped");
				return;
			}

			var postfix = new HarmonyMethod(typeof(CritterSettingsUiI18n), nameof(TranslateInstancePostfix));
			foreach (var name in SettingsMethods)
			{
				var method = AccessTools.Method(settingsType, name);
				if (method == null)
					continue;
				harmony.Patch(method, postfix: postfix);
			}

			var modsPatch = AccessTools.TypeByName("CritterSettings.ModsScreen_Patch");
			var buildDisplay = modsPatch != null ? AccessTools.Method(modsPatch, "Postfix") : null;
			if (buildDisplay != null)
				harmony.Patch(buildDisplay, postfix: new HarmonyMethod(typeof(CritterSettingsUiI18n), nameof(TranslateModsScreenPostfix)));

			Debug.Log("[PlantCritterSettingsCN] Critter Settings UI i18n armed");
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
				Debug.LogWarning($"[PlantCritterSettingsCN] Critter UI translate failed: {ex}");
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
				Debug.LogWarning($"[PlantCritterSettingsCN] Critter ModsScreen translate failed: {ex}");
			}
		}
	}
}
