<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { toursApi } from '@/api/resources'
import type { TourListItemDto } from '@/api/contracts'

const router=useRouter()
const items=ref<TourListItemDto[]>([])
const total=ref(0), loading=ref(false), error=ref('')
const filters=reactive({search:'',status:'',currencyId:'',page:1,pageSize:20})
const statuses:Record<number,string>={1:'Новый',2:'Подготовлен',3:'В продаже',4:'Скрыт'}
async function load(){
 loading.value=true;error.value=''
 try{
  const data=await toursApi.list({Search:filters.search||undefined,Status:filters.status||undefined,CurrencyId:filters.currencyId||undefined,page:filters.page,pageSize:filters.pageSize})
  items.value=data.Items;total.value=data.TotalCount
 }catch(e){error.value=e instanceof Error?e.message:'Не удалось загрузить туры.'}finally{loading.value=false}
}
function search(){filters.page=1;void load()}
onMounted(load)
</script>
<template>
 <section>
  <div class="page-heading"><div><p class="eyebrow">Продукт</p><h2>Туры</h2><p>Шаблоны туров и их состав.</p></div><button class="primary" @click="router.push('/tours/new')">Создать тур</button></div>
  <div v-if="error" class="error-box">{{error}}</div>
  <div class="panel">
   <form class="toolbar" @submit.prevent="search">
    <input v-model="filters.search" class="search" placeholder="Поиск по названию" />
    <select v-model="filters.status"><option value="">Все статусы</option><option v-for="(name,id) in statuses" :key="id" :value="id">{{name}}</option></select>
    <input v-model="filters.currencyId" type="number" placeholder="ID валюты" />
    <button class="secondary">Найти</button>
   </form>
   <div class="table-wrap">
    <table><thead><tr><th>ID</th><th>Название</th><th>Статус</th><th>Валюта</th><th>Доступность</th><th>Revision</th></tr></thead>
    <tbody>
     <tr v-if="loading"><td colspan="6" class="muted">Загрузка…</td></tr>
     <tr v-else-if="!items.length"><td colspan="6" class="muted">Туры не найдены.</td></tr>
     <tr v-for="tour in items" :key="tour.Id" class="clickable" @click="router.push('/tours/'+tour.Id)">
      <td>{{tour.Id}}</td><td><strong>{{tour.Name}}</strong></td><td><span class="badge">{{statuses[tour.Status]??tour.Status}}</span></td><td>{{tour.CurrencyId}}</td>
      <td>{{tour.DateMode===1?(tour.AvailableDateFrom?.slice(0,10)+' — '+tour.AvailableDateTo?.slice(0,10)):'Отдельные даты'}}</td><td class="revision">r{{tour.Revision}}</td>
     </tr>
    </tbody></table>
   </div>
   <div class="pager"><span class="muted">Всего: {{total}}</span><div><button class="secondary" :disabled="filters.page<=1" @click="filters.page--;load()">Назад</button> <button class="secondary" :disabled="filters.page*filters.pageSize>=total" @click="filters.page++;load()">Далее</button></div></div>
  </div>
 </section>
</template>
<style scoped>.clickable{cursor:pointer}.clickable:hover{background:#f7f9fc}.toolbar select,.toolbar input[type=number]{border:1px solid #cfd7e2;border-radius:7px;padding:10px}</style>
