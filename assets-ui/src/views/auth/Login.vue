<script setup>
import { Form, Field, ErrorMessage } from 'vee-validate';
import * as Yup from 'yup';
import i18n from '@/locales/i18'
import { useAuthStore } from '@/stores';
const schema = Yup.object().shape({
    username: Yup.string().required(i18n.global.t('login.userNameRequied')),
    password: Yup.string().required(i18n.global.t('login.passwordRequired'))
});

function onSubmit(values, { setErrors }) {
    const authStore = useAuthStore();
    const { username, password } = values;

    return authStore.login(username, password)
        .catch(error => {
            
            setErrors({ apiError: error });
        });
}
</script>
<template src="./login2.html">
 
</template>
