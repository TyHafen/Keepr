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
          <h1 class="border-bottom border-3 border-dark">
            {{ keep.name }}
          </h1>
        </div>
        <div class="row my-4">
          <p>{{ keep.description }}</p>
        </div>
        <div class="row">
          <div class="col-md-6 d-flex align-items-end justify-content-around">
            <h4>
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
            </h4>

            <h3>
              <i
                v-if="keep.creatorId == account.id"
                class="mdi mdi-delete"
                @click="deleteKeep(keep.id)"
              ></i>
            </h3>
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
import { useRouter } from 'vue-router'
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
    onMounted(async () => {
      try {
        await accountService.getAccountVaults()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })


    return {

      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.accountVaults),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: "Profile", params: { id } })
      },
      async deleteKeep(id) {
        try {
          if (await Pop.confirm("Are you sure you want to delete this vault?")) {
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
          const newVaultKeep = { vaultId: vaultId.value, keepId: AppState.activeKeep.id }
          logger.log(newVaultKeep)
          await vaultKeepsService.create(newVaultKeep)
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