---
name: oni-harmony-debug
description: >-
  Diagnose Oxygen Not Included mod crashes, Harmony patch failures, and mods
  that fail to load or take effect. Use when the game crashes on startup, a
  patch does nothing, NullReferenceException appears, Player.log shows Harmony
  errors, or the user asks to debug an ONI mod.
---

# ONI Harmony Debug

## When to use

Startup crash, silent no-op patches, NRE after load, Harmony/`UserMod2` errors in logs.

## Workflow

```
Debug Progress:
- [ ] 1. Reproduce and capture exact symptom (startup vs in-game)
- [ ] 2. Read Player.log (and mod log if any) for first relevant exception
- [ ] 3. Identify failing mod / patch / timing (Db init, OnLoad, runtime)
- [ ] 4. Form a single hypothesis; prefer smallest failing patch
- [ ] 5. Fix or isolate (disable patch, defer init, fix target method)
- [ ] 6. Rebuild, redeploy Dev folder, retest
```

### Log locations (macOS / Windows)

- macOS: `~/Library/Application Support/unity.Klei.Oxygen Not Included/Player.log` (path may vary slightly by install)
- Windows: `%USERPROFILE%\AppData\LocalLow\Klei\Oxygen Not Included\Player.log`

Search for: mod `staticID`, `Harmony`, `NullReferenceException`, `TypeLoadException`, `ReflectionTypeLoadException`.

### Common failure classes

See [reference.md](reference.md). Start there before large refactors.

## Rules of engagement

- One hypothesis at a time; change one variable per rebuild when possible.
- Do not "fix" by swallowing exceptions in patches.
- Prefer correcting patch targets / timing over Transpilers.
- After fix: state root cause, what changed, and how it was verified.

## Done criteria

- Root cause named
- Fix builds
- Retest notes captured (what you clicked / when it used to crash)
