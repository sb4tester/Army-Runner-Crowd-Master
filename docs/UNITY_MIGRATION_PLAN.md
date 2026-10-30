# Unity Migration and Architecture Plan

**Status:** Approved plan; no Unity project or gameplay code has been created yet.

## 1. Migration approach

The current repository contains an early Three.js/Vite prototype and documentation. Treat it as a gameplay reference and data source, not as code that can be mechanically converted into Unity C#.

- Preserve the existing prototype until the Unity baseline is accepted.
- Do not delete or overwrite the Three.js prototype during the migration.
- Create the Unity project in a clearly separated directory (proposed: `UnityProject/`) or a dedicated branch after confirming repository size and Git LFS needs.
- Migrate the 50 level definitions through an explicit schema mapping and validation tool.
- Keep the original JSON as a source/reference until the Unity level pipeline is verified.

## 2. Unity project baseline

At implementation time, select the latest suitable supported Unity LTS release and pin the exact editor version in `ProjectSettings/ProjectVersion.txt`. Do not upgrade editor or package versions without a recorded change.

Recommended baseline:
- Universal Render Pipeline (URP) for scalable rendering across mobile, desktop, and supported consoles.
- Unity Input System package for touch, pointer, keyboard, and gamepad.
- TextMeshPro for readable UI.
- ScriptableObjects for runtime-ready level data; optional JSON import/export for authoring and automated validation.
- Assembly definitions to separate core gameplay, presentation, platform adapters, and tests.
- Unity Test Framework for EditMode and PlayMode tests.

Package selection must be verified against the exact pinned Unity version and every intended target platform before adoption.

## 3. Proposed code organization

```text
UnityProject/
  Assets/
    ArmyRunner/
      Art/
      Audio/
      Materials/
      Prefabs/
      Scenes/
      ScriptableObjects/
      UI/
      VFX/
      Scripts/
        Core/
        Gameplay/
        Crowd/
        Gates/
        Obstacles/
        Combat/
        Input/
        Progression/
        Save/
        Audio/
        UI/
        Platform/
          Ads/
          IAP/
          Saves/
          Console/
        Utilities/
      Tests/
        EditMode/
        PlayMode/
  Packages/
  ProjectSettings/
```

## 4. Runtime boundaries

### Core gameplay
Owns the runner state, crowd count, gate resolution, obstacle effects, combat calculation, level completion, and progression events. It must not call platform SDKs directly.

### Input
Expose normalized gameplay actions such as MoveHorizontal, Pause, Confirm, and Back. Platform-specific input bindings map to these actions.

### Level data
Use a versioned schema with stable IDs, deterministic spawn positions, gate alternatives, obstacle definitions, enemy strength, and difficulty metadata. Validate values before a level can be loaded.

### Save
Use a versioned save model with current level, highest unlocked level, settings, and owned local cosmetic selections. Write atomically where possible, handle corrupted data, and retain safe defaults. Do not store payment credentials or sensitive personal data.

### Platform services
Define interfaces such as IAdsService, IStoreService, IPlatformSaveService, and IPlatformCapabilities. Provide no-op or unsupported implementations when a platform does not support a service.

## 5. Crowd rendering and performance

- Do not represent an unbounded crowd as thousands of independent MonoBehaviours with per-frame Update calls.
- Separate logical crowd count from visual representation.
- Use pooling, GPU instancing, LOD, simplified distant formations, and batched animation where appropriate.
- Establish a configurable visual cap while preserving the correct logical crowd count.
- Profile on representative low/mid-range mobile hardware before increasing visual complexity.
- Avoid assuming that a high-end desktop result predicts WebGL or mobile performance.

## 6. Data migration

1. Read and archive the existing `public/data/levels.json`.
2. Document the current fields and semantics.
3. Define a versioned Unity level schema.
4. Write a conversion/validation step; do not silently drop unknown fields.
5. Convert all 50 levels.
6. Compare IDs, gate operations, values, obstacle placements, and enemy strengths.
7. Add tests for invalid operations, bounds, missing IDs, and duplicate IDs.
8. Keep the source JSON until migration acceptance.

## 7. Migration phases

- M0: inspect and baseline the existing repository.
- M1: create an empty Unity LTS project and pin packages/editor version.
- M2: implement core state model and input abstraction.
- M3: import and validate all 50 levels.
- M4: implement runner, gates, obstacles, crowd, and combat.
- M5: implement UI, audio, save, and progression.
- M6: add optional ad and IAP adapters.
- M7: add platform build profiles and automated smoke checks where feasible.

## 8. Engineering safeguards

- Keep Unity-generated caches and machine-specific files out of Git.
- Use Git LFS for large binary assets when repository size warrants it.
- Never commit signing keys, store credentials, API secrets, or private console SDK files.
- Document licenses and attribution for every third-party asset and package.
- Require review of package licenses, privacy disclosures, and SDK data collection before release.
- Keep platform-specific code behind compile-time/platform boundaries and runtime capability checks.
