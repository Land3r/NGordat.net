<template>
  <q-input
    v-model="value"
    :type="showPwd ? 'text' : 'password'"
    :label="i18n ? $t(label) : label"
    @blur="$v.value.$touch"
    :error="$v.value.$error"
    bottom-slots
    :error-message="i18n ? $t(errormessage) : errormessage"
  >
    <template v-slot:append>
      <q-icon
        :name="showPwd ? 'visibility_off' : 'visibility'"
        class="cursor-pointer" @click="showPwd = !showPwd"
      />
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
 * - Display a 'show password' button to show password in clear text
 * - Allow validation using vee-validate validation
 * - Emit value_changed to transmit new value
 * - Emit value_invalid to tell parent component that current value is invalid
 */
export default {
  name: 'AppPasswordInput',
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
    initialValue: {
      type: String,
      required: false,
      default: ''
    }
  },
  data: function () {
    return {
      value: this.initialValue,
      showPwd: false
    }
  }
}
</script>
