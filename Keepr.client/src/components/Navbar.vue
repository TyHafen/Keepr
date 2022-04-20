<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary px-3 shadow">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <h3 class="title">Keepr</h3>
      </div>
    </router-link>
    <div class="collapse navbar-collapse justify-content-end" id="navbarText">
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.title {
  font-size: 50px;
  text-shadow: 2px 2px 3px black;
}
</style>
