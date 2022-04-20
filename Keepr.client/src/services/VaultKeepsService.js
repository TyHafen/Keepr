import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async create(newData) {
        const res = await api.post('api/vaultkeeps', newData)

        logger.log('new vault keep', res.data)
    }
    async delete(keepId) {
        let vaultKeepToDelete = AppState.activeVaultKeeps.find(vk => vk.id == keepId)
        const res = await api.delete('api/vaultkeeps/' + vaultKeepToDelete.vaultKeepId)
        logger.log("deleted", vaultKeepToDelete.vaultKeepId)
    }
}


export const vaultKeepsService = new VaultKeepsService()