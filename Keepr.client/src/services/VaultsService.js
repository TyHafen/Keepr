import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {


    async createVault(vaultData) {
        const res = await api.post('api/vaults/', vaultData)
    }
    async getVaultById(id) {
        const res = await api.get(`api/vaults/${id}`)
        AppState.activeVault = res.data
        logger.log("The active vault", AppState.activeVault)
    }
    async getVaultKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        AppState.activeVaultKeeps = res.data
        logger.log("active VaultKeeps", AppState.activeVaultKeeps)
    }
    async deleteVault(id) {
        const res = await api.delete(`api/vaults/${id}`)
        logger.log("vault deleting")
    }
}

export const vaultsService = new VaultsService()