<template>
  <q-input
    :type="type"
    v-model="value"
    :label="i18n ? $t(label) : label"
    @blur="$v.value.$touch"
    :error="$v.value.$error"
    bottom-slots
    :error-message="i18n ? $t(errormessage) : errormessage"
  >
    <template
      v-if="icon"
      v-slot:prepend
    >
      <q-icon :name="icon" />
    </template>
  </q-input>
</template>

<style>
</style>

<script>
import onValueChanged from 'components/common/mixins/onValueChanged'
import withValidation from 'components/common/mixins/withValidation'

/**
 * Generic input component.
 * Supports:
 * - Internationalisation using vue-i18n
 * - Display an icon in the filed if prop icon is specified
 * - Allow validation using vee-validate validation
 * - Emit value_changed to transmit new value
 * - Emit value_invalid to tell parent component that current value is invalid
 */
export default {
  name: 'AppInput',
  mixins: [onValueChanged, withValidation],
  props: {
    label: {
      type: String,
      required: true
    },
    i18n: {
      type: Boolean,
      required: false,
      default: false
    },
    icon: {
      type: String,
      required: false
    },
    initialValue: {
      type: [String, Number],
      required: false,
      default: ''
    },
    type: {
      type: String,
      required: false,
      default: 'text'
    }
  },
  data: function () {
    return {
      value: this.initialValue
    }
  }
}
</script>
