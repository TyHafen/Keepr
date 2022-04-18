<template>
  <div class="component">
    <form @submit.prevent="createKeep">
      <div class="form-group">
        <label for="exampleInputName">Name</label>
        <input
          type="name"
          v-model="keep.name"
          class="form-control"
          id="keep-name"
          aria-describedby="keepHelp"
          placeholder="Enter  Keep Name"
        />
      </div>
      <div class="form-group">
        <label for="exampleInputDescription">Description</label>
        <input
          type="description"
          v-model="keep.description"
          class="form-control"
          id="exampleDescription1"
          placeholder="Description"
        />
      </div>
      <div class="form-group">
        <label for="exampleInputDescription">Image</label>
        <input
          type="description"
          v-model="keep.img"
          class="form-control"
          id="exampleInput1"
          placeholder="Enter Image Url"
        />
      </div>

      <button type="submit" class="btn btn-primary m-2">Submit</button>
    </form>
  </div>
</template>


<script>
import { ref } from '@vue/reactivity';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
export default {
  setup() {
    let keep = ref({});
    return {
      keep,
      async createKeep() {
        try {
          Modal.getOrCreateInstance(document.getElementById('create-keep')).hide()
          await keepsService.createKeep(keep.value)
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