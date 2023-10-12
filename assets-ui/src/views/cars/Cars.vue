<!-- eslint-disable vue/multi-word-component-names -->
<!-- eslint-disable no-unused-vars -->
<script setup>
import { ref, reactive } from 'vue';
import Button from '@/components/Button.vue';
import Modal from '@/components/Modal.vue';
import { CarRequest, ManagerResponse } from '..';
import { storeToRefs } from 'pinia'
import uq from '@umalqura/core';
import { useCarsStore, useAuthStore } from '@/stores'
const carsStore = useCarsStore()
const { userCars, subordinates, subordinateCarRequests, responseTypes } = storeToRefs(carsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)
authUser.value.subordinates = subordinates
authUser.value.carDeliveries = userCars
// const subordinates = storeToRefs(assetsStore);
carsStore.currentUserDeliveries()
carsStore.getSubordinates()
carsStore.getResponseTypes(1);
// watch(authStore.user, (currentValue, oldValue) => {
//       console.log(currentValue);
//       console.log(oldValue);
//     });
const isOpen = ref(false);
const toggleModal = () => {
  isOpen.value = !isOpen.value;
};
const requestSaved = (data) => {
  isOpen.value=false
 carRequests.value.push(data);
};
const requestModalClosed = ()=> {
  isOpen.value=false;
}
const requestError = (data) => {
  console.log(data);
};
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
const carRequests = ref([]);
async function getCarRequests() {
    return await carsStore.carRequestDetailsByBeneficiary(authStore.user.id)
        .then(data => {
            if(data.error) {
            // apiError.value=data.error;
            // setErrors({ apiError: data.error });
            // console.log(data.error)
            }
            else{
            carRequests.value=data;
            }
        })
        .catch(error => {
          // console.log(error);
          // setErrors({ apiError: error });
        });
}

await getCarRequests();
</script>

<template src="./cars.html">
  
</template>
