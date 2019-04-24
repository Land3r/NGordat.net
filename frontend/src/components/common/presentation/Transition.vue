<template>
  <transition
    appear
    enter-active-class="animated flipInX"
  >
    <slot></slot>
  </transition>
</template>

<style>
</style>

<script>
/**
 * Transition component.
 * Allow a SINGLE CHILD to use a transition on appearance.
 */
export default {
  name: 'AppTransition'
}
</script>
