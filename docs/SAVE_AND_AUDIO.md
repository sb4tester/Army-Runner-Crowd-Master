# Save & Audio Systems

## LocalStorage
`SaveManager` uses key `army-runner-save-v1` and stores:
- `currentLevel`: level to resume
- `unlockedLevel`: highest unlocked level
- `soundEnabled`: sound preference

The manager tolerates missing, malformed, or unavailable LocalStorage and falls back to defaults. Save data is local to the browser/device and is not a cloud backup. Clearing browser storage resets progression.

## Audio
`AudioManager` synthesizes simple effects using Web Audio API, avoiding binary asset licensing and download overhead. Supported cues: `gatePlus`, `gateMultiply`, `gateMinus`, `obstacle`, `combat`, `win`, and `lose`.
Browsers generally require a user gesture before audio playback. The sound toggle persists through SaveManager.
