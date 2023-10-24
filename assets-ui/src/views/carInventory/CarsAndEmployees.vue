<script setup>
import { storeToRefs } from 'pinia'
import { ref, computed } from 'vue';
import { useCarsStore } from '@/stores'
import SearchInput from '@/components/SearchInput.vue';
import uq from '@umalqura/core';
const carsStore = useCarsStore()
const { carsAndEmployees } = storeToRefs(carsStore)

carsStore.getCarsAndEmployees();
const numberPlate = ref('');
const carModel = ref('');
const idNo = ref('');
const getCars = computed(() => {
    if(Array.isArray(carsAndEmployees.value)){
      return carsAndEmployees.value.filter((car)=> car.numberPlate.includes(numberPlate.value) 
      && car.model.includes(carModel.value)
      && car.lastDelivery?.user?.idNo.includes(idNo.value));
    } else return [];
    });
  
</script>
<template>
  <div>
    <div class="flex items-center justify-center">
      <h1>{{$t("carReport.title")}}</h1>
 
    </div>
  <div class="flex justify-center gap-x-4 my-3">
     
  <SearchInput v-model="carModel" :placeholder="$t('decision.carModel')" />
  <!-- <input type="text" v-model="idNo" > -->
  <SearchInput v-model="idNo" :placeholder="$t('carReport.idNo')"/>

<SearchInput  v-model="numberPlate" :placeholder="$t('decision.numberPlate')" />

  </div>
  </div>
   <div class="mx-5 min-h-screen flex flex-col">
     <table class=" text-sm">
       <thead>
        <th>#</th>
        <th>{{$t("carReport.carType")}}</th>
        <th>{{$t("carReport.carName")}}</th>
        <th>{{$t("carReport.numberPlate")}}</th>
        <th>{{$t("carReport.model")}}</th>
         <th>{{$t("carReport.deliveryDate")}}</th>
        <th>{{$t("carReport.idNo")}}</th>
    <th>{{$t("carReport.employeeName")}}</th>
       </thead>
       <tbody>
         <tr v-for="(car,index) in getCars" :key="car.id">
            <td>{{ index + 1 }}</td>
            <td>{{ car.carType?.name }}</td>
            <td>{{ car.category?.name }}</td>
            <td>{{ car.numberPlate }}</td>
            <td>{{ car.model }}</td>
           <td>{{ uq(car.lastDelivery?.deliveryDate).format('yyyy/MM/dd') }}</td>
           <td>{{ car.lastDelivery?.user?.idNo }}</td>
           <td>{{ car.lastDelivery?.user?.name }}</td>
          
         </tr>
       </tbody>
     </table>
   
</div>
    
</template>
<style scoped>
input[type="search"] {
  border: none;
  background: transparent;
  margin: 0;
  padding: 7px 8px;
  font-size: 14px;
  color: inherit;
  border: 1px solid transparent;
  border-radius: inherit;
}

input[type="search"]::placeholder {
  color: #bbb;
}
</style>