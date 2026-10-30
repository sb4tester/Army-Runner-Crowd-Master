# Release Readiness Checklist

This checklist is used to decide whether a build is ready for internal testing, beta, or public release.

## Project and source control
- [ ] Unity editor version pinned and documented.
- [ ] Package versions pinned; package compatibility reviewed.
- [ ] Unity project opens without compile errors.
- [ ] No secrets, signing keys, credentials, or private SDK files committed.
- [ ] Third-party assets and packages have recorded licenses/attribution.
- [ ] Git LFS configured if binary assets require it.
- [ ] CI result is recorded for the exact commit.

## Gameplay
- [ ] Main menu, settings, pause, HUD, win/loss, retry, and progression work.
- [ ] All 50 levels load and validate.
- [ ] Gate alternatives are visible, deterministic, and selectable.
- [ ] Crowd calculations handle boundaries and invalid values.
- [ ] Obstacles and combat resolve deterministically.
- [ ] Offline play works with network disabled.
- [ ] Save migration and corrupted-save fallback are tested.

## UX and accessibility
- [ ] Touch, mouse/keyboard, and gamepad flows are tested.
- [ ] UI safe areas and aspect ratios are tested.
- [ ] Text and gate labels remain readable.
- [ ] Audio settings persist.
- [ ] Pause and resume behave correctly.

## Performance
- [ ] Representative low/mid mobile devices profiled.
- [ ] Crowd rendering uses pooling/instancing/LOD as appropriate.
- [ ] Memory and loading budgets measured per platform.
- [ ] Web download size and browser memory tested.
- [ ] Thermal, suspend/resume, and low-memory behavior tested where applicable.

## Monetization and privacy
- [ ] Ads are optional and do not interrupt active gameplay.
- [ ] Ad failures do not block progression.
- [ ] Reward callbacks are verified before granting rewards.
- [ ] IAP purchase and restore flows tested on supported stores.
- [ ] Unsupported platforms hide or disable unavailable services gracefully.
- [ ] Privacy policy, consent, age rating, and store disclosures reviewed.
- [ ] SDK data collection and licenses reviewed.

## Platform release
- [ ] Platform-specific build generated from pinned editor version.
- [ ] Real-device/runtime smoke test completed.
- [ ] Store assets, screenshots, descriptions, and age/content ratings prepared.
- [ ] Signing, provisioning, and package identifiers verified.
- [ ] Console developer approval, SDK access, hardware testing, and certification completed where applicable.
- [ ] Known issues and release notes documented.

## Release decision
- [ ] Internal test approved.
- [ ] Beta approved.
- [ ] Public release approved.
