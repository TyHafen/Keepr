<template >
  <div
    class="component"
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
    @click="setActive"
  >
    <div class="bg-dark card-shadow selectable rounded">
      <img class="card-img" :src="vaultKeep.img" alt="Card image" />
      <div class="card-img-overlay d-flex">
        <div class="row align-items-end justify-content-between m-0">
          <div class="col-md-10 d-flex align-items-end">
            <h5 class="card-title vault-text">{{ vaultKeep.name }}</h5>
          </div>
          <div class="col-md-2">
            <img
              v-if="vaultKeep.creator?.picture"
              class="vault-creator"
              :src="vaultKeep.creator?.picture"
              alt=""
            />
          </div>
        </div>
      </div>
    </div>
  </div>
  <ActiveVaultModal>
    <template #modal-body><KeepsDetailsModal /></template>
  </ActiveVaultModal>
</template>


<script>
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  props: {
    vaultKeep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async setActive() {
        try {
          await keepsService.setActive(props.vaultKeep.id)
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
.vault-text {
  text-shadow: 3px 1px 3px rgb(0, 0, 0);
}
.card-shadow {
  box-shadow: 6px 6px 20px 6px rgba(0, 0, 0, 0.19);
}
.vault-creator {
  width: 30px;
  height: 30px;
  border-radius: 50%;
}
</style>