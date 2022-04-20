<template>
  <div class="component">
    <div class="row">
      <div class="col-md-6">
        <img
          height="500"
          class="w-100 object-fit-cover"
          :src="keep.img"
          alt=""
        />
      </div>
      <div class="col-md-6">
        <div class="row justify-content-center">
          <div class="col-md-9"></div>
          <div class="col-md-3 d-flex justify-content-right p-1">
            <div class="mx-2">
              <h5><i class="mdi mdi-file"> </i>{{ keep.kept }}</h5>
            </div>
            <div class="mx-2">
              <h5><i class="mdi mdi-eye"></i> {{ keep.views }}</h5>
            </div>
          </div>
        </div>

        <div class="row m-3justify-content-center">
          <h1
            class="
              border-bottom border-3 border-dark
              d-flex
              justify-content-between
            "
          >
            {{ keep.name }}
            <i
              v-if="keep.creatorId == account.id"
              class="mdi mdi-delete"
              @click="deleteKeep(keep.id)"
            ></i>
          </h1>
          <h3></h3>
        </div>
        <div class="row">
          <h4>
            <div class="">
              <div>
                <title>add to Vault</title>
                <select name="" id="" v-model="vaultId">
                  <option :value="v.id" v-for="v in myVaults" :key="v.id">
                    {{ v.name }}
                  </option>
                </select>
                <button class="btn btn-primary m-1" @click="create">
                  Add to vault
                </button>
              </div>
            </div>
          </h4>
        </div>
        <div class="row my-4">
          <p>{{ keep.description }}</p>
        </div>

        <div class="row fixed-bottom">
          <div class="col-md-6 d-flex align-items-end justify-content-around">
            <button
              class="btn btn-primary"
              @click="deleteKeepFromVault(keep.id)"
            >
              Delete From Vault
            </button>
          </div>

          <div class="col-md-6">
            <div
              class="row align-items-end"
              @click="goToProfile(keep.creatorId)"
            >
              <img class="photo" :src="keep.creator?.picture" alt="" />
              {{ keep.creator?.name }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, reactive, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { useRoute, useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { onMounted } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { accountService } from '../services/AccountService'
import { vaultKeepsService } from '../services/VaultKeepsService'

export default {
  setup() {
    const vaultId = ref({})
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        // await accountService.getAccountVaults()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })


    return {
      vaultId,
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.accountVaults),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: "Profile", params: { id } })
      },
      async deleteKeep(id) {
        try {
          if (await Pop.confirm("Are you sure you want to delete this keep?")) {
            Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
            await keepsService.deleteKeep(id);

          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }

      },
      async create() {
        try {
          logger.log(vaultId.vaultId)
          const newVaultKeep = { vaultId: vaultId.value, keepId: AppState.activeKeep.id }
          logger.log(newVaultKeep)
          await vaultKeepsService.create(newVaultKeep)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async deleteKeepFromVault(keepId) {
        try {
          if (await Pop.confirm("Are you sure you want to delete this keep?")) { await vaultKeepsService.delete(keepId, route.params.id) }

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
.photo {
  height: 40px;
  width: 60px;
}
</style>