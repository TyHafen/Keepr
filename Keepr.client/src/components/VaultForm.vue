<template>
  <div class="component">
    <form @submit.prevent="createVault">
      <div class="form-group">
        <label for="exampleInputName">Vault Name</label>
        <input
          type="name"
          v-model="vault.name"
          class="form-control"
          id="vault-name"
          aria-describedby="vaultHelp"
          placeholder="Enter vault name"
        />
      </div>
      <div class="form-group">
        <label for="exampleInputDescription">Description</label>
        <input
          type="description"
          v-model="vault.description"
          class="form-control"
          id="exampleInputDescription1"
          placeholder="description"
        />
      </div>
      <div class="form-check">
        <input type="checkbox" class="form-check-input" id="exampleCheck1" />
        <label class="form-check-label" for="exampleCheck1">private</label>
      </div>
      <button type="submit" class="btn btn-primary">Submit</button>
    </form>
  </div>
</template>


<script>
import { ref } from '@vue/reactivity';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService.js'
export default {
  setup() {
    let vault = ref({});
    return {
      vault,
      async createVault() {
        try {
          await vaultsService.createVault(vault.value);
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
</style>