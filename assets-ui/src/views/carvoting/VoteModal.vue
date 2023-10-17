<template src="./votingmodal.html">
   
</template>

<script setup>
import { ref, defineAsyncComponent } from 'vue';
import { storeToRefs } from 'pinia'
import { Form, Field, ErrorMessage } from 'vee-validate';
// import Dialog from '@/components/Dialog.vue';
import Button from '@/components/Button.vue';
import { useCarsStore, useAuthStore } from '@/stores'
import * as Yup from 'yup';

const carsStore=useCarsStore();
carsStore.getResponseTypes(1);

const authStore = useAuthStore()
const { user: authUser } = storeToRefs(authStore)
const Dialog = defineAsyncComponent(() => import('@/components/Dialog.vue'))
const emit = defineEmits(["saved","error","closed"]);
const props = defineProps({
    isOpen: {
      type: Boolean,
      default: false,
    },
    managerResponseId: {
      type: String,
      default: '',
      required: true
    },
    carVotingDetail: {
      type: Object
    }
  });
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
    var vote= props.carVotingDetail;
    vote.managerResponseId=props.managerResponseId;
    vote.responseTypeId=responseTypeId;
    vote.notes=notes;
    vote.voteCount=authUser.value.carBoardMember.boardMemberType.voteCount;
    return await carsStore.carMemberVote(vote)
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
