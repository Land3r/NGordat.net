import { VALUE_CHANGED } from './emit'

/**
 * Mixin to allow a component to emit event 'value_changed' when value has changed.
 */
const onValueChanged = {
  watch: {
    value: function (newValue, oldValue) {
      this.$emit(VALUE_CHANGED, newValue)
    }
  }
}

export default onValueChanged
