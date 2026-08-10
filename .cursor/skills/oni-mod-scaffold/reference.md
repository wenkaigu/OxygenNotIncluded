# ONI Mod Scaffold Reference

## Minimal `UserMod2` entry

```csharp
using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;

namespace Author.ModName
{
    public class Mod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            harmony.PatchAll();
        }
    }
}
```

Adjust PLib init to match the PLib version in use (modular PLib 4.x may only need the packages you reference).

## `mod.yaml`

```yaml
title: "Mod Title"
description: "Short description shown in the mods UI"
staticID: "Author.ModName"
```

## `mod_info.yaml`

```yaml
supportedContent: ALL
minimumSupportedBuild: 526233
version: 1.0.0
APIVersion: 2
```

Notes:

- `supportedContent`: `ALL` | `VANILLA_ID` | `EXPANSION1_ID`
- Update `minimumSupportedBuild` to the game build you actually test against
- `APIVersion: 2` is required for Harmony 2 / mergedown-era loading

## csproj expectations (conceptual)

- `TargetFramework`: typically `net472` or whatever the repo standardizes on
- Package refs: `PLib`, Harmony ref as required by PLib/game, `ILRepack` (or ILMerge) MSBuild task
- Post-build or pack step outputs: `ModName.dll`, `mod.yaml`, `mod_info.yaml`
- Game assembly references: `Assembly-CSharp`, `Assembly-CSharp-firstpass`, Unity modules as needed — paths from props, not hardcoded usernames in committed files

## Local deploy

```
<Klei>/OxygenNotIncluded/mods/Dev/<ModName>/
  ModName.dll
  mod.yaml
  mod_info.yaml
```

## Repo layout reminder

```
mods/
  <ModName>/
    <ModName>.csproj
    Mod.cs
    Patches/
Directory.Build.props
Directory.Build.props.user   # gitignored — game install path
```
