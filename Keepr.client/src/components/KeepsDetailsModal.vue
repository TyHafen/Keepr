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
          <h1 class="border-bottom border-3 border-dark">{{ keep.name }}</h1>
        </div>
        <div class="row my-4">
          <p>{{ keep.description }}</p>
        </div>
        <div class="row">
          <div class="col-md-5 d-flex align-items-end">
            <button class="btn btn-primary"><h5>Add to Vault</h5></button>
          </div>

          <div class="col-md-7">
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

export default {
  setup() {
    const router = useRouter()
    return {
      keep: computed(() => AppState.activeKeep),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: "Profile", params: { id } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.photo {
  height: 50px;
  width: 75px;
}
</style>