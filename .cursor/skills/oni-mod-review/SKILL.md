---
name: oni-mod-review
description: >-
  Quality checklist for Oxygen Not Included mods before calling a feature done,
  packaging, or uploading to Steam Workshop. Use when reviewing an ONI mod,
  finishing a feature, preparing a release, or the user asks for a pre-upload
  or code review of mod changes.
---

# ONI Mod Review

## When to use

Feature complete, release/Workshop prep, or explicit mod code review.

## Process

1. Identify changed mods under `mods/<ModName>/` and read the diff.
2. Work through [checklist.md](checklist.md) — do not skip build/metadata items.
3. Report findings as:
   - **Critical** — must fix before ship
   - **Suggestion** — should fix soon
   - **Nice to have** — optional
4. Only approve "ready" if Critical is empty and build succeeds.

## Review focus (priority order)

1. Loads on `APIVersion: 2` with valid yaml
2. Patch safety (targets, skips, timing)
3. No game DLLs / secrets / absolute personal paths in output or git
4. PLib correctly merged if used
5. Strings/IDs stability and localization
6. Compatibility footguns (unconditional Prefix skips)

## Output template

```markdown
## Verdict
Ready / Not ready

## Critical
- ...

## Suggestions
- ...

## Nice to have
- ...

## Verified
- [ ] Build
- [ ] Metadata
- [ ] Deploy contents (DLL + yaml only)
```
