# PlantCritterSettingsCN Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Create a new Dev-deployable mod `PlantCritterSettingsCN` that (1) always refreshes `FoodInfo` name/description strings after localization, and (2) soft-patches Plant/Critter Settings Manager settings UIs into Simplified Chinese including list display names.

**Architecture:** New folder `mods/PlantCritterSettingsCN/` scaffolded from `mods/HelloWorld/`. Three soft modules wired from `UserMod2.OnLoad`: attribute Harmony patches for game `Localization`/`SaveLoader`; manual Harmony patches for upstream settings screens resolved by type name. Old `mods/PlantSettingsManagerCN/` stays archive-only.

**Tech Stack:** C# net48, Harmony 2 (`HarmonyLib`), PLib (ILRepacked), KMod `UserMod2`, ONI Managed refs via `Directory.Build.props.user`.

## Global Constraints

- New project path must be `mods/PlantCritterSettingsCN/` (do not rename/move the archive folder).
- Leave `mods/PlantSettingsManagerCN/` unpublished (`DeployToOniDev=false`); may copy string maps from it.
- No compile-time references to upstream Workshop DLLs.
- `mod_info.yaml` must set `APIVersion: 2`.
- Never throw from Harmony patches; log warnings only.
- FoodInfo fix always runs; Plant/Critter i18n skip if upstream types missing.
- Load order: this mod after Plant Settings Manager and Critter Settings Manager (document in `mod.yaml`).
- Claim done only after successful `dotnet build` and Dev deploy of DLL+yaml.
- Branch: `feat/plant-critter-settings-cn-food-fix`.

## File structure

| File | Responsibility |
|---|---|
| `mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj` | net48 project, PLib ILRepack, **DeployToOniDev enabled** |
| `mods/PlantCritterSettingsCN/mod.yaml` | title/description/staticID + load-order note |
| `mods/PlantCritterSettingsCN/mod_info.yaml` | APIVersion 2 metadata |
| `mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs` | `UserMod2` entry; init modules |
| `mods/PlantCritterSettingsCN/FoodInfoLocalizationFix.cs` | Refresh `FoodInfo` after loc / save load |
| `mods/PlantCritterSettingsCN/UiTextTranslator.cs` | Shared UI tree walk + `ZhStrings.Translate` |
| `mods/PlantCritterSettingsCN/ZhStrings.cs` | Exact/partial EN→ZH maps (chrome + plants + critters) |
| `mods/PlantCritterSettingsCN/PlantSettingsUiI18n.cs` | Soft-patch Plant Settings Manager UI |
| `mods/PlantCritterSettingsCN/CritterSettingsUiI18n.cs` | Soft-patch Critter Settings Manager UI |
| `OxygenNotIncluded.sln` | Register new project under `mods` solution folder |

Spec: `docs/superpowers/specs/2026-08-11-plant-critter-settings-cn-food-fix-design.md`

---

### Task 1: Scaffold `mods/PlantCritterSettingsCN/`

**Files:**
- Create: `mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj`
- Create: `mods/PlantCritterSettingsCN/mod.yaml`
- Create: `mods/PlantCritterSettingsCN/mod_info.yaml`
- Create: `mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs`
- Modify: `OxygenNotIncluded.sln`

**Interfaces:**
- Consumes: HelloWorld template patterns
- Produces: buildable project `Wenkaigu.PlantCritterSettingsCN`, staticID `wenkaigu.PlantCritterSettingsCN`

- [ ] **Step 1: Copy HelloWorld template into the new folder**

```bash
cd /Users/guwenkai/Projects/AICodingProjects/GameMods/OxygenNotIncluded
cp -R mods/HelloWorld mods/PlantCritterSettingsCN
cd mods/PlantCritterSettingsCN
rm -rf bin obj
mv HelloWorld.csproj PlantCritterSettingsCN.csproj
mv HelloWorldMod.cs PlantCritterSettingsCNMod.cs
```

- [ ] **Step 2: Rename project identity**

In `PlantCritterSettingsCN.csproj` set:

```xml
<RootNamespace>Wenkaigu.PlantCritterSettingsCN</RootNamespace>
<AssemblyName>PlantCritterSettingsCN</AssemblyName>
<ModName>PlantCritterSettingsCN</ModName>
<!-- Deploy for real Dev testing (unlike HelloWorld template) -->
<!-- Remove DeployToOniDev=false OR set true -->
```

Also add Unity UI / TextMeshPro refs needed later (same as archived Plant CN):

```xml
<Reference Include="Unity.TextMeshPro">
  <HintPath>$(GameManagedDir)/Unity.TextMeshPro.dll</HintPath>
  <Private>false</Private>
</Reference>
<Reference Include="UnityEngine.UI">
  <HintPath>$(GameManagedDir)/UnityEngine.UI.dll</HintPath>
  <Private>false</Private>
</Reference>
<Reference Include="UnityEngine.UIModule">
  <HintPath>$(GameManagedDir)/UnityEngine.UIModule.dll</HintPath>
  <Private>false</Private>
</Reference>
```

Ensure deploy target is active (`DeployToOniDev` not false).

- [ ] **Step 3: Write yaml + minimal entry**

`mod.yaml`:

```yaml
title: "Plant & Critter Settings 汉化+食物名修复"
description: "汉化 Plant/Critter Settings Manager 设置界面（含列表名），并修复饮食管理/左上角食物英文。请放在两个上游 mod 之后。"
staticID: "wenkaigu.PlantCritterSettingsCN"
```

`mod_info.yaml`:

```yaml
supportedContent: ALL
minimumSupportedBuild: 744825
version: 1.0.0
APIVersion: 2
```

`PlantCritterSettingsCNMod.cs`:

```csharp
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
			// Modules registered in later tasks
			Debug.Log("[PlantCritterSettingsCN] OnLoad");
		}
	}
}
```

- [ ] **Step 4: Add project to `OxygenNotIncluded.sln`**

Add a `Project(...)` entry for `mods\PlantCritterSettingsCN\PlantCritterSettingsCN.csproj` with a new GUID, nest under existing `mods` solution folder (same pattern as PlantSettingsManagerCN). Include Debug|Any CPU / Release|Any CPU build entries.

- [ ] **Step 5: Build and verify Dev deploy**

```bash
dotnet build mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj -c Debug
ls -la "$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Dev/PlantCritterSettingsCN/"
```

Expected: build succeeded; folder contains `PlantCritterSettingsCN.dll`, `mod.yaml`, `mod_info.yaml`.

- [ ] **Step 6: Commit**

```bash
git add mods/PlantCritterSettingsCN OxygenNotIncluded.sln
git commit -m "Scaffold PlantCritterSettingsCN mod project from HelloWorld."
```

---

### Task 2: FoodInfo localization refresh

**Files:**
- Create: `mods/PlantCritterSettingsCN/FoodInfoLocalizationFix.cs`
- Modify: `mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs`

**Interfaces:**
- Consumes: game `Localization`, `SaveLoader`, `EdiblesManager.FoodInfo`, `Strings`
- Produces: `FoodInfoLocalizationFix.Apply(Harmony)` and/or `[HarmonyPatch]` classes discovered by `PatchAll`

- [ ] **Step 1: Implement refresh helper + patches**

Create `FoodInfoLocalizationFix.cs`:

```csharp
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

			var field = typeof(EdiblesManager).GetField("s_allFoodTypes",
				BindingFlags.Static | BindingFlags.NonPublic);
			if (field?.GetValue(null) is IEnumerable list)
			{
				foreach (var item in list)
				{
					if (item is EdiblesManager.FoodInfo info)
						yield return info;
				}
				yield break;
			}
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
```

Notes for implementer:
- If `Strings.Get` returns `StringEntry`, assign with implicit `string` conversion or `.String` so the **current** translation is stored.
- If `SaveLoader.Load` overload differs, patch the overload used by the game (string path) — verify with Harmony or decompile if build fails.
- `base.OnLoad(harmony)` / `PatchAll` must run so attribute patches apply.

- [ ] **Step 2: Ensure `OnLoad` calls `harmony.PatchAll()`**

In `PlantCritterSettingsCNMod.OnLoad`, keep `base.OnLoad(harmony)` (UserMod2 PatchAll) **or** explicit `harmony.PatchAll(typeof(PlantCritterSettingsCNMod).Assembly);` if base does not patch. Match HelloWorld behavior.

- [ ] **Step 3: Build**

```bash
dotnet build mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj -c Debug
```

Expected: success; deployed DLL updated.

- [ ] **Step 4: Commit**

```bash
git add mods/PlantCritterSettingsCN/FoodInfoLocalizationFix.cs mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs
git commit -m "Refresh FoodInfo strings after localization and save load."
```

---

### Task 3: Shared UI translator + ZhStrings

**Files:**
- Create: `mods/PlantCritterSettingsCN/UiTextTranslator.cs`
- Create: `mods/PlantCritterSettingsCN/ZhStrings.cs`

**Interfaces:**
- Consumes: Unity UI / TMP types
- Produces: `ZhStrings.Translate(string)` ; `UiTextTranslator.TranslateHierarchy(Transform)`

- [ ] **Step 1: Port and extend string maps**

Copy archived `mods/PlantSettingsManagerCN/ZhStrings.cs` into the new project namespace `Wenkaigu.PlantCritterSettingsCN`.

Add plant display names used by upstream `PlantDisplayNames` (examples — complete from decompiled upstream list /官中):

```csharp
{ "Mealwood", "米虱木" },
{ "Bristle Blossom", "毛刺花" },
{ "Dusk Cap", "夜幕菇" },
{ "Sleet Wheat", "冰霜小麦" },
// ... all keys from Plant Settings PlantDisplayNames values + ids as needed
```

Add critter display names from Critter Settings `DisplayNames` values (examples):

```csharp
{ "Hatch", "哈奇" },
{ "Sage Hatch", "石壳哈奇" },
// ... full CritterSettings DisplayNames set
```

Also add Critter settings chrome strings discovered while implementing Task 5 (buttons/labels); stub empty Critter chrome section with a comment if unknown until Task 5 inspection.

Keep Exact-first, then longest-first partial replace (same algorithm as archive).

- [ ] **Step 2: Implement `UiTextTranslator`**

```csharp
internal static class UiTextTranslator
{
	public static void TranslateHierarchy(Transform root)
	{
		if (root == null) return;
		foreach (var loc in root.GetComponentsInChildren<LocText>(true))
		{
			if (loc == null) continue;
			var next = ZhStrings.Translate(((TMP_Text)loc).text);
			if (!string.Equals(next, ((TMP_Text)loc).text, StringComparison.Ordinal))
				((TMP_Text)loc).text = next;
		}
		foreach (var tmp in root.GetComponentsInChildren<TMP_Text>(true))
		{
			if (tmp == null || tmp is LocText) continue;
			var next = ZhStrings.Translate(tmp.text);
			if (!string.Equals(next, tmp.text, StringComparison.Ordinal))
				tmp.text = next;
		}
		foreach (var ui in root.GetComponentsInChildren<UnityEngine.UI.Text>(true))
		{
			if (ui == null) continue;
			var next = ZhStrings.Translate(ui.text);
			if (!string.Equals(next, ui.text, StringComparison.Ordinal))
				ui.text = next;
		}
	}
}
```

- [ ] **Step 3: Build**

```bash
dotnet build mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj -c Debug
```

Expected: success.

- [ ] **Step 4: Commit**

```bash
git add mods/PlantCritterSettingsCN/ZhStrings.cs mods/PlantCritterSettingsCN/UiTextTranslator.cs
git commit -m "Add Chinese string maps and shared UI text translator."
```

---

### Task 4: Plant Settings Manager UI i18n

**Files:**
- Create: `mods/PlantCritterSettingsCN/PlantSettingsUiI18n.cs`
- Modify: `mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs`

**Interfaces:**
- Consumes: `UiTextTranslator`, upstream type names `PlantSettingsManager.SettingsScreen`, `PlantSettingsManager.ModsScreen_Patch`
- Produces: `PlantSettingsUiI18n.Apply(Harmony)`

- [ ] **Step 1: Implement soft patches (from archived SettingsScreenPatches)**

```csharp
internal static class PlantSettingsUiI18n
{
	private static readonly string[] SettingsMethods =
	{
		"BuildUI", "BuildFields", "BuildMutationFields",
		"PopulatePlantList", "SelectPlant", "Show", "ShowRestartNotice"
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
			if (method == null) continue;
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
```

Call `PlantSettingsUiI18n.Apply(harmony);` from `OnLoad` after `PatchAll`.

- [ ] **Step 2: Build**

```bash
dotnet build mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj -c Debug
```

Expected: success.

- [ ] **Step 3: Commit**

```bash
git add mods/PlantCritterSettingsCN/PlantSettingsUiI18n.cs mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs
git commit -m "Add soft-dep Chinese UI patches for Plant Settings Manager."
```

---

### Task 5: Critter Settings Manager UI i18n

**Files:**
- Create: `mods/PlantCritterSettingsCN/CritterSettingsUiI18n.cs`
- Modify: `mods/PlantCritterSettingsCN/ZhStrings.cs` (add Critter chrome strings found during inspection)
- Modify: `mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs`

**Interfaces:**
- Consumes: same translator; upstream `CritterSettings.*` settings screen type names (discover via `AccessTools` / strings in installed DLL)
- Produces: `CritterSettingsUiI18n.Apply(Harmony)`

- [ ] **Step 1: Discover Critter settings screen type/methods**

```bash
strings "$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/3734363283/"*.dll \
  | rg "SettingsScreen|BuildUI|Populate|SelectCritter|ModsScreen"
```

Identify the settings UI class (likely `CritterSettings.SettingsScreen` or similar) and methods analogous to Plant.

- [ ] **Step 2: Implement `CritterSettingsUiI18n.Apply` mirroring Plant**

Same soft-patch pattern; method list filled from Step 1. If Mods list Config button exists, postfix that too. On missing types: warning + return.

Wire `CritterSettingsUiI18n.Apply(harmony);` in `OnLoad`.

- [ ] **Step 3: Extend `ZhStrings` with Critter chrome**

Add exact/partial entries for labels observed in Critter settings (egg lay, incubation, temperature, diet/space fields, save/reset buttons, etc.). Prefer capturing strings from the DLL / in-game English UI.

- [ ] **Step 4: Build**

```bash
dotnet build mods/PlantCritterSettingsCN/PlantCritterSettingsCN.csproj -c Debug
```

Expected: success; Dev folder updated.

- [ ] **Step 5: Commit**

```bash
git add mods/PlantCritterSettingsCN/CritterSettingsUiI18n.cs mods/PlantCritterSettingsCN/ZhStrings.cs mods/PlantCritterSettingsCN/PlantCritterSettingsCNMod.cs
git commit -m "Add soft-dep Chinese UI patches for Critter Settings Manager."
```

---

### Task 6: In-game verification + docs touch-up

**Files:**
- Modify: `mods/PlantCritterSettingsCN/mod.yaml` if load-order wording needs tightening
- Optionally modify: design/plan only if gaps found (avoid drive-by)

**Interfaces:** none

- [ ] **Step 1: Enable mods for SO in `mods.json`**

Enable for Spaced Out (`enabledForDlc` contains `EXPANSION1_ID`):

1. Plant Settings Manager (`3733686186`)
2. Critter Settings Manager (`3734363283`)
3. PlantCritterSettingsCN (Dev entry / local staticID `wenkaigu.PlantCritterSettingsCN`)

Ensure CN helper loads **after** the two upstream mods in list order.

- [ ] **Step 2: Manual game checks**

| Case | Expect |
|---|---|
| All three on | Plant/Critter settings UI Chinese (chrome + list names); Consumables + top-left food Chinese; fridge click Chinese |
| Only this mod | Game OK; food lists Chinese |
| Plant only, this mod off | Food English returns (control) |

Capture notes from `Player.log`: look for `[PlantCritterSettingsCN] FoodInfo refresh` and i18n armed lines.

- [ ] **Step 3: Final commit if yaml/log-driven string fixes were needed**

```bash
git add mods/PlantCritterSettingsCN
git commit -m "Polish PlantCritterSettingsCN strings after in-game verification."
```

(Skip empty commit if nothing changed.)

---

## Spec coverage self-check

| Spec requirement | Task |
|---|---|
| New folder `mods/PlantCritterSettingsCN/` | Task 1 |
| Archive old CN unpublished | Global constraint (no change required if already false) |
| FoodInfo refresh after Localization | Task 2 |
| SaveLoader refresh hardening | Task 2 |
| Plant UI chrome + plant names | Tasks 3–4 |
| Critter UI chrome + critter names | Tasks 3, 5 |
| Soft-dep skip missing upstream | Tasks 4–5 |
| Food fix always on | Task 2 |
| Build + Dev deploy | Tasks 1–5 |
| In-game acceptance | Task 6 |
| No upstream DLL compile refs | Tasks 4–5 (`AccessTools`) |

## Placeholder / consistency scan

- No TBD left for required v1 paths; Critter method names discovered at Task 5 Step 1 (explicit discovery step, not a vague TODO).
- Namespace consistently `Wenkaigu.PlantCritterSettingsCN`.
- staticID consistently `wenkaigu.PlantCritterSettingsCN`.
