---
name: oni-mod-scaffold
description: >-
  Scaffold a new Oxygen Not Included mod with C#, Harmony 2, PLib, UserMod2,
  mod.yaml, and mod_info.yaml. Use when creating a new mod, starting an empty
  ONI project, generating mod boilerplate, or the user asks for a Hello World
  / Dev-folder deployable skeleton. Always copy mods/HelloWorld/ as the template.
---

# ONI Mod Scaffold

## Canonical template

**Always start from `mods/HelloWorld/`.** Copy that project (csproj, `UserMod2` entry, yaml, PLib + ILRepack + Dev deploy targets) and rename `ModName` / `staticID` / namespaces. Do not invent a new csproj layout from scratch unless HelloWorld is missing or the user explicitly asks for a different structure.

## When to use

New mod, empty repo bootstrap, or "create a minimal loadable mod".

## Workflow

Copy and track:

```
Scaffold Progress:
- [ ] 1. Confirm ModName, staticID, supportedContent
- [ ] 2. Copy mods/HelloWorld/ → mods/<ModName>/ and rename IDs/namespaces/csproj
- [ ] 3. Keep PLib init + harmony.PatchAll(); add patches/content as needed
- [ ] 4. Update mod.yaml + mod_info.yaml (APIVersion: 2)
- [ ] 5. Confirm ILRepack still merges PLib (LibraryPath includes GameManagedDir)
- [ ] 6. Build and deploy DLL+yaml to mods/Dev/<ModName>/
- [ ] 7. Sanity-check: mod appears in-game Mods list
```

### Step details

1. **IDs**: `staticID` like `Author.ModName` — stable forever. Ask if missing.
2. **Project**: Clone `mods/HelloWorld/` under `mods/<ModName>/`. Game Managed refs and Dev deploy come from `Directory.Build.props` / `.user` — do not hardcode personal paths in the csproj.
3. **Entry**: `UserMod2` subclass; call `harmony.PatchAll()` in `OnLoad` if using attribute patches. Initialize PLib per current PLib docs (register options only if needed).
4. **Metadata**: Both yaml files required in output. See [reference.md](reference.md).
5. **PLib**: Merge into the mod DLL — do not ship a loose PLib dependency expectation. Match HelloWorld’s ILRepack target (resolve Newtonsoft etc. via `$(GameManagedDir)`).
6. **Deploy**: Copy only mod DLL + yaml (never game DLLs) to Dev mods folder (HelloWorld’s post-build target already does this).
7. **Verify**: Build succeeds; list what was created and how to test in-game.

## Done criteria

- Compiles
- Metadata valid (`APIVersion: 2`)
- Deploy path documented
- No invented game APIs in the skeleton
- New mod clearly derived from `mods/HelloWorld/` (or intentional documented divergence)

## Additional resources

- In-repo template: `mods/HelloWorld/`
- Field notes: [reference.md](reference.md)
