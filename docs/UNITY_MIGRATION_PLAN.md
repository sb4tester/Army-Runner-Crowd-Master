# Unity Migration and Architecture Plan

**Status:** Phase 1 Unity scaffold committed; local Editor verification pending.

## 1. Migration approach

The repository contains an early Three.js/Vite prototype and a new Unity project scaffold. Treat the original prototype as a gameplay reference and data source, not as code that can be mechanically converted into Unity C#.

- Preserve the original Three.js/Vite prototype; do not delete or overwrite it during migration.
- The Unity project lives in UnityProject/.
- The original 50-level JSON remains at public/data/levels.json and has been copied to Unity Resources at UnityProject/Assets/Resources/ArmyRunner/levels.json.
- Runtime data loading uses Resources so the level catalog can be included in native and Web builds.
- Continue migrating functionality in small verified stages.

## 2. Unity project baseline

- Editor: Unity 6.3 LTS, pinned to 6000.3.27f1.
- Render pipeline: Universal Render Pipeline (URP), package 17.3.0.
- Input: Unity Input System 1.18.0.
- Tests: Unity Test Framework 1.4.6.
- Runtime scripts are grouped under the ArmyRunner.Runtime assembly definition.
- EditMode tests use a separate test assembly.

Do not upgrade editor or package versions without recording the change and re-running platform tests.

## 3. Project organization

The initial scaffold uses:

- Assets/ArmyRunner/Editor — project setup wizard.
- Assets/ArmyRunner/Scripts/Core — bootstrap and game state.
- Assets/ArmyRunner/Scripts/Gameplay — level catalog and domain logic.
- Assets/ArmyRunner/Scripts/Input — platform-neutral runner movement.
- Assets/ArmyRunner/Scripts/Crowd — logical crowd count.
- Assets/ArmyRunner/Scripts/Gates — gate trigger behavior.
- Assets/ArmyRunner/Scripts/Camera — follow camera.
- Assets/ArmyRunner/Scripts/Tests/EditMode — unit tests.
- Assets/Resources/ArmyRunner — runtime level JSON.

Future systems should be added under dedicated folders for Obstacles, Combat, Progression, Save, Audio, UI, and Platform adapters.

## 4. Runtime boundaries

### Core gameplay
Owns runner state, crowd count, gate resolution, obstacle effects, combat calculation, level completion, and progression events. It must not call platform SDKs directly.

### Input
Expose normalized gameplay actions such as horizontal movement, pause, confirm, and back. Platform-specific devices map to these actions.

### Level data
Use a versioned schema with stable IDs, deterministic spawn positions, gate alternatives, obstacle definitions, enemy strength, and difficulty metadata. Validate values before a level can be loaded.

### Save
Use a versioned save model with current level, highest unlocked level, and settings. Handle corrupted data and safe defaults. Do not store payment credentials or sensitive personal data.

### Platform services
Define interfaces such as IAdsService, IStoreService, IPlatformSaveService, and IPlatformCapabilities. Provide no-op or unsupported implementations when a platform does not support a service.

## 5. Crowd rendering and performance

- Do not represent an unbounded crowd as thousands of independent MonoBehaviours with per-frame Update calls.
- Separate logical crowd count from visual representation.
- Use pooling, GPU instancing, LOD, simplified distant formations, and batched animation where appropriate.
- Establish a configurable visual cap while preserving the correct logical crowd count.
- Profile on representative low/mid-range mobile hardware before increasing visual complexity.

## 6. Migration phases

- M0: inspect and baseline the existing repository — complete.
- M1: create Unity LTS project scaffold and pin packages/editor version — scaffold committed; Editor verification pending.
- M2: implement core state model and input abstraction — initial foundation committed; continue hardening.
- M3: import and validate all 50 levels — JSON copied; automated Unity-side validation must be run.
- M4: implement full runner, gate pairs, obstacles, crowd visuals, and combat — next development stage.
- M5: implement UI, audio, save, and progression — partially scaffolded; continue.
- M6: add optional ad and IAP adapters — not started.
- M7: add platform build profiles and automated smoke checks — not started.

## 7. Engineering safeguards

- Keep Unity-generated caches and machine-specific files out of Git.
- Use Git LFS for large binary assets when repository size warrants it.
- Never commit signing keys, store credentials, API secrets, or private console SDK files.
- Document licenses and attribution for every third-party asset and package.
- Review package licenses, privacy disclosures, and SDK data collection before release.
- Keep platform-specific code behind interfaces and runtime capability checks.

## 8. Verification boundary

The GitHub scaffold is not evidence of a successful Unity import or build. Unity Editor import, Package Manager resolution, scene wizard execution, EditMode tests, Play Mode, and target builds remain to be run locally.
