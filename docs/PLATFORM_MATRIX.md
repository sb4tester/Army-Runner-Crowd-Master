# Platform Support Matrix

**Status:** Target plan, not a claim that builds or store approvals already exist.

## 1. Target matrix

| Platform | Intended delivery | Controls | Key considerations | Release status |
|---|---|---|---|---|
| Web | Unity Web build (WebGL/Web platform as supported by pinned Unity version) | Touch, mouse, keyboard | Browser memory, download size, graphics API, hosting headers, mobile browser limits | Planned |
| Android | APK for testing; AAB for Google Play | Touch, optional controller | Device fragmentation, permissions, billing/ad SDK support, thermal and memory budgets | Planned |
| iOS/iPadOS | Xcode project/build for App Store | Touch, optional controller | macOS/Xcode signing and provisioning, App Store review, privacy manifests/SDK requirements | Planned |
| Windows | Standalone build / Steam | Keyboard, mouse, controller | Resolution, graphics quality, controller navigation, Steam integration if approved | Planned |
| macOS | Standalone build | Keyboard, mouse, controller | Apple signing/notarization and hardware compatibility | Planned |
| PlayStation | Native platform build through approved Unity platform support | Controller | Sony developer approval, platform SDK access, TRC/certification, platform-specific services | Future / gated |
| Xbox | Native platform build through approved Unity platform support | Controller | Microsoft program approval, GDK access, certification, platform-specific services | Future / gated |
| Nintendo Switch | Native platform build through approved Unity platform support | Controller | Nintendo developer approval, SDK access, performance/memory constraints, certification | Future / gated |

## 2. Important constraints

- Unity support for a platform does not grant developer access, a platform SDK, a publishing account, or store approval.
- Console work cannot be completed or tested fully without approved developer access and the relevant hardware/SDK.
- Web builds have different memory, threading, file-system, and graphics constraints from native builds. Verify the selected Unity version's web support and browser compatibility before committing to a release target.
- iOS builds require the Apple toolchain and signing workflow.
- Store policies and supported SDK versions change; re-check official platform requirements immediately before integration and submission.

## 3. Input requirements

| Action | Touch | Mouse/Keyboard | Gamepad |
|---|---|---|---|
| Steer | Drag/swipe horizontally | Mouse drag or A/D and arrows | Left stick or D-pad |
| Pause | On-screen button | Escape | Menu/Start |
| Confirm | Tap UI | Click/Enter | South face button |
| Back | UI back control | Escape | East face button |

Every menu and gameplay action must be reachable using a gamepad for console readiness.

## 4. Quality and performance targets

Targets below are engineering goals to validate, not guaranteed performance claims.

- Mobile baseline: stable 30 FPS on the agreed minimum device; aim for 60 FPS on representative mid/high devices.
- Desktop/console: target 60 FPS where hardware and platform requirements permit.
- Web: prioritize a stable experience and acceptable initial download; provide quality settings and graceful fallback.
- Memory: define per-platform budgets after profiling representative hardware.
- Crowd count: gameplay count must be independent from the number of rendered character instances.

## 5. Build validation checklist

For each platform build:
- Build from the pinned Unity editor version.
- Record build target, editor version, package lock, commit SHA, and build settings.
- Launch on a real device or supported target runtime.
- Test cold start, menu, one complete level, win, loss, retry, pause, settings, save/reload, and offline mode.
- Test screen aspect ratios, safe areas, input devices, audio focus, suspend/resume, and low-memory behavior where applicable.
- Record known issues and performance measurements.
- Do not mark a platform supported until the build and smoke tests have passed.

## 6. Console readiness gates

Before console implementation:
1. Apply to the relevant platform developer program.
2. Obtain approval and required SDK/hardware access.
3. Confirm Unity license and platform module eligibility.
4. Review technical requirements and certification checklists.
5. Confirm platform-specific save, user, controller, achievement, and store integration requirements.
6. Validate performance and memory on target hardware.
7. Complete certification and store submission.

No console release date should be promised before these gates are cleared.
