<script setup>
import { storeToRefs } from 'pinia'
import { ref } from 'vue';
import { useCarsStore, useAuthStore } from '@/stores'
import i18n from '../../locales/i18';
import uq from '@umalqura/core';
import VoteModal from './VoteModal.vue'

const carsStore = useCarsStore()
const { carVotingPendingList, responseTypes } = storeToRefs(carsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)

carsStore.getCarVotingPendingList();

const managerResponseId = ref('')
const voting = ref(null);
const isOpen = ref(false);

function votingClicked(pending) {
  
  if(pending.carVotingDetail===undefined || pending.carVotingDetail===null){
    pending.carVotingDetail= {
      managerResponseId: pending.id,
      responseTypeId: 0,
      voteCount: 0,
      notes: ''
    }
  }
 voting.value = pending.carVotingDetail;
  managerResponseId.value=pending.id;
  isOpen.value = !isOpen.value;
}
const toggleModal = () => {
  isOpen.value = !isOpen.value;
};
const requestSaved = (data) => {
  isOpen.value=false;
  voting.value=data;
//   if(Array.isArray(subordinateCarRequests.value) && data) {
//   subordinateCarRequests.value.forEach(emp => {
//   emp.carRequests.find((request)=> request.id===data.requestId).carManagerResponse = data;
//  });
// console.log(data);
  
};
const requestModalClosed = ()=> {
  isOpen.value=false;
}
const requestError = (data) => {
  console.log(data);
};
function getRequestCaption(request){
    // console.log(request);
    return 'oook';
//   if(request.carManagerResponse.managerResponseType?.id===0){
//     return i18n.global.t('carRequest.pending');
//   } else return request.carManagerResponse.managerResponseType?.name;
}
function requestStatusStyle(request){
    return 'text-gray-500';
//   switch(request.carManagerResponse.managerResponseType?.id){
//     case 0: return 'text-gray-500';
//     case 1: return 'text-green-500';
//     case 2: return 'text-red-500';
//     default: return 'text-bluee-500';
//   }
}
</script>
<template src="./pendingList.html">

</template>