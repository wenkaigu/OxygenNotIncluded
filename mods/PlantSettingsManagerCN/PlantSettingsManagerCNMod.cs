using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using UnityEngine;

namespace Wenkaigu.PlantSettingsManagerCN
{
	/// <summary>
	/// Chinese UI overlay for Plant Settings Manager (Workshop 3733686186).
	/// Patches settings-screen text at runtime; does not change plant balance logic.
	/// </summary>
	public sealed class PlantSettingsManagerCNMod : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
			base.OnLoad(harmony);
			PUtil.InitLibrary();
			SettingsScreenPatches.Apply(harmony);
			Debug.Log("[PlantSettingsManagerCN] Loaded — Chinese UI patch for Plant Settings Manager");
		}
	}
}
