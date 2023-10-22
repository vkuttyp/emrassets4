<template src="./request.html">
   
</template>

<script setup>
import { ref, defineAsyncComponent, computed } from 'vue';
import { Form, Field } from 'vee-validate';
import i18n from '@/locales/i18'
import Button from '@/components/Button.vue';
import { useCarsStore } from '@/stores'
import * as Yup from 'yup';

const Dialog = defineAsyncComponent(() => import('@/components/Dialog.vue'))
const emit = defineEmits(["saved","error","closed"]);
const props = defineProps({
    isOpen: {
      type: Boolean,
      default: false,
    },
    request: {
      type: Object
    }
  });
  const responded = computed(() => {
  return props.request?.carManagerResponse.responseTypeId ===1;
})
  const toggleModal = () => {
    apiError.value=null;
  emit("closed")
};
const schema = Yup.object().shape({
    requestDetail: Yup.string().required(i18n.global.t("carRequest.detailRequired")),
    notes: Yup.string(),
});
const apiError = ref(null)
async function onSubmit(values) {
    const carsStore=useCarsStore();
    const { requestDetail, notes } = values;
    var mrequest=props.request ?? {};
    mrequest.requestDetail=requestDetail;
    mrequest.notes=notes;
    return await carsStore.requestCar(mrequest)
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
