<script setup lang="ts">
import { computed, nextTick, onBeforeUnmount, onMounted, ref, watch } from 'vue'
import { http } from '@/api/http'
import type { PagedResponse } from '@/api/contracts'

type LookupValue = string | number
interface LookupRow { Id?:number; Code?:string; Name?:string; NameEn?:string; FlightNumber?:string; [key:string]:unknown }

const props=withDefaults(defineProps<{
  modelValue:LookupValue|LookupValue[]|boolean|null|undefined
  endpoint:string
  multiple?:boolean
  valueKey?:string
  labelKey?:string
  filterName?:string
  dependencyValue?:LookupValue|LookupValue[]|boolean|null
  nullable?:boolean
  placeholder?:string
}>(),{multiple:false,valueKey:'Id',labelKey:'Name',nullable:true,placeholder:'Выберите значение'})
const emit=defineEmits<{'update:modelValue':[value:LookupValue|LookupValue[]|null]}>()

const root=ref<HTMLElement>(),searchInput=ref<HTMLInputElement>(),open=ref(false),loading=ref(false),error=ref(''),search=ref(''),onlySelected=ref(false)
const rows=ref<LookupRow[]>([]),selectedRows=ref<LookupRow[]>([])
const values=computed<LookupValue[]>(()=>Array.isArray(props.modelValue)?props.modelValue:props.modelValue==null||props.modelValue===''||typeof props.modelValue==='boolean'?[]:[props.modelValue])
const visibleRows=computed(()=>onlySelected.value?selectedRows.value:rows.value)

function key(row:LookupRow){return row[props.valueKey] as LookupValue}
function title(row:LookupRow){
 const route=row.CityFromId!=null&&row.CityToId!=null?`Город ${row.CityFromId} → город ${row.CityToId}`:undefined
 return String(row[props.labelKey]??row.Name??row.NameEn??row.FlightNumber??row.Code??route??`Запись ${row.Id??''}`)
}
function subtitle(row:LookupRow){const value=String(key(row)??'');const code=row.Code&&props.valueKey!=='Code'?` · ${row.Code}`:'';return `${value}${code}`}
function selected(value:LookupValue){return values.value.some(item=>String(item)===String(value))}
function mergeSelected(incoming:LookupRow[]){const map=new Map(selectedRows.value.map(row=>[String(key(row)),row]));for(const row of incoming)if(selected(key(row)))map.set(String(key(row)),row);selectedRows.value=[...map.values()].filter(row=>selected(key(row)))}

async function request(dependency?:LookupValue){
  const params:Record<string,unknown>={page:1,pageSize:100,sortBy:'name',sortDirection:0}
  if(search.value.trim())params['filter.search']=search.value.trim()
  if(props.filterName&&dependency!=null&&dependency!=='')params[`filter.${props.filterName.charAt(0).toLowerCase()+props.filterName.slice(1)}`]=dependency
  const data=(await http.get<PagedResponse<LookupRow>|LookupRow[]>(props.endpoint,{params})).data
  return Array.isArray(data)?data:data.Items
}
async function hydrateMissing(items:LookupRow[]){
  if(props.valueKey!=='Id')return items
  const known=new Set(items.map(row=>String(key(row))))
  const missing=values.value.filter(value=>!known.has(String(value)))
  const loaded=await Promise.all(missing.map(async value=>{try{return (await http.get<LookupRow>(`${props.endpoint}/${value}`)).data}catch{return undefined}}))
  return [...loaded.filter((row):row is LookupRow=>Boolean(row)),...items]
}
async function load(){
  loading.value=true;error.value=''
  try{
    const dependencies=Array.isArray(props.dependencyValue)?props.dependencyValue:props.dependencyValue!=null&&typeof props.dependencyValue!=='boolean'?[props.dependencyValue]:[undefined]
    const batches=await Promise.all(dependencies.map(value=>request(value)))
    const map=new Map<string,LookupRow>()
    for(const row of batches.flat())map.set(String(key(row)),row)
    const result=await hydrateMissing([...map.values()])
    rows.value=result;mergeSelected(result)
  }catch(e){error.value=e instanceof Error?e.message:'Не удалось загрузить справочник.'}finally{loading.value=false}
}
function toggle(row:LookupRow){
  const value=key(row);if(value==null)return
  if(!props.multiple){selectedRows.value=[row];emit('update:modelValue',value);open.value=false;return}
  const next=selected(value)?values.value.filter(item=>String(item)!==String(value)):[...values.value,value]
  if(selected(value))selectedRows.value=selectedRows.value.filter(item=>String(key(item))!==String(value));else selectedRows.value.push(row)
  emit('update:modelValue',next)
}
function remove(value:LookupValue){
  if(props.multiple){emit('update:modelValue',values.value.filter(item=>String(item)!==String(value)));selectedRows.value=selectedRows.value.filter(row=>String(key(row))!==String(value))}
  else{emit('update:modelValue',null);selectedRows.value=[]}
}
async function show(){open.value=true;await nextTick();searchInput.value?.focus();if(!rows.value.length)await load()}
function clear(){emit('update:modelValue',props.multiple?[]:null);selectedRows.value=[]}
function outside(event:MouseEvent){if(root.value&&!root.value.contains(event.target as Node))open.value=false}
let timer:number|undefined
function delayedLoad(){window.clearTimeout(timer);timer=window.setTimeout(load,250)}
watch(values,()=>mergeSelected(rows.value),{deep:true})
watch(()=>props.dependencyValue,(value,previous)=>{if(previous!==undefined&&JSON.stringify(value)!==JSON.stringify(previous))clear();rows.value=[];void load()},{deep:true})
onMounted(()=>{document.addEventListener('mousedown',outside);void load()})
onBeforeUnmount(()=>{document.removeEventListener('mousedown',outside);window.clearTimeout(timer)})
</script>

<template>
 <div ref="root" class="smart-lookup" :class="{expanded:open}">
  <div class="smart-control" role="combobox" :aria-expanded="open" tabindex="0" @click="show" @keydown.enter.prevent="show" @keydown.esc="open=false">
   <div v-if="selectedRows.length" class="smart-tags">
    <span v-for="row in selectedRows" :key="String(key(row))" class="smart-tag" :title="`${title(row)} · ${subtitle(row)}`">{{title(row)}}<button type="button" aria-label="Убрать" @click.stop="remove(key(row))">×</button></span>
   </div>
   <span v-else class="smart-placeholder">{{placeholder}}</span>
   <button v-if="values.length" type="button" class="smart-clear" title="Очистить" @click.stop="clear">×</button><span class="smart-arrow">⌄</span>
  </div>
  <div v-if="open" class="smart-menu">
   <div class="smart-search"><span>⌕</span><input ref="searchInput" v-model="search" placeholder="Поиск по названию или коду" @input="delayedLoad" @keydown.esc="open=false" /></div>
   <div class="smart-options">
    <div v-if="loading" class="smart-state"><i class="spinner"></i>Загрузка…</div>
    <div v-else-if="error" class="smart-state error">{{error}} <button type="button" @click="load">Повторить</button></div>
    <div v-else-if="!visibleRows.length" class="smart-state">Ничего не найдено</div>
    <button v-for="row in visibleRows" v-else :key="String(key(row))" type="button" class="smart-option" :class="{chosen:selected(key(row))}" @click="toggle(row)">
     <span v-if="multiple" class="smart-check">{{selected(key(row))?'✓':''}}</span><span class="smart-option-text"><strong>{{title(row)}}</strong><small>{{subtitle(row)}}</small></span>
    </button>
   </div>
   <footer class="smart-footer"><span>{{values.length}} выбрано</span><label v-if="multiple"><input v-model="onlySelected" type="checkbox" /> Только выбранные</label><button type="button" @click="open=false">Готово</button></footer>
  </div>
 </div>
</template>

<style scoped>
.smart-lookup{position:relative;min-width:0}.smart-lookup.expanded{z-index:100}.smart-control{min-height:42px;display:flex;align-items:center;gap:7px;border:1px solid #cbd5e1;border-radius:8px;padding:5px 9px;background:#fff;cursor:text;outline:none;transition:.15s}.smart-control:focus,.expanded .smart-control{border-color:#3977eb;box-shadow:0 0 0 3px #3977eb1a}.smart-tags{display:flex;flex:1;flex-wrap:wrap;gap:5px;min-width:0}.smart-tag{display:inline-flex;align-items:center;gap:5px;max-width:220px;border:1px solid #d8e2f1;border-radius:5px;padding:4px 5px 4px 8px;background:#f2f6fc;color:#263a57;font-size:13px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis}.smart-tag button,.smart-clear{border:0;background:none;color:#718097;cursor:pointer;font-size:16px;padding:0 2px}.smart-placeholder{flex:1;color:#8794a7}.smart-arrow{color:#718097;font-size:18px}.smart-menu{position:absolute;z-index:100;top:calc(100% + 6px);left:0;width:max(100%,360px);max-width:min(560px,calc(100vw - 40px));border:1px solid #cfd8e5;border-radius:10px;background:#fff;box-shadow:0 16px 40px #20304a2b;overflow:hidden}.smart-search{display:flex;align-items:center;gap:8px;padding:10px;border-bottom:1px solid #e5eaf1}.smart-search input{width:100%;border:0!important;outline:0;padding:4px!important}.smart-options{max-height:300px;overflow:auto;padding:5px}.smart-option{display:flex;align-items:center;gap:10px;width:100%;border:0;border-radius:6px;background:#fff;padding:8px 9px;text-align:left;cursor:pointer}.smart-option:hover{background:#f1f5fb}.smart-option.chosen{background:#edf4ff}.smart-check{display:grid;place-items:center;width:19px;height:19px;border:1px solid #b8c4d4;border-radius:4px;color:#fff}.chosen .smart-check{border-color:#286be6;background:#286be6}.smart-option-text{display:flex;min-width:0;flex:1;align-items:baseline;justify-content:space-between;gap:12px}.smart-option-text strong{overflow:hidden;text-overflow:ellipsis;white-space:nowrap;font-size:13px}.smart-option-text small{color:#718097;white-space:nowrap}.smart-state{display:flex;justify-content:center;align-items:center;gap:8px;padding:30px;color:#718097}.smart-state.error{color:#a12323}.smart-state button{border:0;background:none;color:#286be6;cursor:pointer}.smart-footer{display:flex;align-items:center;justify-content:space-between;gap:12px;border-top:1px solid #e5eaf1;padding:9px 12px;font-size:12px;color:#637087}.smart-footer label{display:flex;align-items:center;gap:5px}.smart-footer button{border:0;border-radius:6px;padding:6px 11px;background:#286be6;color:#fff;cursor:pointer}.spinner{width:16px;height:16px;border:2px solid #d9e2ef;border-top-color:#286be6;border-radius:50%;animation:spin .7s linear infinite}@keyframes spin{to{transform:rotate(360deg)}}
</style>
