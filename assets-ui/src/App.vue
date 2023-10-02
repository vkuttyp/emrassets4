<script setup>
import { RouterLink, RouterView } from 'vue-router';
import { ref } from 'vue'
import { useAuthStore } from '@/stores';

const authStore = useAuthStore();
const menuOpen=ref(false);
function toggleMenu(){
    return !menuOpen.value;
}
</script>

<!-- <template>
    <div class="app-container bg-light">
        <nav v-show="authStore.user" class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav">
                <RouterLink to="/" class="nav-item nav-link">Home</RouterLink>
                <a @click="authStore.logout()" class="nav-item nav-link">Logout</a>
            </div>
        </nav>
        <div class="container pt-4 pb-4">
            <RouterView />
        </div>
    </div>
</template> -->
<template>

<div id="app" class="font-sans text-gray-800">
      <header class="border-t-4 border-green-700 bg-white z-10 absolute w-full shadow-md">
        <nav class="container mx-auto px-8 flex flex-wrap justify-between items-center py-8">
          <div class="mb-0 lg:mb-6 xl:mb-0">
            <router-link :to="`/${$i18n.locale}`" class="font-bold text-xl flex items-end">
              <img src="@/assets/logo.png" alt="logo" class="w-10">
              <span>إمارة منطقة الباحة</span>
            </router-link>
          </div>
          <div class="block lg:hidden">
            <button @click="toggleMenu" class="flex items-center px-3 py-2 border rounded border-gray-500 hover:text-gray-600 hover:border-gray-600">
              <svg class="current-color h-3 w-3" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path d="M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z" fill="gray" /></svg>
            </button>
          </div>
          <ul
            class="tracking-wide font-bold w-full block flex-grow lg:flex lg:flex-end lg:w-auto items-center mt-8 lg:mt-0"
            :class="menuOpen ? 'block': 'hidden'"
          >
            <li class="mr-12 mb-6 lg:mb-0">
              <router-link :to="`/${$i18n.locale}/cars`" class="text-copy-primary hover:text-gray-600">{{ $t("cars") }}</router-link>
            </li>
            <li class="mr-12 mb-6 lg:mb-0">
              <router-link :to="`/${$i18n.locale}/logout`" class="text-copy-primary hover:text-gray-600">{{ $t("logout") }}</router-link>
            </li>
          </ul>
        </nav>
      </header>
      <div class="bg-gray-100 min-h-screen pt-40 text-lg">
        <router-view/>
      </div>
    </div>
  </template>
<style>
@import '@/assets/base.css';
</style>