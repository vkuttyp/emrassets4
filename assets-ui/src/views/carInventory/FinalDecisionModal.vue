<script setup>
import { ref, defineAsyncComponent,computed,reactive,watch,createApp, h,defineComponent } from 'vue';
import { storeToRefs } from 'pinia'
import { Form, Field, ErrorMessage } from 'vee-validate';
import { useCarsStore } from '@/stores'
import * as Yup from 'yup';

const Dialog = defineAsyncComponent(() => import('@/components/Dialog.vue'))
const carsStore=useCarsStore();
const { carsList } = storeToRefs(carsStore)
carsStore.getResponseTypes(1);
carsStore.getCars();
const filters = reactive([
  {id: 0,name: 'all'},
  {id: 1, name: 'outside'},
  { id: 2, name: 'inside'}
])
const selectCar = (car) => {
  selectedCar.value = car
}
let selectedCar = ref(null)

  
const emit = defineEmits(["saved","error","closed"]);
const props = defineProps({
    isOpen: {
      type: Boolean,
      default: false,
    },
    decision: {
      type: Object
    }
  });
  const toggleModal = () => {
    apiError.value=null;
  emit("closed")
};

const schema = Yup.object().shape({
    responseTypeId: Yup.number().moreThan(0),
    notes: Yup.string(),
});
const apiError = ref(null)
async function onSubmit(values) {
    const { responseTypeId, notes } = values;
    props.decision.responseTypeId=responseTypeId;
    props.decision.notes=notes;
    props.decision.carId=selectedCar.value.id;
    return await carsStore.carFinalDecision(props.decision)
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
<template src="./finalDecisionModal.html"></template>