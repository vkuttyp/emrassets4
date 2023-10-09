import {RouterView,  createRouter, createWebHistory } from 'vue-router';

import { useAuthStore } from '@/stores';
import { HomeView, Login, Logout, Cars, Dashboard } from '@/views';
import i18n  from '@/locales/i18.js';
const locale = i18n.locale ?? 'ar';
export const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    linkActiveClass: 'active',
    routes: [
        {
            path: '/',
            // redirect: `/${i18n.locale}`
            redirect: '/${locale}'
        },
       {
        path: '/:lang',
        component: RouterView,
        children: [
        { 
            path: '',
            name: 'home',
            component: Dashboard
        },
        { 
            path: 'dashboard',
            name: 'home',
            component: Dashboard
        },
        { 
            path: 'cars',
            name: 'cars',
            component: Cars
        },
        { 
            path: 'login',
            name: 'login',
            component: Login
        },
        { 
            path: 'logout',
            name: 'logout',
            component: Logout
        },
    ]
       } 
    
    ]
});

router.beforeEach(async (to) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/ar/login'];
    const authRequired = !publicPages.includes(to.path);
    const auth = useAuthStore();

    if (authRequired && !auth.user) {
        auth.returnUrl = to.fullPath;
        // return '/ar/login';
        return `/${locale}/login`;
    }
});

export default router
