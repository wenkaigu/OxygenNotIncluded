using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace Wenkaigu.PlantCritterSettingsCN
{
	internal static class FoodInfoLocalizationFix
	{
		public static void RefreshAll(string reason)
		{
			try
			{
				var foods = EnumerateFoodInfos();
				int n = 0;
				foreach (var food in foods)
				{
					if (food == null || string.IsNullOrEmpty(food.Id))
						continue;
					var id = food.Id.ToUpperInvariant();
					food.Name = Strings.Get("STRINGS.ITEMS.FOOD." + id + ".NAME");
					food.Description = Strings.Get("STRINGS.ITEMS.FOOD." + id + ".DESC");
					n++;
				}
				EdiblesManager.ClearSaveFoodCache();
				Debug.Log($"[PlantCritterSettingsCN] FoodInfo refresh ({reason}): {n} entries");
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[PlantCritterSettingsCN] FoodInfo refresh failed ({reason}): {ex}");
			}
		}

		private static IEnumerable<EdiblesManager.FoodInfo> EnumerateFoodInfos()
		{
			// Prefer public API when safe; fall back to private static list via reflection.
			try
			{
				var loaded = EdiblesManager.GetAllLoadedFoodTypes();
				if (loaded != null && loaded.Count > 0)
					return loaded;
			}
			catch { /* frontend / timing */ }

			var fallback = new List<EdiblesManager.FoodInfo>();
			var field = typeof(EdiblesManager).GetField("s_allFoodTypes",
				BindingFlags.Static | BindingFlags.NonPublic);
			if (field?.GetValue(null) is IEnumerable list)
			{
				foreach (var item in list)
				{
					if (item is EdiblesManager.FoodInfo info)
						fallback.Add(info);
				}
			}
			return fallback;
		}
	}

	[HarmonyPatch(typeof(Localization), nameof(Localization.Initialize))]
	internal static class Localization_Initialize_FoodInfoFix
	{
		public static void Postfix() => FoodInfoLocalizationFix.RefreshAll("Localization.Initialize");
	}

	[HarmonyPatch(typeof(SaveLoader), nameof(SaveLoader.Load), typeof(string))]
	internal static class SaveLoader_Load_FoodInfoFix
	{
		public static void Postfix() => FoodInfoLocalizationFix.RefreshAll("SaveLoader.Load");
	}
}
