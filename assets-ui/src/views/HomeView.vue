<script setup>
import { storeToRefs } from 'pinia';

import { useAuthStore, useUsersStore } from '@/stores';

const authStore = useAuthStore();
const { user: authUser } = storeToRefs(authStore);

const usersStore = useUsersStore();
const { users } = storeToRefs(usersStore);
usersStore.getAll();
</script>

<template>
    <div>
        <h1>Hi {{authUser?.name}}!</h1>
        <p>You're logged in with Vue 3 + Pinia & JWT!!</p>
        <h3>Users from secure api end point:</h3>
        <!-- <font-awesome-icon icon="fa-solid fa-user-secret" /> -->
        <a href=""><i class="fa fa-home fa-lg"></i>Home</a>
        <button class="icon-before icon-chevron-right">
<label><i class="fa-solid fa-user"></i></label>
    <!-- Label -->
</button>
        <ul v-if="users.length">
            <li v-for="user in users" :key="user.temperatureF">{{user.date}} {{user.summary}}</li>
        </ul>
        <div v-if="users.loading" class="spinner-border spinner-border-sm"></div>
        <div v-if="users.error" class="text-danger">Error loading users: {{users.error}}</div>
    </div>
</template>
