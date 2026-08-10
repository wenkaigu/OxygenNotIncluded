# ONI Mod Scaffold Reference

Canonical in-repo template: **`mods/HelloWorld/`** — copy it when adding a new mod.

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

## csproj expectations

Prefer copying `mods/HelloWorld/HelloWorld.csproj` rather than rewriting. It already standardizes:

- `TargetFramework`: `net48` + `Microsoft.NETFramework.ReferenceAssemblies` (macOS-friendly)
- Package refs: `PLib`, `ILRepack.Lib.MSBuild.Task`
- Game refs: `0Harmony`, `Assembly-CSharp`, `Assembly-CSharp-firstpass`, `UnityEngine.CoreModule` via `$(GameManagedDir)` (`Private=false`)
- ILRepack merges PLib; `LibraryPath` includes `$(GameManagedDir)` so Newtonsoft.Json resolves from the game
- Post-build deploy of `ModName.dll` + `mod.yaml` + `mod_info.yaml` to `$(OniModsDevDir)/$(ModName)/`

## Local deploy

```
~/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Dev/<ModName>/
  ModName.dll
  mod.yaml
  mod_info.yaml
```

(See `oni-mac-paths` rule for the full path table.)

## Repo layout reminder

```
mods/
  HelloWorld/          # canonical template — keep; copy to start new mods
  <ModName>/
    <ModName>.csproj
    *Mod.cs
    Patches/
Directory.Build.props
Directory.Build.props.user.example
Directory.Build.props.user   # gitignored — game install path
OxygenNotIncluded.sln
```
