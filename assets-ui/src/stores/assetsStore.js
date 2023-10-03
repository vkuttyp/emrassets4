import { defineStore } from 'pinia';

import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}/api/assets`;

export const useAssetsStore = defineStore({
    id: 'assets',
    state: () => ({
        userAssets: {},
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
            this.userAssets = { loading: true };
            let url = `${baseUrl}/AssetDeliveriesForCurrentUser`;
            fetchWrapper.get(url)
                .then(assets => { 
                    this.userAssets = assets;
                    // this.getSubordinates();
                    //  console.log(this.users)
                })
                .catch(error => this.userAssets = { error })
        },
        async getSubordinates() {
            this.userAssets = { loading: true };
            let url = `${baseUrl}/SubordinateDetails`;
            fetchWrapper.get(url)
                .then(data => { 
                    this.subordinates = data;
                    //  console.log(this.users)
                })
                .catch(error => this.userAssets = { error })
        }
    }
});
