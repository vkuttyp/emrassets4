<template>
   <suspense>
      <template #default>
        <Cars />
      </template>
      <template #fallback>
       <p>Loading</p>
      </template>
    </suspense>
</template>

<script setup>

import {Cars} from '../'

</script>
