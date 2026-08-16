<script setup lang="ts">
import { computed, onBeforeUnmount, onMounted, ref } from 'vue'

const props=withDefaults(defineProps<{modelValue:number[];min?:number;max?:number;unit?:string}>(),{min:1,max:40,unit:'ночей'})
const emit=defineEmits<{'update:modelValue':[value:number[]]}>()
const root=ref<HTMLElement>(),open=ref(false)
const options=computed(()=>Array.from({length:Math.max(0,props.max-props.min+1)},(_,index)=>props.min+index))
const selected=computed(()=>new Set(props.modelValue))

function toggle(value:number){
 const next=selected.value.has(value)?props.modelValue.filter(item=>item!==value):[...props.modelValue,value].sort((a,b)=>a-b)
 emit('update:modelValue',next)
}
function remove(value:number){emit('update:modelValue',props.modelValue.filter(item=>item!==value))}
function outside(event:MouseEvent){if(root.value&&!root.value.contains(event.target as Node))open.value=false}
onMounted(()=>document.addEventListener('mousedown',outside))
onBeforeUnmount(()=>document.removeEventListener('mousedown',outside))
</script>

<template>
 <div ref="root" class="number-multi" :class="{expanded:open}">
  <div class="number-control" role="combobox" :aria-expanded="open" tabindex="0" @click="open=true" @keydown.enter.prevent="open=true" @keydown.esc="open=false">
   <div v-if="modelValue.length" class="number-tags">
    <span v-for="value in [...modelValue].sort((a,b)=>a-b)" :key="value" class="number-tag">{{value}}<button type="button" :aria-label="`Убрать ${value}`" @click.stop="remove(value)">×</button></span>
   </div>
   <span v-else class="placeholder">Выберите продолжительности</span>
   <button v-if="modelValue.length" type="button" class="clear" title="Очистить" @click.stop="emit('update:modelValue',[])">×</button><span class="arrow">⌄</span>
  </div>
  <div v-if="open" class="number-menu">
   <div class="options">
    <button v-for="value in options" :key="value" type="button" class="option" :class="{chosen:selected.has(value)}" @click="toggle(value)">
     <span class="check">{{selected.has(value)?'✓':''}}</span><span>{{value}} {{unit}}</span>
    </button>
   </div>
   <footer><span>{{modelValue.length}} выбрано</span><button type="button" @click="open=false">Готово</button></footer>
  </div>
 </div>
</template>

<style scoped>
.number-multi{position:relative;min-width:0}.number-multi.expanded{z-index:100}.number-control{min-height:42px;display:flex;align-items:center;gap:7px;border:1px solid #cbd5e1;border-radius:8px;padding:5px 9px;background:#fff;cursor:pointer;outline:none}.number-control:focus,.expanded .number-control{border-color:#3977eb;box-shadow:0 0 0 3px #3977eb1a}.number-tags{display:flex;flex:1;flex-wrap:wrap;gap:5px}.number-tag{display:inline-flex;align-items:center;gap:5px;border:1px solid #d8e2f1;border-radius:5px;padding:4px 5px 4px 8px;background:#f2f6fc;color:#263a57;font-size:13px}.number-tag button,.clear{border:0;background:none;color:#718097;cursor:pointer;font-size:16px;padding:0 2px}.placeholder{flex:1;color:#8794a7}.arrow{color:#718097;font-size:18px}.number-menu{position:absolute;z-index:100;top:calc(100% + 6px);left:0;width:100%;min-width:260px;border:1px solid #cfd8e5;border-radius:10px;background:#fff;box-shadow:0 16px 40px #20304a2b;overflow:hidden}.options{display:grid;grid-template-columns:repeat(2,minmax(0,1fr));max-height:300px;overflow:auto;padding:6px}.option{display:flex;align-items:center;gap:8px;border:0;border-radius:6px;background:#fff;padding:7px 8px;text-align:left;cursor:pointer}.option:hover{background:#f1f5fb}.option.chosen{background:#edf4ff}.check{display:grid;place-items:center;width:18px;height:18px;border:1px solid #b8c4d4;border-radius:4px;color:#fff}.chosen .check{border-color:#286be6;background:#286be6}footer{display:flex;align-items:center;justify-content:space-between;border-top:1px solid #e5eaf1;padding:9px 12px;font-size:12px;color:#637087}footer button{border:0;border-radius:6px;padding:6px 11px;background:#286be6;color:#fff;cursor:pointer}
</style>
