<script setup lang="ts">
import SmartLookup from './SmartLookup.vue'
withDefaults(defineProps<{modelValue:number[];endpoint:string;filterName?:string;dependencyValue?:number|number[]|null;placeholder?:string}>(),{})
const emit=defineEmits<{'update:modelValue':[value:number[]]}>()
</script>
<template><SmartLookup multiple :model-value="modelValue" :endpoint="endpoint" :filter-name="filterName" :dependency-value="dependencyValue" :placeholder="placeholder" @update:model-value="value=>emit('update:modelValue',Array.isArray(value)?value.map(Number):[])" /></template>
