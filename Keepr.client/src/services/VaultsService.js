import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService {


    async createVault(vaultData) {
        const res = await api.post('api/vaults/', vaultData)
        AppState.profileVaults.unshift(res.data)
    }
}

export const vaultsService = new VaultsService()