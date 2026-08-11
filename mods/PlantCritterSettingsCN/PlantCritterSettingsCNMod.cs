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
			Debug.Log("[PlantCritterSettingsCN] OnLoad");
		}
	}
}
