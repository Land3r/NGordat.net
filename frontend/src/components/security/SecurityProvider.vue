<script>
import { mapActions, mapGetters } from 'vuex'

import SecurityHelper from 'helpers/security'
import SecurityService from 'services/SecurityService'

export default {
  name: 'SecurityProvider',
  props: {},
  data: function () {
    return {}
  },
  computed: {
    ...mapGetters('application', [
      'isServerSecurityReceived',
      'isClientSecurityGenerated'
    ])
  },
  methods: {
    generateClientPrivatePublicKeysIfNeeded: function () {
      if (!this.isClientSecurityGenerated) {
        // Generate keys.
        let securityHelper = new SecurityHelper()
        const keys = securityHelper.generatePrivatePublicKeys()
        this.setClientKeys(keys)

        // Send public key to server.
        const securityService = new SecurityService()
        securityService.doSendPublicKey(keys.public)
      }
    },
    ...mapActions('application', [
      'setClientKeys',
      'setServerPublicKey'
    ])
  },
  created: function () {
    this.generateClientPrivatePublicKeysIfNeeded()
  },
  updated: function () {
    this.generateClientPrivatePublicKeysIfNeeded()
  },
  render () {
    return this.$slots.default
  }
}
</script>
