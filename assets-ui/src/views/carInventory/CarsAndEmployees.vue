<script setup>
import { storeToRefs } from 'pinia'
import { ref } from 'vue';
import { useCarsStore } from '@/stores'
import uq from '@umalqura/core';
const carsStore = useCarsStore()
const { carsAndEmployees } = storeToRefs(carsStore)

carsStore.getCarsAndEmployees();
</script>
<template>
   <div>
  <strong>{{$t("carReport.title")}}:</strong>
 
     <table class=" text-sm">
       <thead>
        <th>#</th>
        <th>{{$t("carReport.carType")}}</th>
        <th>{{$t("carReport.carName")}}</th>
        <th>{{$t("carReport.numberPlate")}}</th>
        <th>{{$t("carReport.model")}}</th>
         <th>{{$t("carReport.deliveryDate")}}</th>
        
    <th>{{$t("carReport.employeeName")}}</th>
       </thead>
       <tbody>
         <tr v-for="(car,index) in carsAndEmployees" :key="car.id">
            <td>{{ index + 1 }}</td>
            <td>{{ car.carType?.name }}</td>
            <td>{{ car.category?.name }}</td>
            <td>{{ car.numberPlate }}</td>
            <td>{{ car.model }}</td>
           <td>{{ uq(car.lastDelivery?.deliveryDate).format('yyyy/MM/dd') }}</td>
           <td>{{ car.lastDelivery?.user?.name }}</td>
          
         </tr>
       </tbody>
     </table>
   
</div>
    
</template>