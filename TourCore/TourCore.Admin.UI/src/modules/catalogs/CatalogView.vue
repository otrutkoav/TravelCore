<script setup lang="ts">
import { computed, reactive, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import { http } from '@/api/http'
import type { PagedResponse } from '@/api/contracts'
import { navigation } from '@/app/navigation'
import RemoteLookup from '@/components/RemoteLookup.vue'
import { catalogSchemas, fieldLabel, lookupFor, type FieldKind } from './catalogSchemas'

const route=useRoute(),resource=computed(()=>String(route.params.resource)),schema=computed(()=>catalogSchemas[resource.value]),items=ref<Record<string,unknown>[]>([]),page=ref(1),total=ref(0),loading=ref(false),error=ref('')
const filters=reactive<Record<string,string|number>>({}),sortBy=ref('Id'),sortDirection=ref(0),editorOpen=ref(false),editingId=ref<number>(),form=reactive<Record<string,string|number|boolean|null>>({}),saving=ref(false)
const title=computed(()=>{for(const group of navigation)for(const item of group.children??[])if(item.to?.endsWith('/'+resource.value))return item.label;return 'Справочник'})
const columns=computed(()=>{const preferred=['Id','Code','Name','NameEn','CountryId','CityId','Status','SortOrder'];const keys=Object.keys(items.value[0]??{});return [...preferred.filter(x=>keys.includes(x)),...keys.filter(x=>!preferred.includes(x))].slice(0,8)})
function display(value:unknown){if(value==null)return '—';if(typeof value==='boolean')return value?'Да':'Нет';if(Array.isArray(value))return value.length?value.join(', '):'—';if(typeof value==='object')return JSON.stringify(value);return String(value)}
function formLookup(name:string){return lookupFor(name,(schema.value?.fields??[]).map(x=>x.name))}
function filterLookup(name:string){return lookupFor(name,(schema.value?.filters??[]).map(x=>x.name))}
function query(){const params:Record<string,unknown>={page:page.value,pageSize:20,sortBy:sortBy.value,sortDirection:sortDirection.value};for(const [key,value] of Object.entries(filters))if(value!==''&&value!=null)params[`filter.${key.charAt(0).toLowerCase()+key.slice(1)}`]=value;return params}
async function load(){if(!schema.value){error.value='Endpoint для раздела не настроен.';return}loading.value=true;error.value='';try{const result=(await http.get<PagedResponse<Record<string,unknown>>>(schema.value.endpoint,{params:query()})).data;items.value=result.Items;total.value=result.TotalCount}catch(e){items.value=[];error.value=e instanceof Error?e.message:'Ошибка загрузки'}finally{loading.value=false}}
function openCreate(){editingId.value=undefined;for(const field of schema.value?.fields??[])form[field.name]=field.kind==='boolean'?false:'';editorOpen.value=true}
function editorValue(value:unknown,kind:FieldKind|undefined){
 if(value==null)return kind==='boolean'?false:''
 if(kind==='date'){
  const match=String(value).match(/\d{4}-\d{2}-\d{2}/)
  return match?.[0]??''
 }
 if(kind==='time'){
  const text=String(value)
  const match=text.match(/(?:T|^)(\d{2}:\d{2})(?::\d{2}(?:\.\d+)?)?(?:Z|[+-]\d{2}:?\d{2})?$/)
  return match?.[1]??''
 }
 return typeof value==='string'||typeof value==='number'||typeof value==='boolean'?value:String(value)
}
async function openEdit(item:Record<string,unknown>){const id=Number(item.Id);error.value='';try{const data=(await http.get<Record<string,unknown>>(`${schema.value?.endpoint}/${id}`)).data;editingId.value=id;for(const field of schema.value?.fields??[])form[field.name]=editorValue(data[field.name],field.kind);editorOpen.value=true}catch(e){error.value=e instanceof Error?e.message:'Ошибка загрузки записи'}}
function payload(){const result:Record<string,unknown>={};for(const field of schema.value?.fields??[]){const value=form[field.name];if(value===''&&field.nullable)result[field.name]=null;else if(field.kind==='number')result[field.name]=Number(value);else result[field.name]=value}return result}
async function save(){if(!schema.value)return;saving.value=true;error.value='';try{if(editingId.value)await http.put(`${schema.value.endpoint}/${editingId.value}`,payload());else await http.post(schema.value.endpoint,payload());editorOpen.value=false;await load()}catch(e){error.value=e instanceof Error?e.message:'Ошибка сохранения'}finally{saving.value=false}}
function toggleSort(column:string){const value=column.charAt(0).toLowerCase()+column.slice(1);if(sortBy.value===value)sortDirection.value=sortDirection.value===0?1:0;else{sortBy.value=value;sortDirection.value=0}page.value=1;void load()}
watch(resource,()=>{page.value=1;editorOpen.value=false;for(const key of Object.keys(filters))delete filters[key];for(const field of schema.value?.filters??[])filters[field.name]='';sortBy.value='id';void load()},{immediate:true})
</script>
<template>
 <section>
  <div class="page-heading"><div><p class="eyebrow">Справочник</p><h2>{{title}}</h2><p>Создание, редактирование, фильтрация и сортировка.</p></div><button class="primary" @click="openCreate">Добавить</button></div>
  <div v-if="error" class="error-box">{{error}} <button class="secondary" @click="load">Повторить</button></div>
  <div class="panel">
   <form v-if="schema?.filters.length" class="catalog-filters" @submit.prevent="page=1;load()">
    <div v-for="field in schema.filters" :key="field.name" class="field"><label>{{field.label}}</label><RemoteLookup v-if="filterLookup(field.name)" v-model="filters[field.name]" :endpoint="filterLookup(field.name)!.endpoint" :value-key="filterLookup(field.name)!.valueKey" :dependency-value="filterLookup(field.name)!.dependsOn?filters[filterLookup(field.name)!.dependsOn!]:null" :filter-name="filterLookup(field.name)!.filterName" /><select v-else-if="field.kind==='boolean'" v-model="filters[field.name]"><option value="">Все</option><option value="true">Да</option><option value="false">Нет</option></select><input v-else v-model="filters[field.name]" :type="field.kind==='number'?'number':field.kind==='date'?'date':'text'" /></div>
    <button class="secondary">Применить</button>
   </form>
   <div class="table-wrap"><table><thead><tr><th v-for="column in columns" :key="column" class="sortable" @click="toggleSort(column)">{{fieldLabel(column)}} <span v-if="sortBy===column.charAt(0).toLowerCase()+column.slice(1)">{{sortDirection===0?'↑':'↓'}}</span></th></tr></thead><tbody>
   <tr v-if="loading"><td :colspan="columns.length||1">Загрузка…</td></tr><tr v-else-if="!items.length"><td :colspan="columns.length||1" class="muted">Нет данных.</td></tr>
   <tr v-for="(item,index) in items" :key="String(item.Id??index)" class="editable-row" @click="openEdit(item)"><td v-for="column in columns" :key="column">{{display(item[column])}}</td></tr>
  </tbody></table></div><div class="pager"><span class="muted">Всего: {{total}}</span><div><button class="secondary" :disabled="page<=1" @click="page--;load()">Назад</button> <button class="secondary" :disabled="page*20>=total" @click="page++;load()">Далее</button></div></div></div>
  <div v-if="editorOpen" class="modal-backdrop" @click.self="editorOpen=false"><form class="catalog-editor" @submit.prevent="save"><div class="editor-header"><div><p class="eyebrow">{{editingId?'Редактирование':'Создание'}}</p><h2>{{title}}</h2></div><button type="button" class="secondary" @click="editorOpen=false">Закрыть</button></div><div class="form-grid">
   <div v-for="field in schema?.fields" :key="field.name" class="field" :class="{wide:field.kind==='textarea'}"><label>{{field.label}}</label><RemoteLookup v-if="formLookup(field.name)" v-model="form[field.name]" :endpoint="formLookup(field.name)!.endpoint" :value-key="formLookup(field.name)!.valueKey" :dependency-value="formLookup(field.name)!.dependsOn?form[formLookup(field.name)!.dependsOn!]:null" :filter-name="formLookup(field.name)!.filterName" :nullable="field.nullable" /><input v-else-if="field.kind==='boolean'" type="checkbox" :checked="Boolean(form[field.name])" @change="form[field.name]=($event.target as HTMLInputElement).checked" /><textarea v-else-if="field.kind==='textarea'" :value="String(form[field.name]??'')" @input="form[field.name]=($event.target as HTMLTextAreaElement).value"></textarea><input v-else :value="String(form[field.name]??'')" :type="field.kind==='number'?'number':field.kind==='date'?'date':field.kind==='time'?'time':'text'" @input="form[field.name]=($event.target as HTMLInputElement).value" /></div>
  </div><div class="actions"><button class="primary" :disabled="saving">{{saving?'Сохранение…':'Сохранить'}}</button></div></form></div>
 </section>
</template>
<style scoped>
.catalog-filters{position:relative;z-index:10;display:grid;grid-template-columns:repeat(auto-fit,minmax(210px,1fr));align-items:end;gap:14px;padding:18px;border-bottom:1px solid var(--border);overflow:visible;background:#fff;border-radius:10px 10px 0 0}
.catalog-filters .field{min-width:0}.catalog-filters>.secondary{height:42px;justify-self:start}.sortable{cursor:pointer;white-space:nowrap}.editable-row{cursor:pointer}.editable-row:hover{background:#f4f7fc}.modal-backdrop{position:fixed;z-index:30;inset:0;background:#101a2a99;padding:30px;overflow:auto}.catalog-editor{max-width:900px;margin:auto;background:#fff;border-radius:12px;padding:24px}.editor-header{display:flex;align-items:start;justify-content:space-between;margin-bottom:20px}.editor-header h2{margin:4px 0}
@media(max-width:700px){.catalog-filters{grid-template-columns:1fr}.catalog-filters>.secondary{width:100%}}
</style>
