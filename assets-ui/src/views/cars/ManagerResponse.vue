<template src="./managerResponse.html">
   
</template>

<script setup>
import { ref } from 'vue';
import { Form, Field } from 'vee-validate';
import Dialog from '@/components/Dialog.vue';
import Button from '@/components/Button.vue';
import { useCarsStore } from '@/stores'
import * as Yup from 'yup';

const carsStore=useCarsStore();
const emit = defineEmits(["saved","error","closed"]);
const props = defineProps({
    isOpen: {
      type: Boolean,
      default: false,
    },
  });
  const selectedItem = ref(null);
  const toggleModal = () => {
    apiError.value=null;
  emit("closed")
};
const schema = Yup.object().shape({
    responseTypeId: Yup.number().moreThan(0),
    notes: Yup.string().required('notes is required'),
});
const apiError = ref(null)
async function onSubmit(values) {
   
    const { responseTypeId, notes } = values;
    return await carsStore.carManagerResponse(responseTypeId, notes)
        .then(data => {
            if(data.error) {
            emit('error',data.error);
            apiError.value=data.error;
            // setErrors({ apiError: data.error });
            }
            else{
            apiError.value=null;
            emit('saved',data);
            }
        })
        .catch(error => {
            emit('error', error);
            apiError.value=error;
        });
}
</script>
