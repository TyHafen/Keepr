import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepService {
    async getAll() {
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
        logger.log(AppState.keeps)
    }
    async setActive(id) {
        const res = await api.get("api/keeps/" + id)
        AppState.activeKeep = res.data
        logger.log(AppState.activeKeep)
    }

}


export const keepsService = new KeepService()