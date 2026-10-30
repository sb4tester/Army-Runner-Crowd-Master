# Architecture

## Runtime layers
- **Presentation**: `index.html` HUD, menu, result overlay and sound toggle.
- **Bootstrap**: `src/main.js` wires systems and UI events.
- **Level data**: `public/data/levels.json` contains balance and layout parameters.
- **LevelManager**: fetches, validates and exposes level configuration.
- **Game**: owns Three.js scene, render loop, gates, obstacles and unit count.
- **PlayerController**: maps pointer drag to a bounded horizontal target.
- **SaveManager**: persists progression and sound preference.
- **AudioManager**: generates lightweight synthesized SFX via Web Audio API.

## Gameplay flow
1. Bootstrap loads save state and level JSON.
2. LevelManager selects the saved, unlocked level.
3. Game builds a course from the selected level config.
4. Player moves horizontally while the world scrolls toward the player.
5. Gate and obstacle triggers adjust the crowd.
6. At finish, crowd count is compared with enemyPower.
7. Win unlocks the next level; loss keeps the current level available.

## Extension points
- Replace placeholder capsules with instanced crowd models/animations.
- Add explicit left/right gate lanes and visible gate labels.
- Add object pooling for high obstacle counts.
- Replace immediate finish comparison with staged combat choreography.
- Add analytics and cloud saves behind separate adapters; never couple them to Game.
