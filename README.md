# Army Runner Crowd Master

A cross-platform 3D crowd-runner game project.

- Engine: Unity 6.3 LTS
- Visual style: 3D Stylized Low-poly
- Game mode: Offline-first single-player
- Monetization plan: Optional advertising + in-app purchases (not integrated yet)
- Targets: Web, Android, iOS, Windows, macOS; consoles are gated by developer approval and platform SDK access.

## Repository layout

- UnityProject/ — Unity project scaffold and initial C# foundation.
- public/ and src/ — preserved original Three.js/Vite prototype for reference.
- docs/ — product plan, migration, platform, monetization, and release documentation.

## Open the Unity project

1. Install Unity Hub.
2. Install Unity 6.3 LTS, editor version 6000.3.27f1.
3. Add Android Build Support (Android SDK & NDK + OpenJDK), WebGL Build Support, and Windows Build Support as needed. iOS requires the iOS Build Support module and Xcode on macOS.
4. In Unity Hub, choose Add > Add project from disk and select UnityProject/.
5. Wait for Package Manager to resolve packages.
6. Run Army Runner > Setup > Create Starter Scene from the Unity menu.
7. Open Assets/ArmyRunner/Scenes/Main.unity and press Play.

The Unity foundation is a work in progress, not a finished game. Builds and Play Mode behavior must be verified in the Unity Editor on the developer machine.

See docs/UNITY_SETUP.md, docs/FINAL_GAME_PLAN.md, and docs/UNITY_MIGRATION_PLAN.md.
