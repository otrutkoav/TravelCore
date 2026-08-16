<script setup lang="ts">
import { onMounted,reactive,ref } from 'vue'
import type { TourRailwayTransferDto,TourServiceDto } from '@/api/contracts'
import { tourRailwayTransferApi } from '@/api/resources'
import RemoteMultiLookup from '@/components/RemoteMultiLookup.vue'

const props=defineProps<{tourId:number;service:TourServiceDto}>(),emit=defineEmits<{close:[];saved:[]}>()
const form=reactive<TourRailwayTransferDto>({DepartureCountryIds:[],DepartureCityIds:[],ArrivalCountryIds:[],ArrivalCityIds:[],RailwayTransferIds:[],TransportIds:[],DepartureTimeFrom:'00:00',DepartureTimeTo:'23:59',ArrivalTimeFrom:'00:00',ArrivalTimeTo:'23:59'})
const error=ref(''),loading=ref(true),saving=ref(false)
function time(value:string|undefined){const match=String(value??'').match(/(?:T|^)(\d{2}:\d{2})(?::\d{2})?/);return match?.[1]??'00:00'}
function command():TourRailwayTransferDto{return{...form,DepartureTimeFrom:`${form.DepartureTimeFrom}:00`,DepartureTimeTo:`${form.DepartureTimeTo}:00`,ArrivalTimeFrom:`${form.ArrivalTimeFrom}:00`,ArrivalTimeTo:`${form.ArrivalTimeTo}:00`}}
function validate(){
 const required:Array<[number[],string]>=[[form.DepartureCountryIds,'Выберите страну отправления.'],[form.DepartureCityIds,'Выберите город отправления.'],[form.ArrivalCountryIds,'Выберите страну прибытия.'],[form.ArrivalCityIds,'Выберите город прибытия.'],[form.RailwayTransferIds,'Выберите железнодорожный переезд.']]
 const missing=required.find(([values])=>!values.length);if(missing)return missing[1]
 if(form.DepartureTimeFrom>form.DepartureTimeTo)return 'Начало времени отправления не может быть позже окончания.'
 if(form.ArrivalTimeFrom>form.ArrivalTimeTo)return 'Начало времени прибытия не может быть позже окончания.'
 return ''
}
function assign(data:TourRailwayTransferDto){Object.assign(form,data,{DepartureTimeFrom:time(data.DepartureTimeFrom),DepartureTimeTo:time(data.DepartureTimeTo),ArrivalTimeFrom:time(data.ArrivalTimeFrom),ArrivalTimeTo:time(data.ArrivalTimeTo)})}
async function load(){error.value='';try{assign(await tourRailwayTransferApi.get(props.tourId,props.service.Id))}catch(e){error.value=e instanceof Error?e.message:'Ошибка загрузки железнодорожного переезда'}finally{loading.value=false}}
async function save(){const validation=validate();if(validation){error.value=validation;return}saving.value=true;error.value='';try{assign(await tourRailwayTransferApi.replace(props.tourId,props.service.Id,command()));emit('saved')}catch(e){error.value=e instanceof Error?e.message:'Ошибка сохранения железнодорожного переезда'}finally{saving.value=false}}
onMounted(load)
</script>

<template>
 <div class="modal-backdrop" @click.self="emit('close')"><section class="modal">
  <div class="modal-header"><div><p class="eyebrow">Услуга {{service.Name}}</p><h2>Железнодорожный переезд</h2><p>Маршрут, допустимые направления, транспорт и интервалы времени.</p></div><button class="secondary" @click="emit('close')">Закрыть</button></div>
  <div v-if="error" class="error-box">{{error}}</div><p v-if="loading">Загрузка…</p>
  <form v-else @submit.prevent="save">
   <div class="section-card"><h3>Маршрут</h3><div class="form-grid">
    <div class="field"><label>Страны отправления *</label><RemoteMultiLookup v-model="form.DepartureCountryIds" endpoint="/api/v1/geography/countries" placeholder="Выберите страны" /></div>
    <div class="field"><label>Города отправления *</label><RemoteMultiLookup v-model="form.DepartureCityIds" endpoint="/api/v1/geography/cities" filter-name="CountryId" :dependency-value="form.DepartureCountryIds" placeholder="Выберите города" /></div>
    <div class="field"><label>Страны прибытия *</label><RemoteMultiLookup v-model="form.ArrivalCountryIds" endpoint="/api/v1/geography/countries" placeholder="Выберите страны" /></div>
    <div class="field"><label>Города прибытия *</label><RemoteMultiLookup v-model="form.ArrivalCityIds" endpoint="/api/v1/geography/cities" filter-name="CountryId" :dependency-value="form.ArrivalCountryIds" placeholder="Выберите города" /></div>
   </div></div>
   <div class="section-card"><h3>Варианты и ограничения</h3><div class="form-grid">
    <div class="field"><label>Железнодорожные переезды *</label><RemoteMultiLookup v-model="form.RailwayTransferIds" endpoint="/api/v1/railway/transfers" placeholder="Выберите направления" /></div>
    <div class="field"><label>Транспорт</label><RemoteMultiLookup v-model="form.TransportIds" endpoint="/api/v1/transportation/transports" placeholder="Без ограничений" /></div>
   </div></div>
   <div class="section-card"><h3>Допустимое время</h3><div class="time-grid">
    <div class="field"><label>Отправление с</label><input v-model="form.DepartureTimeFrom" type="time" required /></div><div class="field"><label>Отправление по</label><input v-model="form.DepartureTimeTo" type="time" required /></div>
    <div class="field"><label>Прибытие с</label><input v-model="form.ArrivalTimeFrom" type="time" required /></div><div class="field"><label>Прибытие по</label><input v-model="form.ArrivalTimeTo" type="time" required /></div>
   </div></div>
   <div class="actions"><button class="primary" :disabled="saving">{{saving?'Сохранение…':'Сохранить железнодорожный переезд'}}</button></div>
  </form>
 </section></div>
</template>

<style scoped>
.modal-backdrop{position:fixed;z-index:30;inset:0;background:#101a2a99;padding:30px;overflow:auto}.modal{max-width:1050px;margin:auto;background:#f8f9fb;border-radius:12px;padding:24px;box-shadow:0 20px 60px #0004}.modal-header{display:flex;align-items:flex-start;justify-content:space-between;gap:20px;margin-bottom:20px}.modal-header h2{margin:3px 0}.modal-header p{margin:0}.section-card{margin-bottom:14px;padding:18px;border:1px solid var(--border);border-radius:10px;background:#fff}.section-card h3{margin:0 0 14px;font-size:15px}.time-grid{display:grid;grid-template-columns:repeat(4,minmax(130px,1fr));gap:14px}@media(max-width:750px){.modal-backdrop{padding:12px}.time-grid{grid-template-columns:repeat(2,1fr)}}
</style>
