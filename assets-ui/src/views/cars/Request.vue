<template src="./request.html">
   
</template>

<script setup>
import { ref } from 'vue';
import { Form, Field } from 'vee-validate';
import Dialog from '@/components/Dialog.vue';
import Button from '@/components/Button.vue';
import { useCarsStore } from '@/stores'
import * as Yup from 'yup';


const emit = defineEmits(["saved","error"]);
const props = defineProps({
    isOpen: {
      type: Boolean,
      default: false,
    },
  });

  const toggleModal = () => {
//   isOpen.value = !isOpen.value;
};
const schema = Yup.object().shape({
    requestDetail: Yup.string().required('detail is required'),
    notes: Yup.string().required('notes is required'),
});
const apiError = ref(null)
async function onSubmit(values) {
    const carsStore=useCarsStore();
    const { requestDetail, notes } = values;
    return await carsStore.requestCar(requestDetail, notes)
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
