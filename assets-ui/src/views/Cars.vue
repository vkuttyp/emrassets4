<script setup>
import { storeToRefs } from 'pinia'

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
// assetsStore.getByBeneficieries(userId.value);
// function getData(id){
//     assetsStore.getByBeneficieries(id);
// }
</script>

<template>
    <div class="container m-auto grid grid-cols-4 gap-1 text-white md:grid-cols-12">
  <header class="col-span-full bg-slate-600 p-4">{{ authUser.name }}</header>
  <aside v-if="authUser.deliveries.length" class="col-span-full bg-slate-600 p-4">
    <table class="table">
      <thead>
        <th>name</th>
        <th>Model</th>
        <th>Number Plate</th>
        <th>Date</th>
      </thead>
      <tbody>
        <tr v-for="delivery in authUser.deliveries" :key="delivery.id">
          <td>{{ delivery?.asset?.category?.name }}</td>
          <td>{{ delivery?.asset?.model }}</td>
          <td>{{ delivery?.asset?.numberPlate }}</td>
          <td>{{ delivery?.deliveryDate }}</td>
        </tr>
      </tbody>
    </table>
</aside>
<aside v-if="authUser.subordinates.length" class="col-span-full bg-slate-600 p-4">
<ul>
    <li v-for="emp in authUser.subordinates" :key="emp.id">
        <div v-if="emp.deliveries?.length">
            {{ emp.name }}
            <button @click="emp.collapsed=!emp.collapsed">
                {{emp.collapsed ? '⏪' : '⏬'}}
            </button>
            <table class="table" v-show="emp.collapsed">
      <thead>
        <th>name</th>
        <th>Model</th>
        <th>Number Plate</th>
        <th>Date</th>
      </thead>
      <tbody>
        <tr v-for="delivery in emp.deliveries" :key="delivery.id">
          <td>{{ delivery?.asset?.category?.name }}</td>
          <td>{{ delivery?.asset?.model }}</td>
          <td>{{ delivery?.asset?.numberPlate }}</td>
          <td>{{ delivery?.deliveryDate }}</td>
        </tr>
      </tbody>
    </table>
        </div>
        <div v-else>
            {{ emp.name }}
        </div>
    </li>
</ul>
</aside>
 
  <footer class="col-span-full bg-slate-600 p-4">Footer</footer>
</div>

  <div>
    <!-- <pre>{{ userAssets }}</pre> -->

    <div v-if="userAssets.loading" class="spinner-border spinner-border-sm"></div>
    <div v-if="userAssets.error" class="text-danger">
      Error loading Assets: {{ userAssets.error }}
    </div>
  </div>
  <!-- <pre>{{authUser}}</pre> -->
</template>