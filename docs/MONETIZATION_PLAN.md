# Monetization Plan — Ads and In-App Purchases

**Status:** Approved direction; providers and products are not yet selected or integrated.

## 1. Principles

- The complete core game loop must remain playable offline.
- Ads and purchases are optional services and must never be required to start or finish a normal level.
- Do not interrupt active running, gate selection, or combat with ads.
- Keep monetization integrations replaceable behind interfaces.
- Follow the rules of each target store and advertising provider.

## 2. Advertising design

### Rewarded ads
- Opt-in only; clearly explain the reward before the user chooses to watch.
- Candidate rewards: one optional continue after a loss, or a modest bonus after a completed level.
- Limit frequency and avoid making a reward necessary for normal progression.
- If no ad is available or the device is offline, continue without penalty.

### Interstitial ads
- Only consider at natural breaks after a result or when returning to a menu.
- Never show during active gameplay, gate decisions, combat, pause transitions, or immediately after app launch.
- Apply conservative frequency caps and cooldowns.
- Disable or replace the format on platforms where the provider is unsupported or disallowed.

### No-ads option
- Consider a one-time remove-ads purchase where platform policies and the selected ad provider allow it.
- Define precisely which ad formats are removed; rewarded ads should remain an explicit user choice only if the product design and store rules permit.
- Validate entitlement through the platform store.

## 3. In-app purchase candidates

| Product | Type | Purpose |
|---|---|---|
| Remove Ads | Non-consumable | Removes eligible forced ads |
| Cosmetic army skins | Non-consumable or platform-appropriate entitlement | Visual customization |
| Cosmetic character packs | Non-consumable | Optional appearance content |
| Optional reward bundle | Consumable only if approved by store rules | Convenience, not required progression |

Do not finalize prices or product IDs until target stores and commercial strategy are confirmed.

## 4. Architecture

Suggested abstractions:
- `IAdsService`: initialization, availability, rewarded ad request, interstitial request, consent state.
- `IStoreService`: product catalog, purchase, restore, entitlement query.
- `IMonetizationPolicy`: platform eligibility, ad frequency limits, and product availability.

The game should use no-op implementations if the service is unsupported, not configured, or offline. Gameplay code should not reference a vendor SDK directly.

## 5. Privacy and user protection

- Choose SDKs only after reviewing their current privacy disclosures, supported platforms, data collection, and license terms.
- Obtain consent where required before personalized ads or analytics.
- Provide a privacy policy and accurate store disclosures.
- Avoid collecting identifiers or personal data for offline gameplay.
- Design child-directed and age-rating decisions before enabling ad personalization or purchases.
- Provide clear purchase confirmation and restore-purchase flows where required.
- Do not use deceptive UI, accidental-click layouts, fake urgency, or misleading reward claims.

## 6. Console and web considerations

- Do not assume mobile ad networks or mobile billing SDKs are supported on desktop, web, or consoles.
- Use the platform's approved store/billing mechanisms where required.
- Treat console monetization as a separate compliance and integration workstream.
- For web, confirm hosting, ad provider, consent, and browser compatibility before enabling ads.
- Keep all platform-specific products behind capability checks.

## 7. Acceptance criteria

- Game remains playable with network disabled.
- Failed ad initialization or ad load never blocks progression.
- Reward is granted only after a verified rewarded-ad completion callback.
- Interstitials occur only at approved break points and respect cooldowns.
- Purchases are validated and entitlements survive app restart.
- Restore purchases works where required.
- Unsupported platforms expose no broken purchase/ad buttons.
- Privacy policy and store disclosures match actual SDK behavior.
