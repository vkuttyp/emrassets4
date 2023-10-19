<script setup>
import { storeToRefs } from 'pinia'
import { ref } from 'vue';
import { useCarsStore, useAuthStore } from '@/stores'
import uq from '@umalqura/core';
import FinalDecisionModal from './FinalDecisionModal.vue';
const carsStore = useCarsStore()
const { votingIncomple, votingCompleted, responseTypes } = storeToRefs(carsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)

carsStore.getCarVotingPendingList();

const decisionsList = ref([]);
const decisionModalOpen = ref(false);
const managerResponseId=ref(0);
const selectedDecision = ref(null);
const toggleModal = () => {
    decisionModalOpen.value = !decisionModalOpen.value;
};

const decisionSaved = (data) => {
    decisionModalOpen.value=false;
  const index = decisionsList.value.findIndex((e) => e.id === data.id);
    if (index === -1) {
        decisionsList.value.unshift(data);
    } else {
        decisionsList.value[index] = data;
    }
};

const decisionModalClosed = ()=> {
    decisionModalOpen.value=false;
}
const decisionModaltError = (data) => {
  console.log(data);
};

function newDecisionClicked(r){
    managerResponseId.value=r.id;
    selectedDecision.value = {
        managerResponseId: r.id,
        carId: 0,
        responseTypeId: 0,
        notes: '',
        responseType: {id: 0, name: ''}
    }
// selectedDecision.value=r;
  decisionModalOpen.value=true;
}
</script>

<template src="./finalApproval.html"></template>