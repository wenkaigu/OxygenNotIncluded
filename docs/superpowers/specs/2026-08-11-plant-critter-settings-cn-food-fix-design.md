# PlantCritterSettingsCN + FoodInfo Fix — Design

**Date:** 2026-08-11  
**Branch:** `feat/plant-critter-settings-cn-food-fix`  
**Status:** Approved for planning (approach 1)

## Goal

Ship **one** new Oxygen Not Included mod that:

1. Localizes **Plant Settings Manager** and **Critter Settings Manager** settings UI to Simplified Chinese (chrome + plant/critter list display names).
2. Fixes English food names/descriptions in **Consumables** and the **top-left food list** caused by those upstream mods freezing `EdiblesManager.FoodInfo` strings before localization.

## Non-goals

- Do not fork or redistribute upstream DLLs.
- Do not change plant/critter balance logic.
- Do not modify game Managed assemblies.
- Do not deploy or revive `mods/PlantSettingsManagerCN/` (archive only; keep sources in repo).

## Background (root cause)

- World objects (fridge, click food) use live `LocString` / `ProperName()` → stay Chinese.
- Consumables UI and top-left food list use `FoodInfo.ConsumableName` → `FoodInfo.Name`, a **plain `string` snapshotted** in the `FoodInfo` constructor via `Strings.Get(...)`.
- `TUNING.FOOD.FOOD_TYPES` creates all `FoodInfo` instances on first access. If that happens while English is active (or names are otherwise frozen), Consumables/top-left stay English for the session.
- Plant/Critter Settings Manager run heavy `OnLoad` / `CreatePrefab` work around early init and were confirmed to reproduce the food English bug; same-author pattern for both.

## Product choices (locked)

| Decision | Choice |
|---|---|
| Packaging | Single combined mod |
| Project layout | **New folder** `mods/PlantCritterSettingsCN/` |
| Old CN mod | Keep `mods/PlantSettingsManagerCN/` as archive only (`DeployToOniDev=false`) |
| I18n depth | Settings chrome **and** plant/critter list display names |
| Soft deps | Missing upstream → skip that i18n block; **FoodInfo fix always runs** |
| Approach | Runtime UI string replace + post-localization `FoodInfo` refresh |

## Architecture

```text
mods/PlantCritterSettingsCN/          // NEW project folder
├── PlantCritterSettingsCNMod.cs      // UserMod2 entry
├── FoodInfoLocalizationFix.cs        // always attempt
├── PlantSettingsUiI18n.cs            // if Plant Settings Manager present
├── CritterSettingsUiI18n.cs          // if Critter Settings Manager present
├── ZhStrings.cs                      // exact + partial maps (+ entity names)
├── mod.yaml / mod_info.yaml
└── PlantCritterSettingsCN.csproj     // DeployToOniDev enabled for Dev testing
```

Modules are independent soft features:

| Module | When | Responsibility |
|---|---|---|
| `FoodInfoLocalizationFix` | Always | After localization (and optionally after save load), rewrite `FoodInfo.Name` / `Description` from `Strings.Get` |
| `PlantSettingsUiI18n` | Plant Settings Manager types found | Patch settings UI methods; translate chrome + plant display names |
| `CritterSettingsUiI18n` | Critter Settings Manager types found | Same for critter settings UI + critter display names |

### Compile / load rules

- **No compile-time reference** to upstream Workshop DLLs; resolve with `AccessTools.TypeByName` / `AccessTools.Method`.
- Load this mod **after** Plant Settings Manager and Critter Settings Manager (document in `mod.yaml` description).
- PLib merged via ILRepack (repo standard). Scaffold from `mods/HelloWorld/`, then enable Dev deploy.
- Add project to `OxygenNotIncluded.sln`.

Suggested identity:

- Title: `Plant & Critter Settings 汉化+食物名修复` (final title tweakable)
- `staticID`: `wenkaigu.PlantCritterSettingsCN`

## FoodInfo fix detail

1. Harmony `Postfix` on `Localization.Initialize`.
2. Enumerate registered food infos (`EdiblesManager` static list/map via public API or reflection).
3. For each entry with non-empty `Id`:
   - Read `StringEntry` via `Strings.Get("STRINGS.ITEMS.FOOD." + Id.ToUpperInvariant() + ".NAME"/".DESC")`
   - Assign the **current translated text** into the public `string` fields `Name` / `Description` (e.g. `entry.String` / equivalent), not a stale snapshot
4. Call `EdiblesManager.ClearSaveFoodCache()` when available.
5. **v1 required hardening:** also refresh on `SaveLoader.Load` postfix (Plant Settings reloads config on save load; keep Consumables/top-left correct after load).
6. Never throw out of patches; log warnings on failure.

## UI i18n detail

Reuse the archived Plant CN pattern:

- Postfix selected upstream settings-screen methods (`BuildUI`, `BuildFields`, list populate/select/show, etc.).
- Walk UI hierarchy: `LocText`, `TMP_Text`, `UnityEngine.UI.Text`.
- Replace via dictionary:
  1. Fixed chrome strings (buttons, labels, tooltips, restart notice)
  2. Plant display names (e.g. Mealwood → 米虱木)
  3. Critter display names (e.g. Hatch → 哈奇)
- Prefer official/community-common Chinese names where possible.
- Unmatched strings left unchanged.
- Seed Plant chrome maps from archived `mods/PlantSettingsManagerCN/ZhStrings.cs`; extend for Critter + entity names.

Optional enhancement (not required for v1): if upstream exposes static `DisplayNames` / `PlantDisplayNames` dictionaries and they are writable, patch those after type load — still keep UI-tree replace as the primary path.

## Error handling

- Upstream type/method missing → skip that i18n module + `Debug.LogWarning`; other modules continue.
- Food refresh reflection/`Strings.Get` failure → warning only.
- Dictionary miss → keep original text.

## Testing / acceptance

1. Only this mod: game runs; Consumables/top-left Chinese (harmless refresh).
2. This mod + Plant Settings Manager: settings UI Chinese including plant names; Consumables/top-left Chinese.
3. This mod + Critter Settings Manager: same for critters.
4. All three enabled: both UIs Chinese; food lists Chinese; fridge click still Chinese.
5. Plant Settings only (this mod off): English food names reproducible (control).
6. `dotnet build` succeeds; DLL+yaml deploy to `mods/Dev/PlantCritterSettingsCN/`.

## Implementation notes for planning

- Create **new** `mods/PlantCritterSettingsCN/` (do not rename the archive folder into this).
- Leave `mods/PlantSettingsManagerCN/` untouched except ensuring it stays unpublished.
- Prefer attribute Harmony patches for game types (`Localization`, optionally `SaveLoader`); use manual `harmony.Patch` for upstream types resolved by name.
- Keep patches minimal and additive (repo Harmony rules).
