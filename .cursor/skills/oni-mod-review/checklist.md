# ONI Mod Release Checklist

## Build & package

- [ ] Solution/project builds with zero errors
- [ ] Output folder contains mod DLL + `mod.yaml` + `mod_info.yaml` only (no `Assembly-CSharp.dll`, Unity DLLs, etc.)
- [ ] PLib types resolve in-game (ILRepack/ILMerge configured if PLib is referenced)
- [ ] Dev deploy path updated for manual smoke test when claiming runtime success

## Metadata

- [ ] `mod.yaml`: `title`, `description`, stable `staticID`
- [ ] `mod_info.yaml`: `APIVersion: 2`, `version`, `minimumSupportedBuild`, `supportedContent`
- [ ] `minimumSupportedBuild` matches a build you actually tested (or is intentionally lower with caveat)

## Harmony / runtime safety

- [ ] Patches use `typeof` + `nameof` (or justified overload targeting)
- [ ] No unconditional Prefix that skips vanilla unless explicitly required and documented
- [ ] No empty `catch` that hides patch failures
- [ ] Transpilers justified and minimal, or absent
- [ ] No reliance on game systems in `OnLoad` that are not ready yet

## Content & strings

- [ ] New building/content IDs are stable; renames called out as breaking
- [ ] User-facing strings not scattered as unexplained literals (follow repo localization practice)
- [ ] Balance-sensitive numbers have brief intent comments where non-obvious

## Repo hygiene

- [ ] No `Directory.Build.props.user` or machine-specific absolute paths committed
- [ ] No Workshop credentials or private keys
- [ ] Changelog / version bump prepared if this is a release

## Smoke test (when environment available)

- [ ] Mod appears in Mods list and enables
- [ ] New game or load does not immediately NRE
- [ ] Feature path exercised once (build building / toggle option / trigger patch condition)
