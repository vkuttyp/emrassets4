<!-- eslint-disable vue/multi-word-component-names -->
<!-- eslint-disable no-unused-vars -->
<script setup>
import { storeToRefs } from 'pinia'
import uq from '@umalqura/core';
import { useAssetsStore, useAuthStore } from '@/stores'
const assetsStore = useAssetsStore()
const { userAssets, subordinates } = storeToRefs(assetsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)
authUser.value.subordinates = subordinates
authUser.value.deliveries = userAssets
// const subordinates = storeToRefs(assetsStore);
assetsStore.currentUserDeliveries()
assetsStore.getSubordinates()

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