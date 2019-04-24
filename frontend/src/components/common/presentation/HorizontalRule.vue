<template>
  <div class="strike">
    <span>
      <slot></slot>
    </span>
  </div>
</template>

<style>
.strike {
  display: block;
  text-align: center;
  overflow: hidden;
  white-space: nowrap;
  padding: 1em 0em 1em 0em;
}

.strike > span {
  position: relative;
  display: inline-block;
}

.strike > span:before,
.strike > span:after {
  content: "";
  position: absolute;
  top: 50%;
  width: 9999px;
  height: 1px;
  background: rgba(0, 0, 0, 0.6);
}

.strike > span:before {
  right: 100%;
  margin-right: 15px;
}

.strike > span:after {
  left: 100%;
  margin-left: 15px;
}
</style>

<script>
/**
 * Horizontal rule component.
 * Allow some text to be placed inside an <hr/> tag.
 */
export default {
  name: 'AppHorizontalRule'
}
</script>
