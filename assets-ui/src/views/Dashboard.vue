<template>
    <div>
        <h1>Hi {{authUser?.name}}!</h1>
       <pre>{{ authUser }}</pre>
        <!-- <ul v-if="users.length">
            <li v-for="user in users" :key="user.temperatureF">{{user.date}} {{user.summary}}</li>
        </ul>
        <div v-if="users.loading" class="spinner-border spinner-border-sm"></div>
        <div v-if="users.error" class="text-danger">Error loading users: {{users.error}}</div> -->
    </div>
</template>

<script setup>
import { storeToRefs } from 'pinia';

import { useAuthStore, useUsersStore } from '@/stores';

const authStore = useAuthStore();
const { user: authUser } = storeToRefs(authStore);

const usersStore = useUsersStore();
const { users } = storeToRefs(usersStore);
usersStore.getAll();
</script>
