<template>
  <div class="container-fluid">
    <div class="row m-2 justify-content-start p-3">
      <div class="col-md-1 p-2 align-items-end d-flex">
        <img class="rounded" :src="profile.picture" alt="" />
      </div>
      <div class="col-md-9">
        <h1>{{ profile.name }}</h1>
        <h3>Vaults: {{ profileVaults.length }}</h3>
        <h3>Keeps: {{ profileKeeps.length }}</h3>
      </div>
    </div>
    <div class="row mt-5 m-3">
      <h1 class="">
        Vaults
        <i
          v-if="account.id == profile.id"
          class="mdi mdi-plus selectable"
          data-bs-toggle="modal"
          data-bs-target="#create-vault"
        ></i>
      </h1>
      <div class="col-md-2 m-1" v-for="v in profileVaults" :key="v.id">
        <router-link :to="{ name: 'Vault', params: { id: v.id } }">
          <h4 class="btn btn-primary shadow bold">
            {{ v.name }} <i v-if="v.isPrivate" class="mdi mdi-lock"></i>
          </h4>
        </router-link>
      </div>
    </div>
    <div class="mt-5 m-3">
      <h1>
        Keeps
        <i
          class="mdi mdi-plus selectable"
          data-bs-toggle="modal"
          data-bs-target="#create-keep"
        ></i>
      </h1>
      <div class="masonry">
        <div class="col-md-12 p-3" v-for="k in profileKeeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <KeepFormModal>
    <template #modal-title>Create a new keep</template>
    <template #modal-body><KeepsForm /></template>
  </KeepFormModal>

  <VaultFormModal>
    <template #modal-body><VaultForm /></template>
  </VaultFormModal>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService.js'
import { accountService } from '../services/AccountService.js'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getProfilesVaults(route.params.id)
        await profilesService.getProfilesKeeps(route.params.id)

      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      profileVaults: computed(() => AppState.profileVaults),
      profileKeeps: computed(() => AppState.profileKeeps)
    }
  }
}
</script>

<style  lang="scss" scoped>
.masonry {
  columns: 6 220px;
  column-gap: 1rem;

  div {
    display: inline-block;
    width: 100%;
  }
}
.bold {
  font-weight: 600;
}
</style>
