<script setup lang="ts">
import { computed, ref } from 'vue'
import { RouterLink, RouterView, useRoute } from 'vue-router'
import { navigation } from '@/app/navigation'

const route=useRoute()
const menuOpen=ref(false)
const title=computed(()=>String(route.meta.title??'TourCore Admin'))
const environment=window.__TOURCORE_CONFIG__?.environmentName??'Environment'
</script>

<template>
 <div class="app-shell">
  <aside class="sidebar" :class="{open:menuOpen}">
   <div class="brand"><span class="brand-mark">TC</span><div><strong>TourCore</strong><small>Administration</small></div></div>
   <nav class="navigation">
    <template v-for="section in navigation" :key="section.label">
     <RouterLink v-if="section.to" :to="section.to" class="nav-link">{{section.label}}</RouterLink>
     <section v-else class="nav-section">
      <h2>{{section.label}}</h2>
      <RouterLink v-for="child in section.children" :key="child.label" :to="child.to!" class="nav-link">{{child.label}}</RouterLink>
     </section>
    </template>
   </nav>
  </aside>
  <div class="workspace">
   <header class="topbar">
    <button class="menu-button" @click="menuOpen=!menuOpen">☰</button>
    <h1>{{title}}</h1>
    <span class="environment"><i></i>{{environment}}</span>
   </header>
   <main class="content"><RouterView /></main>
  </div>
 </div>
</template>
