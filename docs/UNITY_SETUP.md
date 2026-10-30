# Unity Project Setup

## Pinned editor

- Unity Editor: **6000.3.27f1 (Unity 6.3 LTS)**
- Render pipeline: Universal Render Pipeline (URP)
- Input: Unity Input System 1.18.0
- Test framework: Unity Test Framework 1.4.6

Unity 6.3 LTS is selected as the production baseline rather than a short-lived Update release.

## Install Unity

1. Install Unity Hub from the official Unity website.
2. Install Editor version 6000.3.27f1.
3. Select platform modules: Android Build Support (SDK & NDK + OpenJDK), WebGL Build Support, Windows Build Support, and iOS Build Support when preparing iOS builds (Xcode is required on macOS).
4. Console platform modules and SDKs are not included in this public scaffold. They require approved developer access.

## Open the project

1. Clone the repository.
2. In Unity Hub, choose Add > Add project from disk.
3. Select the UnityProject folder.
4. Allow Package Manager to resolve the manifest dependencies.
5. Open the Unity menu: **Army Runner > Setup > Create Starter Scene**.
6. Open Assets/ArmyRunner/Scenes/Main.unity.
7. Press Play.

The editor wizard creates a simple low-poly blockout with a runner, camera follow, track, two sample gates, and a bootstrap object. It is a foundation scene, not final art or a complete level.

## Level data

The original 50-level JSON is copied to Assets/Resources/ArmyRunner/levels.json. The source remains at public/data/levels.json. Runtime loading uses Unity Resources so it works in native builds and Web builds without direct file-system access.

## Tests

Open Window > General > Test Runner, select EditMode, and run the ArmyRunner.Tests tests. The tests cover gate arithmetic and combat result resolution.

## Important limitations

- The repository scaffold has not been opened or compiled in the Unity Editor in this environment.
- No Unity build, Play Mode test, Android APK, iOS build, Web build, desktop build, or console build has been produced yet.
- The editor wizard must be run locally to generate the starter scene and materials.
- Ad and IAP SDKs are intentionally not installed or integrated in this phase.
- The legacy Three.js/Vite prototype remains preserved for reference.
