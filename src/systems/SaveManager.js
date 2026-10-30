const KEY='army-runner-save-v1';
const DEFAULTS={currentLevel:1,unlockedLevel:1,soundEnabled:true};
export class SaveManager {
 constructor(storage=globalThis.localStorage){this.storage=storage;}
 load(){try{const raw=this.storage?.getItem(KEY);if(!raw)return {...DEFAULTS};const s={...DEFAULTS,...JSON.parse(raw)};s.currentLevel=Math.max(1,Math.floor(Number(s.currentLevel)||1));s.unlockedLevel=Math.max(1,Math.floor(Number(s.unlockedLevel)||1));s.soundEnabled=Boolean(s.soundEnabled);return s;}catch{return {...DEFAULTS};}}
 save(patch){const next={...this.load(),...patch};try{this.storage?.setItem(KEY,JSON.stringify(next));}catch(e){console.warn('Save unavailable',e);}return next;}
 setCurrentLevel(level){return this.save({currentLevel:level});}
 unlockLevel(level){const s=this.load();return this.save({unlockedLevel:Math.max(s.unlockedLevel,level),currentLevel:level});}
 setSoundEnabled(enabled){return this.save({soundEnabled:Boolean(enabled)});}
 reset(){try{this.storage?.removeItem(KEY);}catch{}return {...DEFAULTS};}
}