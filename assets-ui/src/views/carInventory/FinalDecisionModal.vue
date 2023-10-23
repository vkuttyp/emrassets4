<script setup>
import { ref, defineAsyncComponent,computed,reactive } from 'vue';
import { storeToRefs } from 'pinia'
import { Form, Field, ErrorMessage } from 'vee-validate';
import { useCarsStore } from '@/stores'
import * as Yup from 'yup';
import i18n from '@/locales/i18'
const Dialog = defineAsyncComponent(() => import('@/components/Dialog.vue'))
const carsStore=useCarsStore();
const { carsList, carTypes } = storeToRefs(carsStore)
carsStore.getResponseTypes();
carsStore.getCarTypes();
carsStore.getCars();
const t = i18n.global.t;
const selectCar = (car) => {
  selectedCar.value = car
}
let selectedCar = ref(null)

function carTypeChanged(event){
  let typeId=event.target.value;
  carsList.value.filter((car)=> car.carType===typeId);
}
const selectedCarType = ref(0);
const numberPlate = ref('');
const carModel = ref('');
const getCars = computed(() => {
      if (selectedCarType.value>0) {
        return carsList.value.filter((car) => car.carTypeId === selectedCarType.value && car.numberPlate.includes(numberPlate.value) && car.model.includes(carModel.value));
      }
      return carsList.value.filter((car)=> car.numberPlate.includes(numberPlate.value) && car.model.includes(carModel.value));
    });

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
    responseTypeId: Yup.number().moreThan(0,t('decision.approval')),
    notes: Yup.string(),
});
const apiError = ref(null)
const myAlert = ref({
  visible: false,
  title: '',
  text: ''
})
function showAlert(title, text){
  myAlert.value.visible=true;
  myAlert.value.title=title;
  myAlert.value.text=text;
  setTimeout(() => myAlert.value.visible = false, 1000)
}
async function onSubmit(values) {
   let carId= selectedCar.value?.id ?? 0;
   
    const { responseTypeId, notes } = values;
    if(carId ===0 && responseTypeId===1){
    showAlert(t('decision.alertTitle'), t('decision.alertText'))
    return;
   }
    props.decision.responseTypeId=responseTypeId;
    props.decision.notes=notes;
    props.decision.carId=carId;
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
