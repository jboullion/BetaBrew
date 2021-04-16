<template>
  <div class="btn-link">
    <button
      @click="onClick"
      :class="['beta-button', { 'full-width': isFullWidth }]"
      type="button"
      :disabled="disabled"
    >
      <slot></slot>
    </button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
import { Prop } from 'vue-property-decorator'

@Component({
  name: 'BetaButton',
  components: {},
})
export default class BetaButton extends Vue {
  @Prop({ required: false, type: Boolean, default: false })
  isFullWidth?: boolean

  @Prop({ required: false, type: Boolean, default: false })
  disabled?: boolean

  onClick(): void {
    this.$emit('button:click')
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import '@/scss/global.scss';

.beta-button {
  background-color: lighten($brand-blue, 10%);
  color: white;
  cursor: pointer;
  padding: 0.8rem;
  transition: background-color 0.2s;
  margin: 0.3rem 0.2rem;
  display: inline-block;
  font-size: 1rem;
  min-width: 100px;
  border: none;
  border-bottom: 2px solid darken($brand-blue, 20%);
  border-radius: 3px;
  outline: 0;

  &:hover {
    background: lighten($brand-blue, 20%);
  }

  &:disabled {
    background: lighten($brand-blue, 50%);
    border-bottom: 2px solid lighten($brand-blue, 20%);
  }

  &:active,
  &.active {
    background: $brand-green;
    border-bottom: 2px solid darken($brand-green, 20%);
  }
}

.full-width {
  display: block;
  width: 100%;
}
</style>
