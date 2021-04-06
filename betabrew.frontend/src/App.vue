<template>
  <div id="app">
    <div class="app-menu">
      <side-menu />
    </div>
    <div class="app-content">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import SideMenu from '@/components/SideMenu.vue'

@Component({
  name: 'App',
  components: { SideMenu },
})
export default class App extends Vue {}
</script>

<style lang="scss">
@import '@/scss/global.scss';

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  display: flex;

  .app-menu {
    position: fixed;
  }

  .app-content {
    padding: 15px;
    flex: 1;
    margin: 20px auto 20px $menu-width;
  }
}
</style>
