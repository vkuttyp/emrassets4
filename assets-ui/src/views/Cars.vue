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
  <!-- <pre>{{ subordinates }}</pre> -->
  <div v-if="authUser">
    <p>Name: {{ authUser.name }}</p>
    <button>Cars</button>
    <table>
      <thead>
        <th>name</th>
        <th>Model</th>
        <th>Number Plate</th>
        <th>Date</th>
      </thead>
      <tbody>
        <tr v-for="delivery in authUser.deliveries" :key="delivery.id">
          <td>{{ delivery.asset.category.name }}</td>
          <td>{{ delivery?.asset?.model }}</td>
          <td>{{ delivery?.asset?.numberPlate }}</td>
          <td>{{ delivery?.deliveryDate }}</td>
        </tr>
      </tbody>
    </table>
    <div v-if="subordinates?.length">
      <h2>Subordinates</h2>
      <table>
        <thead>
          <th>name</th>
          <th>idnumber</th>
          <th>Date</th>
        </thead>
        <tbody>
          <tr v-for="sub in subordinates" :key="sub.id">
            <td>{{ sub.name }}</td>
            <td>{{ sub.idNo }}</td>

            <!-- <td><pre>{{ sub.deliveries }}</pre></td> -->
            
            <td v-for="delivery in sub.deliveries" :key="delivery.id">
              <table>
                <thead>
                  <th>name</th>
                  <th>Model</th>
                  <th>Number Plate</th>
                  <th>Date</th>
                </thead>
                <tbody>
                  <tr v-for="delivery in sub.deliveries" :key="delivery.id">
                    <td>{{ delivery.asset.category.name }}</td>
                    <td>{{ delivery?.asset?.model }}</td>
                    <td>{{ delivery?.asset?.numberPlate }}</td>
                    <td>{{ delivery?.deliveryDate }}</td>
                  </tr>
                </tbody>
              </table>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

  <div v-if="userAssets?.length">
    <!-- <pre>{{ userAssets }}</pre> -->

    <div v-if="userAssets.loading" class="spinner-border spinner-border-sm"></div>
    <div v-if="userAssets.error" class="text-danger">
      Error loading Assets: {{ userAssets.error }}
    </div>
  </div>
  <!-- <pre>{{authUser}}</pre> -->
</template>
