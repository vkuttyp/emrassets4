<script setup>
import { useAuthStore } from '../stores/auth.store';
import { tryOnMounted } from '@vueuse/core';
const authStore = useAuthStore();
// mounted(){
//     authStore.logout();
// }
// eslint-disable-next-line no-undef
tryOnMounted(() => {
    authStore.logout();
});
</script>
<template>
    <h3>You have been logout successfully</h3>
</template>