<template >
  <div
    class="component"
    data-bs-toggle="modal"
    data-bs-target="active-vault"
    @click="setActive"
  >
    <div class="bg-dark card-shadow selectable rounded">
      <img class="card-img" :src="keep.img" alt="Card image" />
      <div class="card-img-overlay d-flex">
        <div class="row align-items-end">
          <h4 class="card-title vault-text">{{ keep.name }}</h4>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async setActive() {
        try {
          await keepsService.setActive(props.keep.id)
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
</style>