<template>
  <div class="component container">
    <div class="masonry">
      <div class="col-md-4 p-3" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService.js'
import { AppState } from '../AppState'
export default {
  setup() {
    watchEffect(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry {
  columns: 6 220px;
  column-gap: 1rem;

  div {
    display: inline-block;
    width: 100%;
  }
}
</style>
