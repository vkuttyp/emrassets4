<!-- eslint-disable vue/multi-word-component-names -->
<!-- eslint-disable no-unused-vars -->
<script setup>
import { ref } from 'vue';
import Button from '@/components/Button.vue';
import Modal from '@/components/Modal.vue';
import { CarRequest } from '..';
import { storeToRefs } from 'pinia'
import uq from '@umalqura/core';
import { useCarsStore, useAuthStore } from '@/stores'
const carsStore = useCarsStore()
const { userCars, subordinates } = storeToRefs(carsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)
authUser.value.subordinates = subordinates
authUser.value.deliveries = userCars
// const subordinates = storeToRefs(assetsStore);
carsStore.currentUserDeliveries()
carsStore.getSubordinates()

const isOpen = ref(false);
const toggleModal = () => {
  isOpen.value = !isOpen.value;
};
const requestSaved = (data) => {
  isOpen.value=false
  console.log(data);
};
const requestError = (data) => {
  console.log(data);
};

</script>

<template src="./cars.html">
  
</template>
<style scoped>

/* https://hidde.blog/more-accessible-markup-with-display-contents/ */
body {
  font-family: 'Cairo';
}
.container  { 
  background-color: #EFEFFF;
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-gap: 1em;
}
  .container .item:nth-child(1) {
    grid-column: 1 / 2;
  }
  .container .item:nth-child(2) {
    grid-column: 2 / 3;
  }

.item {
  padding: 1rem;
}

.content {
  background-color: white;
  margin: 1em 0;
}

</style>