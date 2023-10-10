// import './assets/base.css'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import  i18n  from './locales/i18'

import App from './App.vue'
import router from './helpers/router'
import VueClickAway from "vue3-click-away";

router.beforeEach((to, from, next)=> {
    document.title = `${i18n.global.t('common.siteName')} - ${to.meta.title}`;
    let language = to.params.lang;
    if(!language) {
        language = 'ar'
    }
    i18n.locale = language;
    next();
})
const app = createApp(App)
app.use(createPinia())
app.use(router)
app.use(i18n)
app.use(VueClickAway);
app.mount('#app')
