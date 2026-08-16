<script setup lang="ts">
import { onMounted,ref } from 'vue'
import type { TourHotelDto,TourHotelGroupDto,TourServiceDto } from '@/api/contracts'
import { tourHotelsApi } from '@/api/resources'
import RemoteLookup from '@/components/RemoteLookup.vue'
import RemoteMultiLookup from '@/components/RemoteMultiLookup.vue'
import NumberMultiSelect from '@/components/NumberMultiSelect.vue'
const props=defineProps<{tourId:number;service:TourServiceDto}>(),emit=defineEmits<{close:[];saved:[]}>()
const groups=ref<TourHotelGroupDto[]>([]),error=ref(''),loading=ref(true),saving=ref(false)
function addGroup(){groups.value.push({Name:'Новый вариант',Priority:groups.value.length+1,OrderIndex:groups.value.length+1,Hotels:[]})}
function addHotel(group:TourHotelGroupDto){group.Hotels.push({OrderIndex:group.Hotels.length+1,CountryId:0,CityIds:[],DurationsInNights:[],HotelIds:[]})}
function normalize(){groups.value.forEach((g,gi)=>{g.OrderIndex=gi+1;g.Hotels.forEach((h,hi)=>h.OrderIndex=hi+1)})}
function move<T>(items:T[],index:number,delta:number){const next=index+delta;if(next<0||next>=items.length)return;const value=items[index];if(!value)return;items.splice(index,1);items.splice(next,0,value);normalize()}
async function load(){try{groups.value=await tourHotelsApi.get(props.tourId,props.service.Id)}catch(e){error.value=e instanceof Error?e.message:'Ошибка загрузки'}finally{loading.value=false}}
async function save(){normalize();saving.value=true;error.value='';try{groups.value=await tourHotelsApi.replace(props.tourId,props.service.Id,groups.value);emit('saved')}catch(e){error.value=e instanceof Error?e.message:'Ошибка сохранения'}finally{saving.value=false}}
onMounted(load)
</script>
<template>
 <div class="modal-backdrop" @click.self="emit('close')"><section class="modal">
  <div class="modal-header"><div><p class="eyebrow">Услуга {{service.Name}}</p><h2>Проживание</h2><p>Альтернативные группы и последовательные участки проживания.</p></div><button class="secondary" @click="emit('close')">Закрыть</button></div>
  <div v-if="error" class="error-box">{{error}}</div><p v-if="loading">Загрузка…</p>
  <template v-else>
   <div v-for="(group,gi) in groups" :key="group.Id??gi" class="group-card">
    <div class="group-heading"><div class="order-buttons"><button @click="move(groups,gi,-1)">↑</button><button @click="move(groups,gi,1)">↓</button></div><div class="field grow"><label>Название варианта</label><input v-model="group.Name" /></div><div class="field priority"><label>Приоритет</label><input v-model.number="group.Priority" type="number" /></div></div>
    <div v-for="(hotel,hi) in group.Hotels" :key="hotel.Id??hi" class="segment">
     <div class="segment-title"><strong>Участок {{hi+1}}</strong><div class="order-buttons"><button @click="move(group.Hotels,hi,-1)">↑</button><button @click="move(group.Hotels,hi,1)">↓</button></div></div>
     <div class="form-grid">
      <div class="field"><label>Страна</label><RemoteLookup v-model="hotel.CountryId" endpoint="/api/v1/geography/countries" :nullable="false" /></div>
      <div class="field"><label>Города</label><RemoteMultiLookup v-model="hotel.CityIds" endpoint="/api/v1/geography/cities" filter-name="CountryId" :dependency-value="hotel.CountryId" /></div>
      <div class="field"><label>Продолжительности, ночей</label><NumberMultiSelect v-model="hotel.DurationsInNights" :min="1" :max="40" /></div>
      <div class="field"><label>Разрешённые отели</label><RemoteMultiLookup v-model="hotel.HotelIds" endpoint="/api/v1/hotels" filter-name="CityId" :dependency-value="hotel.CityIds" /></div>
     </div>
    </div>
    <button class="secondary" @click="addHotel(group)">Добавить участок</button>
   </div>
   <button class="secondary" @click="addGroup">Добавить вариант проживания</button>
   <div class="actions"><button class="primary" :disabled="saving" @click="save">{{saving?'Сохранение…':'Сохранить всё проживание'}}</button></div>
  </template>
 </section></div>
</template>
<style scoped>.modal-backdrop{position:fixed;z-index:30;inset:0;background:#101a2a99;padding:30px;overflow:auto}.modal{max-width:1100px;margin:auto;background:#f8f9fb;border-radius:12px;padding:24px;box-shadow:0 20px 60px #0004}.modal-header,.group-heading,.segment-title{display:flex;align-items:center;gap:12px;justify-content:space-between}.modal-header{align-items:flex-start}.modal-header h2{margin:3px 0}.modal-header p{margin:0}.grow{flex:1}.priority{width:130px}.order-buttons{display:flex;gap:4px}.order-buttons button{border:1px solid var(--border);background:#fff;border-radius:5px;padding:5px 8px}.segment-title{margin-bottom:12px}</style>
