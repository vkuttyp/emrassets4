<template>
    <!-- <button @click="openModal" class="btn btn-secondary">Open Medal</button>

  <Modal2 :isOpen="isOpen" @closeModal="isOpen = false">
    <template #title> Sending Invoice ... </template>
    <template #content>
      <p class="text-sm text-gray-500">Invoide is on the way for  ...</p>
    </template>
  </Modal2> -->
   <suspense>
      <template #default>
        <Cars />
      </template>
      <template #fallback>
       <p>Loading</p>
      </template>
    </suspense>
</template>

<script setup>
// import { ref, defineAsyncComponent } from 'vue';
// const isOpen=ref(false);
// import {Modal2} from '@/components'
// // const Modal2 = defineAsyncComponent(() => import('@/components/aditya/refactored/Modal.vue'))
// function openModal() {
//   isOpen.value = true
// }
import {Cars} from '../'

</script>
