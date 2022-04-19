import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async create(newData) {
        const res = await api.post('api/vaultkeeps', newData)
        logger.log('new vault keep', res.data)
    }
}


export const vaultKeepsService = new VaultKeepsService()