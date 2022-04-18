import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepService {
    async createKeep(keepData) {
        const res = await api.post('api/keeps/', keepData)
    }
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
    async deleteKeep(id) {
        const res = await api.delete("api/keeps/" + id)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }

}


export const keepsService = new KeepService()