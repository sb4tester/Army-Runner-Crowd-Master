# Developer Guide

## Requirements
- Node.js 20.19+ (or 22.12+)
- npm
- Git

## Local setup
```bash
git clone https://github.com/sb4tester/Army-Runner-Crowd-Master.git
cd Army-Runner-Crowd-Master
npm install
npm run dev
```
Open the local URL printed by Vite. Production build: `npm run build`; preview: `npm run preview`.

## Project conventions
- Keep gameplay systems in small ES modules under `src/systems` or `src/game`.
- Put tunable level data in `public/data/levels.json`; do not hard-code per-level balance in the engine.
- Keep persistent settings behind `SaveManager`.
- Play sound through `AudioManager`; browser audio starts/resumes after a user gesture.
- Avoid adding dependencies without documenting why.

## Change workflow
1. Create a feature branch: `git switch -c feature/short-description`.
2. Make a focused change and update relevant docs.
3. Run `npm run build` and manually test desktop + mobile pointer controls.
4. Commit with a conventional prefix: `feat:`, `fix:`, `docs:`, `refactor:`, or `test:`.
5. Open a pull request against `main`.

## Definition of done
- No console errors in normal play.
- Level JSON validates and new levels load without engine changes.
- Save migration/default behavior is safe when LocalStorage is unavailable or malformed.
- Sound can be toggled and preference survives reload.
- Build succeeds and docs reflect changed behavior.
