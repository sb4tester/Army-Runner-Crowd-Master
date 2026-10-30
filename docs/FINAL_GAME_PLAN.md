# Army Runner Crowd Master — Final Game Plan

**Status:** Approved planning baseline — implementation has not started  
**Engine direction:** Unity  
**Primary experience:** Offline-first single-player  
**Visual direction:** 3D Stylized Low-poly  
**Monetization direction:** Advertising + optional in-app purchases  
**Target platforms:** Web, Android, iOS, Windows, macOS, and console platforms subject to approval and platform access

## 1. Product vision

Build a polished, accessible 3D crowd-runner game in which the player steers an automatically running army, chooses number gates, avoids hazards, grows or loses the crowd, and fights an enemy force at the end of each level.

The game must be enjoyable without an account or an internet connection. Network access is optional and limited to platform services such as ads, purchases, analytics (if later approved), and cloud/platform features.

## 2. Approved product decisions

| Area | Decision |
|---|---|
| Engine | Unity |
| Art direction | 3D Stylized Low-poly |
| Game mode | Offline-first single-player |
| Initial content | 50 data-driven levels |
| Monetization | Ads + optional IAP |
| Controls | Touch, mouse/keyboard, and gamepad |
| Progress | Local save first; platform/cloud sync is an optional future adapter |
| Online multiplayer | Not in initial scope |
| Console | Architect for controller and certification needs; release only after platform approval |

## 3. Core gameplay loop

1. Launch the game and enter the main menu.
2. Select the current unlocked level.
3. The army runs forward automatically.
4. The player steers horizontally to choose between gate lanes and avoid hazards.
5. Gates modify the crowd using addition, subtraction, multiplication, or division.
6. Obstacles and enemy encounters can reduce the crowd.
7. At the finish, the remaining army fights the enemy group.
8. Resolve win/loss, award the result, unlock the next level on success, and offer retry/continue.
9. Save progress locally.

### Gate rules

- Present at least two meaningful lane choices where the level layout allows it.
- Display the operation and value clearly in world space and in a readable UI treatment.
- Define deterministic collision/selection rules; do not apply both alternatives.
- Validate division and multiplication bounds to prevent negative, zero, overflow, or runaway crowd counts.
- Make each level's gate choices deterministic and testable from level data.

### Combat rules

- Combat must be readable and deterministic, not a purely arbitrary animation.
- The initial result should be based on the player's remaining crowd versus the enemy force and any explicitly configured combat modifiers.
- Show the crowd count changing during combat and communicate victory/defeat clearly.
- Avoid spawning one full GameObject per soldier at scale; use a scalable representation and pooled visuals.

## 4. Game modes and connectivity

- The initial release is single-player and playable offline.
- No login is required for core gameplay.
- Ads and store purchases require connectivity and must fail gracefully when unavailable.
- A failed ad request must never block level start, retry, or progression.
- Purchased entitlements must be validated and restored through the platform store where supported.
- Cloud save, leaderboards, and multiplayer are out of scope until separately approved.

## 5. Art and presentation

- Use a consistent stylized low-poly visual language with clear silhouettes and strong color contrast.
- Prioritize readable gates, hazards, player army, and enemy army over decorative detail.
- Use scalable quality settings: mobile/web baseline and enhanced PC/console presentation.
- Keep character materials, VFX, lighting, and post-processing within platform budgets.
- Provide UI scaling, safe areas, readable text, and accessibility options.

## 6. Technical architecture principles

- One shared gameplay codebase with platform-specific adapters at the boundary.
- Keep gameplay logic independent from Unity platform APIs and third-party monetization SDKs.
- Use Unity Input System with action maps for touch, pointer, keyboard, and gamepad.
- Use ScriptableObjects or a validated data pipeline for authored level configuration. Preserve a documented JSON import/export format if external level editing is desired.
- Use object pooling and instancing/LOD strategies for crowds, projectiles, and repeated effects.
- Keep save data versioned and migrate safely between releases.
- Treat platform SDKs, ads, IAP, analytics, and cloud services as optional integrations behind interfaces.

## 7. Initial content scope

- 50 playable levels migrated from the existing prototype's level data.
- Increasing difficulty through crowd size, gate trade-offs, obstacle placement, enemy strength, and pacing.
- Reusable environment and obstacle sets.
- Main menu, gameplay HUD, pause/settings, win/loss, and level progression screens.
- Sound effects and music with persistent sound settings.

## 8. Monetization principles

### Advertising
- Rewarded ads are opt-in and provide a clearly described optional reward.
- Interstitial ads may appear only at natural breaks, such as after a result and before returning to the menu; never interrupt active gameplay or combat.
- Provide a no-ads purchase where the platform and store rules permit it.
- Ads must be disabled or replaced with an approved alternative on platforms where the selected ad provider is unsupported or disallowed.
- Ads must not be required to complete a level or unlock the normal level path.

### In-app purchases
- Initial candidate products: remove ads, cosmetic character/army skins, and optional convenience/reward bundles.
- Do not sell pay-to-win advantages that make normal progression unfair.
- Use platform-native billing and entitlement validation.
- Support restore purchases where required.
- Do not assume one billing SDK works on every target platform.

### Privacy and compliance
- Minimize data collection.
- Obtain required consent before personalized advertising or analytics where applicable.
- Provide a privacy policy and age/content disclosures appropriate to each store.
- Do not collect personal data for core offline gameplay.
- Complete each platform's store, consumer-protection, and privacy requirements before release.

## 9. Non-goals for the first release

- Online multiplayer or real-time PvP.
- Mandatory account registration.
- Backend dependency for saving or playing.
- User-generated content.
- Complex live operations or seasonal events.
- Console release before developer program approval and certification readiness.

## 10. Definition of done

A release candidate is not complete until:
- The Unity project opens in the documented editor version without compile errors.
- All 50 levels load and pass validation.
- The full core loop can be completed, lost, retried, and won.
- Input works with the target device's primary controls.
- Progress and settings survive application restart.
- Offline play works with network disabled.
- Ads and purchases fail safely when offline or unsupported.
- Platform builds are produced and smoke-tested on actual target devices.
- Performance, privacy, store assets, and certification checklists are reviewed.
- CI build status and known limitations are documented honestly.
