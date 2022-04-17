import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getProfileKeeps(id) {
        const res = await api.get(`api/profiles/${id}/keep`)
        AppState.profileKeeps = res.data
        logger.log(profileKeeps)
    }

}



export const profilesService = new ProfilesService()