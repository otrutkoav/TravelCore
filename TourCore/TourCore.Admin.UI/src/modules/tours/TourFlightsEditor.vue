<script setup lang="ts">
import { onMounted,ref } from 'vue'
import type { TourFlightDto,TourFlightGroupDto,TourServiceDto } from '@/api/contracts'
import { tourFlightsApi } from '@/api/resources'
import RemoteMultiLookup from '@/components/RemoteMultiLookup.vue'
const props=defineProps<{tourId:number;service:TourServiceDto}>(),emit=defineEmits<{close:[];saved:[]}>()
const groups=ref<TourFlightGroupDto[]>([]),error=ref(''),loading=ref(true),saving=ref(false)
function ints(text:string){return text.split(',').map(x=>Number(x.trim())).filter(x=>Number.isInteger(x)&&x>0)}
function text(values:number[]){return values.join(', ')}
function emptyFlight(order:number):TourFlightDto{return{OrderIndex:order,DepartureCountryIds:[],DepartureCityIds:[],ArrivalCountryIds:[],ArrivalCityIds:[],AirlineIds:[],FlightIds:[],DepartureAirportIds:[],ArrivalAirportIds:[],FareIds:[],MinConnectionMinutes:null,MaxConnectionMinutes:null}}
function addGroup(){groups.value.push({Name:'Новый вариант перелёта',Priority:groups.value.length+1,OrderIndex:groups.value.length+1,Flights:[]})}
function addFlight(group:TourFlightGroupDto){group.Flights.push(emptyFlight(group.Flights.length+1))}
function normalize(){groups.value.forEach((group,gi)=>{group.OrderIndex=gi+1;group.Flights.forEach((flight,fi)=>flight.OrderIndex=fi+1)})}
function move<T>(items:T[],index:number,delta:number){const next=index+delta;if(next<0||next>=items.length)return;const value=items[index];if(!value)return;items.splice(index,1);items.splice(next,0,value);normalize()}
async function load(){try{groups.value=await tourFlightsApi.get(props.tourId,props.service.Id)}catch(e){error.value=e instanceof Error?e.message:'Ошибка загрузки'}finally{loading.value=false}}
async function save(){normalize();saving.value=true;error.value='';try{groups.value=await tourFlightsApi.replace(props.tourId,props.service.Id,groups.value);emit('saved')}catch(e){error.value=e instanceof Error?e.message:'Ошибка сохранения'}finally{saving.value=false}}
onMounted(load)
</script>
<template><div class="modal-backdrop" @click.self="emit('close')"><section class="modal">
 <div class="modal-header"><div><p class="eyebrow">Услуга {{service.Name}}</p><h2>Авиаперелёт</h2><p>Альтернативные варианты и последовательные участки перелёта.</p></div><button class="secondary" @click="emit('close')">Закрыть</button></div>
 <div v-if="error" class="error-box">{{error}}</div><p v-if="loading">Загрузка…</p><template v-else>
  <div v-for="(group,gi) in groups" :key="group.Id??gi" class="group-card"><div class="group-heading"><div class="order-buttons"><button @click="move(groups,gi,-1)">↑</button><button @click="move(groups,gi,1)">↓</button></div><div class="field grow"><label>Название варианта</label><input v-model="group.Name" /></div><div class="field priority"><label>Приоритет</label><input v-model.number="group.Priority" type="number" /></div></div>
   <div v-for="(flight,fi) in group.Flights" :key="flight.Id??fi" class="segment"><div class="segment-title"><strong>Перелёт {{fi+1}}</strong><div class="order-buttons"><button @click="move(group.Flights,fi,-1)">↑</button><button @click="move(group.Flights,fi,1)">↓</button></div></div>
    <div class="form-grid">
     <div class="field"><label>Страны вылета</label><RemoteMultiLookup v-model="flight.DepartureCountryIds" endpoint="/api/v1/geography/countries" /></div><div class="field"><label>Города вылета</label><RemoteMultiLookup v-model="flight.DepartureCityIds" endpoint="/api/v1/geography/cities" filter-name="CountryId" :dependency-value="flight.DepartureCountryIds" /></div>
     <div class="field"><label>Страны прилёта</label><RemoteMultiLookup v-model="flight.ArrivalCountryIds" endpoint="/api/v1/geography/countries" /></div><div class="field"><label>Города прилёта</label><RemoteMultiLookup v-model="flight.ArrivalCityIds" endpoint="/api/v1/geography/cities" filter-name="CountryId" :dependency-value="flight.ArrivalCountryIds" /></div>
     <div class="field"><label>Авиакомпании</label><RemoteMultiLookup v-model="flight.AirlineIds" endpoint="/api/v1/avia/airlines" /></div><div class="field"><label>Рейсы / чартеры</label><RemoteMultiLookup v-model="flight.FlightIds" endpoint="/api/v1/avia/charters" /></div>
     <div class="field"><label>Аэропорты вылета</label><RemoteMultiLookup v-model="flight.DepartureAirportIds" endpoint="/api/v1/avia/airports" filter-name="CityId" :dependency-value="flight.DepartureCityIds" /></div><div class="field"><label>Аэропорты прилёта</label><RemoteMultiLookup v-model="flight.ArrivalAirportIds" endpoint="/api/v1/avia/airports" filter-name="CityId" :dependency-value="flight.ArrivalCityIds" /></div>
     <div class="field"><label>ID тарифов через запятую</label><input :value="text(flight.FareIds)" @input="flight.FareIds=ints(($event.target as HTMLInputElement).value)" /></div><div class="field"><label>Пересадка, минут</label><div class="connection"><input v-model.number="flight.MinConnectionMinutes" type="number" placeholder="Мин." /><input v-model.number="flight.MaxConnectionMinutes" type="number" placeholder="Макс." /></div></div>
    </div>
   </div><button class="secondary" @click="addFlight(group)">Добавить перелёт</button>
  </div><button class="secondary" @click="addGroup">Добавить вариант перелёта</button><div class="actions"><button class="primary" :disabled="saving" @click="save">{{saving?'Сохранение…':'Сохранить все перелёты'}}</button></div>
 </template>
</section></div></template>
<style scoped>.modal-backdrop{position:fixed;z-index:30;inset:0;background:#101a2a99;padding:30px;overflow:auto}.modal{max-width:1200px;margin:auto;background:#f8f9fb;border-radius:12px;padding:24px}.modal-header,.group-heading,.segment-title{display:flex;align-items:center;gap:12px;justify-content:space-between}.modal-header{align-items:flex-start}.modal-header h2{margin:3px 0}.modal-header p{margin:0}.grow{flex:1}.priority{width:130px}.order-buttons,.connection{display:flex;gap:6px}.order-buttons button{border:1px solid var(--border);background:#fff;border-radius:5px;padding:5px 8px}.segment-title{margin-bottom:12px}.connection input{width:50%}</style>
