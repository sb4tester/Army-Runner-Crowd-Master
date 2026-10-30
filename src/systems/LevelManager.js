export class LevelManager {
 constructor(url='/data/levels.json'){this.url=url;this.levels=new Map();this.currentLevelId=1;}
 async load(){const r=await fetch(this.url);if(!r.ok)throw new Error('Level config load failed: '+r.status);const d=await r.json();for(const l of d.levels??[]){this.validate(l);this.levels.set(l.id,Object.freeze(l));}if(!this.levels.size)throw new Error('No levels configured');return this;}
 validate(l){for(const k of ['id','speed','trackWidth','gateValues','obstaclesCount','enemyPower','distance'])if(l[k]===undefined)throw new Error('Level '+l.id+' missing '+k);if(!Number.isInteger(l.id)||l.id<1||l.speed<=0||l.enemyPower<0||l.obstaclesCount<0||!Array.isArray(l.gateValues)||!l.gateValues.length)throw new Error('Invalid level '+l.id);}
 getLevel(id=this.currentLevelId){const l=this.levels.get(id);if(!l)throw new Error('Level '+id+' not found');return l;}
 setCurrentLevel(id){this.getLevel(id);this.currentLevelId=id;return this.getLevel();}
 nextLevel(){return this.levels.has(this.currentLevelId+1)?this.setCurrentLevel(this.currentLevelId+1):null;}
 get totalLevels(){return this.levels.size;} get isLastLevel(){return !this.levels.has(this.currentLevelId+1);}
}