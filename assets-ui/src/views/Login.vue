<script setup>
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';

import { useAuthStore } from '@/stores';

const schema = Yup.object().shape({
    username: Yup.string().required('Username is required'),
    password: Yup.string().required('Password is required'),
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
<template>
  <div class="h-screen overflow-hidden flex items-center justify-center" style="background: #edf2f7;">
  <div class="bg-gray-100 flex justify-center items-center h-screen">
  <!-- Left: Image -->
<div class="w-1/2 h-screen hidden lg:block">
<!-- <img src="https://placehold.co/800x/667fff/ffffff.png?text=Emarah&font=Montserrat" alt="Placeholder Image" class="object-cover w-full h-full"> -->
<img src="../assets/logo2.png" style="width:800px;height:667px" class="py-4 object-cover w-full h-full">
</div>
<!-- Right: Login Form -->
<div class="lg:p-36 md:p-52 sm:20 p-8 w-full lg:w-1/2">
<h1 class="text-2xl font-semibold mb-4"> {{ $t("login.login") }}</h1>
<Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors, isSubmitting }">
            <div class="form-group">
                <label>{{ $t("login.username") }}</label>
                <Field name="username" type="text" class="form-control" :class="{ 'is-invalid': errors.username }" />
                <div class="invalid-feedback">{{errors.username}}</div>
            </div>            
            <div class="form-group">
                <label>{{ $t("login.password") }}</label>
                <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
                <div class="invalid-feedback">{{errors.password}}</div>
            </div>            
            <div class="form-group">
                <button class="btn btn-primary" :disabled="isSubmitting">
                    <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
                    {{ $t("login.login") }}
                </button>
            </div>
            <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{errors.apiError.Title}}</div>
        </Form>

</div>
</div>
</div>
</template>