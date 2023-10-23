import {RouterView,  createRouter, createWebHistory } from 'vue-router';

import { useAuthStore } from '@/stores';
import { Login, Logout, Cars, Dashboard, CarVoting, CarFinalApproval, CarsAndEmployees } from '@/views';
import i18n  from '../locales/i18.js';
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
            name: 'dashboard',
            component: Dashboard,
            meta: { title: i18n.global.t('common.dashboardTitle') }
        },
        { 
            path: 'dashboard',
            name: 'home',
            component: Dashboard,
            meta: { title: i18n.global.t('common.dashboardTitle') }
        },
        { 
            path: 'cars',
            name: 'cars',
            component: Cars,
            meta: { title: i18n.global.t('common.carsTitle') }
        },
        { 
            path: 'carsvoting',
            name: 'carsvoting',
            component: CarVoting,
            meta: { title: i18n.global.t('common.carsVotingTitle') }
        },
        { 
            path: 'carsapproval',
            name: 'carsapproval',
            component: CarFinalApproval,
            meta: { title: i18n.global.t('common.carsApprovalTitle') }
        },
        { 
            path: 'carsAndEmployees',
            name: 'carsAndEmployees',
            component: CarsAndEmployees,
            meta: { title: i18n.global.t('common.carsApprovalTitle') }
        },
        { 
            path: 'login',
            name: 'login',
            component: Login,
            meta: { title: i18n.global.t('common.loginTitle') }
        },
        { 
            path: 'logout',
            name: 'logout',
            component: Logout,
            meta: { title: 'Logout' }
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
