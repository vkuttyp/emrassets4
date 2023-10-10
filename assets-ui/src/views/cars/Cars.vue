<!-- eslint-disable vue/multi-word-component-names -->
<!-- eslint-disable no-unused-vars -->
<script setup>
import { ref, watch } from 'vue';
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

const carRequests = ref([]);
async function getCarRequests() {
    return await carsStore.carRequestDetailsByBeneficiary(authStore.user.id)
        .then(data => {
            if(data.error) {
            // apiError.value=data.error;
            // setErrors({ apiError: data.error });
            console.log(data.error)
            }
            else{
            carRequests.value=data;
            }
        })
        .catch(error => {
          console.log(error);
          // setErrors({ apiError: error });
        });
}

await getCarRequests();
</script>

<template src="./cars.html">
  
</template>
