import { defineStore } from 'pinia';

import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}/api/cars`;

export const useCarsStore = defineStore({
    id: 'cars',
    state: () => ({
        saving: {},
        userCars: {},
        subordinates: {},
        carRequests: {},
        responseTypes: {},
        carTypes: {},
        carVotingPendingList: {},
        carsList: {},
        carsFinalDecisions: {}
    }),
    getters: {
        subordinateCarRequests: (state) => Object.values(state.subordinates).filter((s) => s.carRequests?.length),
        votingIncomple: (state) => Object.values(state.carVotingPendingList).filter((s) => s.totalVotesCount > s.totalVotesObtained),
        votingCompleted: (state) => Object.values(state.carVotingPendingList).filter((s) => s.totalVotesCount === s.totalVotesObtained),
      },
    actions: {

        async carRequestDetailsByBeneficiary(beneficiaryId) {
            this.carRequests = { loading: true };
            let url = `${baseUrl}/CarRequestDetailsByBeneficiary/${beneficiaryId}`;
            await fetchWrapper.get(url)
            .then(items => {
                this.carRequests=items;
             })
            .catch(error => this.carRequests = { error })
        },

        async getResponseTypes() {
            this.responseTypes = { loading: true };
            let url = `${baseUrl}/GetListItems/1`;
             await fetchWrapper.get(url)
             .then(items => {
                this.responseTypes=items;
             })
            .catch(error => this.responseTypes = { error })
        },
        async getCarTypes() {
            this.carTypes = { loading: true };
            let url = `${baseUrl}/GetListItems/2`;
             await fetchWrapper.get(url)
             .then(items => {
                this.carTypes=items;
             })
            .catch(error => this.carTypes = { error })
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
        async requestCar(request) {
            this.carRequest = { loading: true}
            return await fetchWrapper.post(`${baseUrl}/RequestCar`, request)
            .catch(error => this.carRequest = { error})
        },

        //Manager Response:
        async carManagerResponse(managerResponse) {
            this.carRequest = { loading: true}
            return await fetchWrapper.post(`${baseUrl}/ResponseCar`, managerResponse)
            .catch(error => this.carRequest = { error})
        },

        //Pending Voting List:
        async getCarVotingPendingList() {
            this.userCars = { loading: true };
            let url = `${baseUrl}/CarVotingPendingList`;
            fetchWrapper.get(url)
                .then(data => { 
                    this.carVotingPendingList = data;
                    //  console.log(this.users)
                })
                .catch(error => this.carVotingPendingList = { error })
        },

         //Board voting:
         async carMemberVote(vote) {
            this.saving = { loading: true}
            return await fetchWrapper.post(`${baseUrl}/CarVoting`, vote)
            .catch(error => this.saving = { error})
        },

         //Final Decision
         async carFinalDecision(decision) {
            this.saving = { loading: true}
            return await fetchWrapper.post(`${baseUrl}/CarFinalDecision`, decision)
            .catch(error => this.saving = { error})
        },

        //Cars list:
        async getCars() {
            this.carsList = { loading: true };
            let url = `${baseUrl}/GetCars`;
            fetchWrapper.get(url)
                .then(cars => { 
                    this.carsList = cars;
                })
                .catch(error => this.carsList = { error })
        },

         //:CarVotingFinalDecision_List
         async getCarsFinalDecisions() {
            this.carsFinalDecisions = { loading: true };
            let url = `${baseUrl}/CarVotingFinalDecision_List`;
            fetchWrapper.get(url)
                .then(d => { 
                    this.carsFinalDecisions = d;
                })
                .catch(error => this.carsFinalDecisions = { error })
        },
    }
});