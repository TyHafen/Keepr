import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getProfile(id) {
        const res = await api.get(`api/profiles/${id}`)
        AppState.profile = res.data
        logger.log("profile:", AppState.profile)
        logger.log("acoount:", AppState.account)
    }
    async getProfilesKeeps(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
        AppState.profileKeeps = res.data
        logger.log("profileKeeps:", AppState.profileKeeps)
    }
    async getProfilesVaults(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        AppState.profileVaults = res.data
        logger.log("profile vaults:", AppState.profileVaults)
    }


}



export const profilesService = new ProfilesService()