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
        <div class="row justify-content-center">keeps, sees, likes</div>

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
              <div class="dropdown">
                <button
                  class="btn btn-primary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Add to Vault
                </button>
                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <a class="dropdown-item" href="#">Action</a>
                  <a class="dropdown-item" href="#">Another action</a>
                  <a class="dropdown-item" href="#">Something else here</a>
                </div>
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
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'

export default {
  setup() {
    const router = useRouter()
    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
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