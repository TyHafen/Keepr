<template>
  <div class="container-fluid">
    <div class="row m-2 justify-content-start p-3">
      <div class="col-md-1 p-2">
        <img class="rounded" :src="account.picture" alt="" />
      </div>
      <div class="col-md-9 m-3">
        <h1>Welcome {{ account.name }}</h1>
        <h3>Vaults</h3>
        <h3>Keeps</h3>
      </div>
    </div>
    <div class="row mt-5 m-3">
      <h1>Vaults +</h1>
    </div>
    <div class="row mt-5 m-3">
      <h1>Keeps +</h1>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService.js'
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfileKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      profileKeeps: computed(() => AppState.profileKeeps)
    }
  }
}
</script>

<style scoped>
img {
  width: 150px;
}
</style>
