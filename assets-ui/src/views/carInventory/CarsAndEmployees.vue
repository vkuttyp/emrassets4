<script setup>
import { storeToRefs } from 'pinia'
import { ref, computed } from 'vue';
import { useCarsStore } from '@/stores'
import uq from '@umalqura/core';
const carsStore = useCarsStore()
const { carsAndEmployees } = storeToRefs(carsStore)

carsStore.getCarsAndEmployees();
const numberPlate = ref('');
const carModel = ref('');
const idNo = ref('');
const getCars = computed(() => {
    
      return carsAndEmployees.value.filter((car)=> car.numberPlate.includes(numberPlate.value) 
      && car.model.includes(carModel.value)
      && car.lastDelivery?.user?.idNo.includes(idNo.value));
    });

</script>
<template>
   <div>
  <strong>{{$t("carReport.title")}}:</strong>
  <input type="text" v-model="carModel" :placeholder="$t('decision.carModel')">
  <input type="text" v-model="idNo" >

<input type="text" v-model="numberPlate" :placeholder="$t('decision.numberPlate')">
     <table class=" text-sm mr-20">
       <thead>
        <th>#</th>
        <th>{{$t("carReport.carType")}}</th>
        <th>{{$t("carReport.carName")}}</th>
        <th>{{$t("carReport.numberPlate")}}</th>
        <th>{{$t("carReport.model")}}</th>
         <th>{{$t("carReport.deliveryDate")}}</th>
        <th>ID</th>
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