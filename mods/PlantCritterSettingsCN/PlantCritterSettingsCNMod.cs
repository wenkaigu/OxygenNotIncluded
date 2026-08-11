using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using UnityEngine;

namespace Wenkaigu.PlantCritterSettingsCN
{
	public sealed class PlantCritterSettingsCNMod : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
			base.OnLoad(harmony);
			PUtil.InitLibrary();
			harmony.PatchAll();
			PlantSettingsUiI18n.Apply(harmony);
			CritterSettingsUiI18n.Apply(harmony);
			Debug.Log("[PlantCritterSettingsCN] OnLoad");
		}
	}
}
