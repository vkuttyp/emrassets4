<script setup>
import { RouterLink, RouterView } from 'vue-router';
import { ref } from 'vue'
import { useAuthStore } from '@/stores';
import { storeToRefs } from 'pinia'
import LanguageSwitcher from '@/components/LanguageSwitcher.vue';
import DropdownMenu from "@/components/DropdownMenu.vue"
const authStore = useAuthStore();
const { user: authUser } = storeToRefs(authStore)
const menuOpen=ref(false);
function toggleMenu(){
    menuOpen.value = !menuOpen.value;
}

</script>
<template>
<div id="app" class="font-Aljazeera text-gray-800" :dir="$i18n.locale=='ar'?'rtl':'ltr'">
      <header class="border-t-4 border-green-700 bg-white z-10 absolute w-full shadow-md">
        <nav class="px-8 flex py-8">
          <div class="mb-0 lg:mb-6 xl:mb-0">
            <router-link :to="`/${$i18n.locale}`" class="font-bold text-xl flex items-end">
              <img src="@/assets/img/logo.png" alt="logo" class="w-10">
              <span class="mx-4">إمارة منطقة الباحة</span>
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
         
            <li v-if="authUser?.carBoardMember?.boardMemberType?.voteCount > 0" class="mr-12 mb-6 lg:mb-0">
              <router-link :to="`/${$i18n.locale}/carsvoting`" class="text-copy-primary hover:text-gray-600">{{ $t("common.carsVotingTitle") }}</router-link>
            </li>
            <li v-if="authUser?.carBoardMember?.boardMemberType?.id===5" class="mr-12 mb-6 lg:mb-0">
              <router-link :to="`/${$i18n.locale}/carsapproval`" class="text-copy-primary hover:text-gray-600">{{ $t("common.carsApprovalTitle") }}</router-link>
            </li>
            <li class="flex-grow"></li>
            <li v-if="authUser" class="mr-12 mb-6 lg:mb-0 flex items-center">
              <DropdownMenu :authUser="authUser" />
              <!-- <img src="@/assets/img/user_avatar.png" class="avatar" alt=""> 
              <router-link :to="`/${$i18n.locale}/logout`" class="mx-3 text-blue-600 text-sm hover:text-gray-600">{{ authUser.name }}</router-link> -->
            </li>
            <li class="m-2">
              <LanguageSwitcher />
              <!-- <select v-model="$i18n.locale" id="locale">
              <option v-for="locale in $i18n.availableLocales" :key="locale" :value="locale">{{ locale }}</option>
  </select> -->

            </li>
          </ul>
        </nav>
      </header>
      <!-- <div v-bind:class = "!results.contactCompany ? 'grid grid-cols-2':'grid'"> -->
      <div :class="!authUser ? '': 'bg-gray-100 min-h-screen pt-40 text-lg'">
        <router-view/>
      </div>
    </div>
  </template>
<style>
@import '@/assets/base.css';
.avatar {
  vertical-align: middle;
  width: 50px;
  height: 50px;
  border-radius: 50%;
}
</style>