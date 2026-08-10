using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using UnityEngine;

namespace Wenkaigu.HelloWorld
{
	/// <summary>
	/// Minimal Dev-folder smoke test: loads, inits PLib, applies attribute patches.
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
