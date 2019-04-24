<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-forgotpasswordform
        :onSendEmailBtnClk="doSendEmail"
        :onCancelBtnClk="() => { $router.go(-1) }"
      />
    </app-transition>
  </q-page>
</template>

<style>
</style>

<script>
import AppTransition from 'components/common/presentation/Transition'

import ForgotPasswordForm from 'components/form/login/ForgotPasswordForm'

export default {
  name: 'PageForgotPassword',
  components: {
    'app-transition': AppTransition,
    'app-forgotpasswordform': ForgotPasswordForm
  },
  methods: {
    doSendEmail: function () {
      // TODO
    }
  }
}
</script>
