import { defineStore } from 'pinia';

import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}/api/cars`;

export const useCarsStore = defineStore({
    id: 'cars',
    state: () => ({
        userCars: {},
        subordinates: {},
        carRequest: {},
        responseTypes: {}
    }),
    getters: {
        subordinateCarRequests: (state) => Object.values(state.subordinates).filter((s) => s.carRequests?.length),
      },
    actions: {
        async carRequestDetailsByBeneficiary(beneficiaryId) {
            this.carRequest = { loading: true };
            let url = `${baseUrl}/CarRequestDetailsByBeneficiary/${beneficiaryId}`;
            return await fetchWrapper.get(url)
            .catch(error => this.carRequest = { error })
        },
        async getResponseTypes(typeId) {
            this.responseTypes = { loading: true };
            let url = `${baseUrl}/GetListItems/${typeId}`;
             await fetchWrapper.get(url)
             .then(items => {
                this.responseTypes=items;
             })
            .catch(error => this.responseTypes = { error })
        },
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
        },
        async requestCar(requestDetail, notes) {
            this.carRequest = { loading: true}
            return await fetchWrapper.post(`${baseUrl}/RequestCar`, { requestDetail, notes })
            .catch(error => this.carRequest = { error})
        },

        //Manager Response:
        async carManagerResponse(managerResponse) {
            this.carRequest = { loading: true}
            return await fetchWrapper.post(`${baseUrl}/ResponseCar`, managerResponse)
            .catch(error => this.carRequest = { error})
        },
    }
});