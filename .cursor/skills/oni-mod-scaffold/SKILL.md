---
name: oni-mod-scaffold
description: >-
  Scaffold a new Oxygen Not Included mod with C#, Harmony 2, PLib, UserMod2,
  mod.yaml, and mod_info.yaml. Use when creating a new mod, starting an empty
  ONI project, generating mod boilerplate, or the user asks for a Hello World
  / Dev-folder deployable skeleton.
---

# ONI Mod Scaffold

## When to use

New mod, empty repo bootstrap, or "create a minimal loadable mod".

## Workflow

Copy and track:

```
Scaffold Progress:
- [ ] 1. Confirm ModName, staticID, supportedContent
- [ ] 2. Create mods/<ModName>/ project + csproj refs
- [ ] 3. Add UserMod2 entry + optional PLib init
- [ ] 4. Add mod.yaml + mod_info.yaml (APIVersion: 2)
- [ ] 5. Wire ILRepack/ILMerge for PLib
- [ ] 6. Build and deploy DLL+yaml to mods/Dev/<ModName>/
- [ ] 7. Sanity-check: mod appears in-game Mods list
```

### Step details

1. **IDs**: `staticID` like `Author.ModName` — stable forever. Ask if missing.
2. **Project**: Place under `mods/<ModName>/`. Reference game Managed assemblies via props template; keep personal paths in `Directory.Build.props.user`.
3. **Entry**: `UserMod2` subclass; call `harmony.PatchAll()` in `OnLoad` if using attribute patches. Initialize PLib per current PLib docs (register options only if needed).
4. **Metadata**: Both yaml files required in output. See [reference.md](reference.md).
5. **PLib**: Merge into the mod DLL — do not ship a loose PLib dependency expectation.
6. **Deploy**: Copy only mod DLL + yaml (never game DLLs) to Dev mods folder.
7. **Verify**: Build succeeds; list what was created and how to test in-game.

## Done criteria

- Compiles
- Metadata valid (`APIVersion: 2`)
- Deploy path documented
- No invented game APIs in the skeleton

## Additional resources

- Templates and field notes: [reference.md](reference.md)
