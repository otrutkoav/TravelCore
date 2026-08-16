import { createRouter, createWebHashHistory } from 'vue-router'
import DashboardView from '@/modules/dashboard/DashboardView.vue'
import CatalogView from '@/modules/catalogs/CatalogView.vue'
import ToursListView from '@/modules/tours/ToursListView.vue'
import TourEditorView from '@/modules/tours/TourEditorView.vue'

export const router=createRouter({
 history:createWebHashHistory(),
 routes:[
  {path:'/',component:DashboardView,meta:{title:'Главная'}},
  {path:'/tours',component:ToursListView,meta:{title:'Туры'}},
  {path:'/tours/new',component:TourEditorView,meta:{title:'Новый тур'}},
  {path:'/tours/:id(\\d+)',component:TourEditorView,meta:{title:'Редактор тура'}},
  {path:'/catalog/:resource',component:CatalogView,meta:{title:'Справочник'}},
  {path:'/:pathMatch(.*)*',redirect:'/'},
 ],
})
