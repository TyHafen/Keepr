<template>
  <div class="component container-fluid">
    <div class="row justify-content-around m-3">
      <div class="col-md-6">
        <h1 class="vault-title">{{ vault.name }}</h1>
        <h1>{{ vault.description }}</h1>
      </div>
      <div
        class="col-md-3 d-flex justify-content-around m-2 p-5"
        @click="deleteVault(vault.id)"
      >
        <button
          v-if="vault.creatorId == account.id"
          class="btn btn-primary btn-height"
        >
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row m-3">
      <h4>Keeps:</h4>
      <div class="masonry">
        <div class="col-md-12 p-3" v-for="v in vaultKeeps" :key="v.id">
          <VaultKeeps :vaultKeep="v" />
        </div>
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
import { router } from "../router"
import Pop from '../utils/Pop';
import Keep from '../components/Keep.vue';
export default {
  components: { Keep },

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
      vaultKeeps: computed(() => AppState.activeVaultKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      async deleteVault(id) {
        try {
          if (await Pop.confirm("Are you sure you want to delete this vault?")) {
            await vaultsService.deleteVault(id);
            router.push({ name: "Profile", params: { id: AppState.account.id } })
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }

      }
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
.masonry {
  columns: 6 220px;
  column-gap: 1rem;

  div {
    display: inline-block;
    width: 100%;
  }
}
</style>