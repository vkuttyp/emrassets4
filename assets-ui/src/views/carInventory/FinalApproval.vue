<script setup>
import { storeToRefs } from 'pinia'
import { ref } from 'vue';
import { useCarsStore, useAuthStore } from '@/stores'
import uq from '@umalqura/core';

const carsStore = useCarsStore()
const { votingIncomple, votingCompleted, responseTypes } = storeToRefs(carsStore)

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)

carsStore.getCarVotingPendingList();

</script>

<template src="./finalApproval.html"></template>