<script setup lang="ts">
import { computed,onMounted,reactive,ref } from 'vue'
import { useRoute,useRouter } from 'vue-router'
import { toursApi } from '@/api/resources'
import type { TourCommand } from '@/api/contracts'
import TourServicesEditor from './TourServicesEditor.vue'
import RemoteLookup from '@/components/RemoteLookup.vue'
import RemoteMultiLookup from '@/components/RemoteMultiLookup.vue'

const route=useRoute(),router=useRouter()
const id=computed(()=>Number(route.params.id)||0),isNew=computed(()=>!id.value)
const tab=ref<'base'|'services'>('base'),loading=ref(false),saving=ref(false),error=ref(''),revision=ref<number>()
const form=reactive<TourCommand>({Name:'',CurrencyId:0,DateMode:1,AvailableDateFrom:null,AvailableDateTo:null,AvailableDates:[],TourTypeIds:[],DescriptionUrl:'',DescriptionHtml:'',BookingConditionsHtml:'',Status:1})
const datesText=ref('')
async function load(){
 if(isNew.value)return
 loading.value=true
 try{const tour=await toursApi.get(id.value);Object.assign(form,{Name:tour.Name,CurrencyId:tour.CurrencyId,DateMode:tour.DateMode,AvailableDateFrom:tour.AvailableDateFrom?.slice(0,10)??null,AvailableDateTo:tour.AvailableDateTo?.slice(0,10)??null,AvailableDates:tour.AvailableDates,TourTypeIds:tour.TourTypes.map(x=>x.Id),DescriptionUrl:tour.DescriptionUrl??'',DescriptionHtml:tour.DescriptionHtml??'',BookingConditionsHtml:tour.BookingConditionsHtml??'',Status:tour.Status});datesText.value=tour.AvailableDates.map(x=>x.slice(0,10)).join(', ');revision.value=tour.Revision}catch(e){error.value=e instanceof Error?e.message:'Ошибка загрузки'}finally{loading.value=false}
}
async function save(){
 error.value='';saving.value=true
 form.AvailableDates=form.DateMode===2?datesText.value.split(',').map(x=>x.trim()).filter(Boolean):[]
 try{const result=isNew.value?await toursApi.create(form):await toursApi.update(id.value,form);revision.value=result.Revision;if(isNew.value)await router.replace('/tours/'+result.Id)}catch(e){error.value=e instanceof Error?e.message:'Ошибка сохранения'}finally{saving.value=false}
}
onMounted(load)
</script>
<template>
 <section>
  <div class="page-heading"><div><p class="eyebrow">Туры / {{isNew?'Создание':'Редактирование'}}</p><h2>{{form.Name||'Новый тур'}}</h2><p v-if="!isNew">ID {{id}} · <span class="revision">Revision r{{revision}}</span></p></div><button class="secondary" @click="router.push('/tours')">К списку</button></div>
  <div v-if="error" class="error-box">{{error}}</div><div v-if="loading" class="panel empty-panel">Загрузка…</div>
  <div v-else class="panel" style="padding:22px">
   <div class="tabs"><button class="tab" :class="{active:tab==='base'}" @click="tab='base'">Основные данные</button><button class="tab" :class="{active:tab==='services'}" :disabled="isNew" @click="tab='services'">Состав тура</button></div>
   <form v-if="tab==='base'" @submit.prevent="save">
    <div class="form-grid">
     <div class="field wide"><label>Название</label><input v-model="form.Name" required maxlength="256" /></div>
     <div class="field"><label>Валюта</label><RemoteLookup v-model="form.CurrencyId" endpoint="/api/v1/finance/rates" :nullable="false" /></div>
     <div v-if="!isNew" class="field"><label>Статус</label><select v-model.number="form.Status"><option :value="1">Новый</option><option :value="2">Подготовлен</option><option :value="3">В продаже</option><option :value="4">Скрыт</option></select></div>
     <div class="field"><label>Режим дат</label><select v-model.number="form.DateMode"><option :value="1">Диапазон</option><option :value="2">Отдельные даты</option></select></div>
     <template v-if="form.DateMode===1"><div class="field"><label>Дата с</label><input v-model="form.AvailableDateFrom" type="date" /></div><div class="field"><label>Дата по</label><input v-model="form.AvailableDateTo" type="date" /></div></template>
     <div v-else class="field wide"><label>Даты через запятую</label><input v-model="datesText" placeholder="2026-09-01, 2026-09-08" /></div>
     <div class="field wide"><label>Типы тура</label><RemoteMultiLookup v-model="form.TourTypeIds" endpoint="/api/v1/tours/tour-types" /></div>
     <div class="field wide"><label>URL описания</label><input v-model="form.DescriptionUrl" /></div>
     <div class="field wide"><label>HTML описания</label><textarea v-model="form.DescriptionHtml"></textarea></div>
     <div class="field wide"><label>HTML условий бронирования</label><textarea v-model="form.BookingConditionsHtml"></textarea></div>
    </div>
    <div class="actions"><button class="primary" :disabled="saving">{{saving?'Сохранение…':isNew?'Создать тур':'Сохранить'}}</button></div>
   </form>
   <TourServicesEditor v-else :tour-id="id" @tour-changed="load" />
  </div>
 </section>
</template>
