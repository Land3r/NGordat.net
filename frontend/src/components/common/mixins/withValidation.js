import { VALUE_VALID } from './emit'

/**
 * Mixin to allow a component to have vuelidate validation passed as a prop.
 */
const withValidation = {
  props: {
    validation: {
      type: Object,
      required: false,
      default: () => ({})
    },
    errormessage: {
      type: String,
      required: false,
      default: ''
    }
  },
  validations () {
    return {
      value: { ...this.validation }
    }
  },
  computed: {
    isValid: function () {
      return !this.$v.$invalid
    }
  },
  watch: {
    isValid: function (newValue, oldValue) {
      if (newValue === false) {
        this.$emit(VALUE_VALID, false)
      } else if (newValue === true) {
        this.$emit(VALUE_VALID, true)
      }
    }
  }
}

export default withValidation
