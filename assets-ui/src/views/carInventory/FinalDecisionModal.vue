<script setup>
import { ref, defineAsyncComponent,computed,reactive } from 'vue';
import { storeToRefs } from 'pinia'
import { Form, Field, ErrorMessage } from 'vee-validate';
import { useCarsStore } from '@/stores'
import * as Yup from 'yup';
const Dialog = defineAsyncComponent(() => import('@/components/Dialog.vue'))
const carsStore=useCarsStore();
const { carsList, carTypes } = storeToRefs(carsStore)
carsStore.getResponseTypes();
carsStore.getCarTypes();
carsStore.getCars();
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
<style lang="scss">
$table-header: #1976D2;
$table-header-border: #1565C0;
$table-border: #d9d9d9;
$row-bg: #f4f2f1;
$header-bg: #1565C0;

body {
  width: 100%;
  height: 100%;
  margin: 0;
  padding: 0;
}

caption {
  display: none;
}

div {
  box-sizing: border-box;
}

.table-container {
  //display: block;
  margin: 1em auto;
  width: 90%;
  max-width: 600px;
  border-collapse: collapse;
}

.flag-icon {
  margin-right: 0.1em;
}

td {
  border: 1px solid $table-border;
  padding: 0.5em;
}

th {
  background: $header-bg;
  color: white;
  padding: 0.5em;
  border: 1px solid $table-header-border;
}

.flex-row {
  width: 25%;
}

.flex-row, .flex-cell {
  text-align: center;
}

@media only screen and (max-width: 767px) {
  
  .table-container {
    //display: block;
  }
  th, td {
    width: auto;
    display: block;
    border: 0;
  }
  
  th {
    border-left: solid 1px $table-header-border;
    border-right: solid 1px $table-header-border;
    border-bottom: solid 1px $table-header-border;
  }
  
  td {
    border-left: solid 1px $table-border;
    border-right: solid 1px $table-border;
    border-bottom: solid 1px $table-border;
  }
  
  .flex-row {
    width: 100%;
  }
}
</style>