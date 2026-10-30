export class PlayerController {
 constructor(element,width=4.5){this.element=element;this.width=width;this.x=0;this.targetX=0;this.down=false;this.lastX=0;this.bind();}
 bind(){this.element.addEventListener('pointerdown',e=>{this.down=true;this.lastX=e.clientX;this.element.setPointerCapture?.(e.pointerId);});this.element.addEventListener('pointermove',e=>{if(!this.down)return;this.targetX=Math.max(-this.width,Math.min(this.width,this.targetX+(e.clientX-this.lastX)/innerWidth*this.width*2.4));this.lastX=e.clientX;});for(const t of ['pointerup','pointercancel'])this.element.addEventListener(t,()=>this.down=false);}
 update(dt){this.x+=(this.targetX-this.x)*(1-Math.exp(-12*dt));return this.x;}
 setWidth(v){this.width=v;}
}