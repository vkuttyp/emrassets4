<script setup>
import { ref, watchEffect } from 'vue';
import i18n from '../../locales/i18';
import { CarRequest, ManagerResponse } from '..';
import { storeToRefs } from 'pinia'
import uq from '@umalqura/core';
import { useCarsStore, useAuthStore } from '@/stores'
const carsStore = useCarsStore()
const { userCars, subordinates, subordinateCarRequests, responseTypes, carRequests } = storeToRefs(carsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)
authUser.value.subordinates = subordinates
authUser.value.carDeliveries = userCars
// const subordinates = storeToRefs(assetsStore);
carsStore.currentUserDeliveries()
carsStore.getSubordinates()
carsStore.getResponseTypes();
function openNewRequest(){
  request.value=null;
  isOpen.value=true;
}
const isOpen = ref(false);
const toggleModal = () => {
  isOpen.value = !isOpen.value;
};

const requestSaved = (data) => {
  isOpen.value=false;
  const index = carRequests.value.findIndex((e) => e.id === data.id);
    if (index === -1) {
      carRequests.value.unshift(data);
    } else {
        carRequests.value[index] = data;
    }
};

const requestModalClosed = ()=> {
  isOpen.value=false;
}
const requestError = (data) => {
  console.log(data);
};

const request = ref(null)
function requestClicked(r){
  request.value=r;
  isOpen.value=true;
}

const requestId = ref(null)
const carManagerResponse = ref(null);
const isOpen2 = ref(false);
function approveClicked(request, event) {
 carManagerResponse.value = request.carManagerResponse;
  requestId.value=request.id;
  isOpen2.value = !isOpen2.value;
}
const toggleModal2 = () => {
  isOpen2.value = !isOpen2.value;
};
const requestSaved2 = (data) => {
  isOpen2.value=false;
  if(Array.isArray(subordinateCarRequests.value) && data) {
  subordinateCarRequests.value.forEach(emp => {
  emp.carRequests.find((request)=> request.id===data.requestId).carManagerResponse = data;
 });
  }
};
const requestModalClosed2 = ()=> {
  isOpen2.value=false;
}
const requestError2 = (data) => {
  console.log(data);
};
function getRequestCaption(request){
  if(request.carManagerResponse?.managerResponseType?.id===0){
    return i18n.global.t('carRequest.pending');
  } else return request.carManagerResponse?.managerResponseType?.name;
}
function requestStatusStyle(request){
  switch(request.carManagerResponse?.managerResponseType?.id){
    case 0: return 'text-gray-500';
    case 1: return 'text-green-500';
    case 2: return 'text-red-500';
    default: return 'text-bluee-500';
  }
}
watchEffect(() => {
  carsStore.carRequestDetailsByBeneficiary(authStore.user.id);
});
</script>

<template src="./cars.html">
  
</template>
