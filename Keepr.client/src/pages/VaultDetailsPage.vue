<template>
  <div class="component container-fluid">
    <div class="row justify-content-around m-3">
      <div class="col-md-6">
        <h1 class="vault-title">{{ vault.name }}</h1>
        <h1>{{ vault.description }}</h1>
      </div>
      <div class="col-md-3 d-flex justify-content-around m-2 p-5">
        <button
          v-if="vault.creatorId == account.id"
          class="btn btn-primary btn-height"
        >
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row m-3">
      <div class="col-md-12">
        <h4>Keeps:</h4>
      </div>
      <div class="col-md-4"></div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity';
import { useRoute } from 'vue-router';
import { onMounted } from '@vue/runtime-core';
import { vaultsService } from '../services/VaultsService';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
export default {

  setup() {
    let editable = ref({})
    const route = useRoute();
    onMounted(async () => {
      if (route.params.id) {
        try {
          vaultsService.getVaultById(route.params.id)
          vaultsService.getVaultKeeps(route.params.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.vault-title {
  font-size: 80px;
}
.btn-height {
  height: 50px;
}
</style>