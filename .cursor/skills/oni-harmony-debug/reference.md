# ONI Harmony Debug Reference

## Failure patterns

| Symptom | Likely cause | First check |
|---------|--------------|-------------|
| Mod disabled / won't load | Missing or wrong `mod_info.yaml`, `APIVersion` not `2` | Metadata next to DLL in Dev folder |
| Crash during mod load | Exception in `OnLoad` / PLib init / `PatchAll` | Stack trace pointing at your `Mod` class |
| `HarmonyException` / patch apply fail | Wrong target method, overload mismatch, signature drift after game update | `[HarmonyPatch]` type + method + argument types |
| Patch "never runs" | Wrong method patched; Prefix always skips; runs before systems exist | Add temporary log in Postfix; confirm `nameof` target |
| NRE in Postfix | Assumed instance/state not ready; null `__instance` fields | Guard nulls; defer to after `Db` / world gen as appropriate |
| Breaks with other mods | Competing Prefix skips / shared static state | Make changes additive; avoid unconditional skip |
| Works in Editor ideas but not game | Wrong deploy folder; stale DLL; ILRepack missed PLib types | Confirm Dev path timestamps; type load errors for PLib |

## Timing notes

- `UserMod2.OnLoad` is early — not everything in `Db` / world is available.
- Prefer registering building configs / strings at the game-supported registration points, not ad-hoc mid-frame.
- If a patch must run after DB init, patch a method known to run at that phase (or use established PLib/game callbacks) instead of hoping `OnLoad` is late enough.

## Isolation tactic

1. Comment out / remove `PatchAll` temporarily — if crash disappears, bisect patch classes.
2. Re-enable half the patches until the culprit is found.
3. Keep the minimal repro patch when asking for further help.

## What to paste when stuck

- First exception + 30 lines of stack from `Player.log`
- The `[HarmonyPatch]` declaration and method signature
- Game build number and mod `version` / `minimumSupportedBuild`
- Whether the crash is solo or only with other mods
