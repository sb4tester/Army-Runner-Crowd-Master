# Phase 1 — Unity Project Setup Status

**Status:** Source scaffold committed to GitHub. Local Unity Editor verification is still required.

## Committed

- [x] Preserve the original Three.js/Vite prototype.
- [x] Add Unity 6.3 LTS project root and pin editor version 6000.3.27f1.
- [x] Add URP, Input System, and Unity Test Framework package dependencies.
- [x] Add the ArmyRunner.Runtime assembly definition.
- [x] Add level schema, catalog loader, gate arithmetic, and combat result resolver.
- [x] Add runner movement, logical crowd count, and gate trigger components.
- [x] Add local versioned save service.
- [x] Copy all 50 original level definitions into Unity Resources.
- [x] Add a camera follow component.
- [x] Add an Editor menu wizard that generates a starter blockout scene.
- [x] Add EditMode tests for gate arithmetic and combat outcomes.
- [x] Add Unity setup and migration documentation.

## Verified from GitHub

- The Unity package manifest, editor version, C# source files, and test files exist on main.
- The Unity Resources level JSON parses and contains 50 levels (IDs 1–50).
- The original public/data/levels.json remains unchanged in the legacy prototype.

## Not yet verified

- [ ] Unity Package Manager resolves all dependencies.
- [ ] Unity Editor imports the project without compiler errors.
- [ ] Run Army Runner > Setup > Create Starter Scene.
- [ ] Run EditMode tests.
- [ ] Verify the starter scene in Play Mode.
- [ ] Build and smoke-test a Windows or Web player.

## Next development stage

1. Validate the Unity project on the developer's Windows machine.
2. Fix any package or compile issues reported by Unity.
3. Build the full two-lane gate-pair system with deterministic lane selection.
4. Generate track, gate, and obstacle layouts from the 50 level definitions.
5. Add crowd visual pooling and animated end-of-level combat.
6. Add menus, HUD, pause, results, and level progression.
7. Add optional ads and IAP only after the offline core loop is stable.
