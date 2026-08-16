<script setup lang="ts">
import SmartLookup from './SmartLookup.vue'
withDefaults(defineProps<{modelValue:string|number|boolean|null|undefined;endpoint:string;valueKey?:string;labelKey?:string;filterName?:string;dependencyValue?:string|number|boolean|null;nullable?:boolean;placeholder?:string}>(),{valueKey:'Id',labelKey:'Name',nullable:true})
const emit=defineEmits<{'update:modelValue':[value:string|number|null]}>()
</script>
<template><SmartLookup :model-value="modelValue" :endpoint="endpoint" :value-key="valueKey" :label-key="labelKey" :filter-name="filterName" :dependency-value="dependencyValue" :nullable="nullable" :placeholder="placeholder" @update:model-value="value=>emit('update:modelValue',Array.isArray(value)?null:value)" /></template>
