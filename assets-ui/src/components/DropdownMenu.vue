<template>
  <div class="relative">
    <button
      href="#"
      class="flex items-center"
      @click="toggleVisibility"
      @keydown.space.exact.prevent="toggleVisibility"
      @keydown.esc.exact="hideDropdown"
      @keydown.shift.tab="hideDropdown"
      @keydown.up.exact.prevent="startArrowKeys"
      @keydown.down.exact.prevent="startArrowKeys"
    >
      <img src="@/assets/img/user_avatar.png" alt="avatar" class="w-8 h-8 rounded-full">
      <svg fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24"><path class="heroicon-ui" d="M15.3 9.3a1 1 0 0 1 1.4 1.4l-4 4a1 1 0 0 1-1.4 0l-4-4a1 1 0 0 1 1.4-1.4l3.3 3.29 3.3-3.3z"></path></svg>
    {{ authUser?.name }}
    </button>
    <transition name="dropdown-fade">
      <ul v-click-away="hideDropdown" v-if="isVisible" ref="dropdown" class="absolute normal-case font-normal xs:left-0 lg:right-0 bg-white shadow overflow-hidden rounded w-48 border mt-2 py-1 lg:z-20">
       
        <li>
          <a
          @click="openNewRequest"
            class="flex items-center px-3 py-3 hover:bg-gray-200"
            @keydown.tab.exact="focusNext(false)"
            @keydown.shift.tab="focusPrevious(false)"
            @keydown.up.exact.prevent="focusPrevious(true)"
            @keydown.down.exact.prevent="focusNext(true)"
            @keydown.esc.exact="hideDropdown"
          >
            <svg fill="currentColor" class="text-gray-600" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24"><path class="heroicon-ui" d="M12 22a10 10 0 1 1 0-20 10 10 0 0 1 0 20zm0-2a8 8 0 1 0 0-16 8 8 0 0 0 0 16zM10.59 8.59a1 1 0 1 1-1.42-1.42 4 4 0 1 1 5.66 5.66l-2.12 2.12a1 1 0 1 1-1.42-1.42l2.12-2.12A2 2 0 0 0 10.6 8.6zM12 18a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"></path></svg>
            <span class="ml-2">{{$t("cars.request")}}</span>
          </a>
        </li>
        <li>
          <router-link :to="`/${$i18n.locale}/logout`" 
          class="flex items-center px-3 py-3 hover:bg-gray-200"
          @keydown.shift.tab="focusPrevious(false)"
            @keydown.up.exact.prevent="focusPrevious(true)"
            @keydown.down.exact.prevent=""
            @keydown.tab.exact="hideDropdown"
            @keydown.esc.exact="hideDropdown">
            <svg fill="currentColor" class="text-gray-600" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M0 0h24v24H0z" fill="none"></path><path d="M10.09 15.59L11.5 17l5-5-5-5-1.41 1.41L12.67 11H3v2h9.67l-2.58 2.59zM19 3H5c-1.11 0-2 .9-2 2v4h2V5h14v14H5v-4H3v4c0 1.1.89 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2z"></path></svg>
            <span class="ml-2 text-sm text-red-600">{{ $t("login.logout") }}</span>
          </router-link>
        </li>
      </ul>
    </transition>
  </div>
  <CarRequest :request="request" :isOpen="isOpen" @saved="requestSaved" @error="requestError" @closed="requestModalClosed"></CarRequest>

</template>

<script setup>
import { ref } from 'vue'
 import { CarRequest } from '@/views';
 const props= defineProps({
  authUser: {
    type: Object
  }
 })
const isVisible = ref(false);
const focusedIndex = ref(0);
function toggleVisibility() {
      isVisible.value = !isVisible.value
    }
function hideDropdown() {
      isVisible.value = false
      focusedIndex.value = 0
    }
    function startArrowKeys() {
      if (isVisible.value) {
        // this.$refs.account.focus()
        this.$refs.dropdown.children[0].children[0].focus()
      }
    }
    function focusPrevious(isArrowKey) {
      focusedIndex.value = focusedIndex.value - 1
      if (isArrowKey) {
        focusItem()
      }
    }
    function focusNext(isArrowKey) {
      focusedIndex.value = this.focusedIndex.value + 1
      if (isArrowKey) {
        this.focusItem()
      }
    }
    function focusItem() {
      this.$refs.dropdown.children[this.focusedIndex].children[0].focus()
    }

    function openNewRequest(){
  request.value=null;
  isOpen.value=true;
}
const isOpen = ref(false);
const toggleModal = () => {
  isOpen.value = !isOpen.value;
};

const requestSaved = (data) => {
  console.log(data);
  isOpen.value=false;
};

const requestModalClosed = ()=> {
  isOpen.value=false;
}
const requestError = (data) => {
  console.log(data);
};

const request = ref(null)
</script>

<style scoped>
  .dropdown-fade-enter-active, .dropdown-fade-leave-active {
    transition: all .1s ease-in-out;
  }
  .dropdown-fade-enter, .dropdown-fade-leave-to {
    opacity: 0;
    transform: translateY(-12px);
  }
</style>

