import { defineStore } from 'pinia';

import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}/api/cars`;

export const useCarsStore = defineStore({
    id: 'cars',
    state: () => ({
        userCars: {},
        subordinates: {}
    }),
    actions: {
        // async getByBeneficieries(beneficieryId) {
        //     this.userAssets = { loading: true };
        //     let url = `${baseUrl}/AssetDeliveriesByBeneficiary/${beneficieryId}`;
        //     fetchWrapper.get(url)
        //         .then(assets => { 
        //             this.userAssets = assets;
        //             // this.getSubordinates();
        //             //  console.log(this.users)
        //         })
        //         .catch(error => this.userAssets = { error })
        // },
        async currentUserDeliveries() {
            this.userCars = { loading: true };
            let url = `${baseUrl}/CarDeliveriesForCurrentUser`;
            fetchWrapper.get(url)
                .then(cars => { 
                    this.userCars = cars;
                    // this.getSubordinates();
                    //  console.log(this.users)
                })
                .catch(error => this.userCars = { error })
        },
        async getSubordinates() {
            this.userCars = { loading: true };
            let url = `${baseUrl}/SubordinateDetails`;
            fetchWrapper.get(url)
                .then(data => { 
                    this.subordinates = data;
                    //  console.log(this.users)
                })
                .catch(error => this.subordinates = { error })
        }
    }
});