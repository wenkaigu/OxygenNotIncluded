using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using UnityEngine;

namespace Wenkaigu.HelloWorld
{
	/// <summary>
	/// Canonical project template / build smoke test. Not deployed to mods/Dev by default
	/// (<c>DeployToOniDev=false</c>) so it does not appear in the in-game Mods list.
	/// </summary>
	public sealed class HelloWorldMod : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
			base.OnLoad(harmony);
			PUtil.InitLibrary();
			harmony.PatchAll();
			Debug.Log("[HelloWorld] Loaded (wenkaigu.HelloWorld)");
		}
	}
}
