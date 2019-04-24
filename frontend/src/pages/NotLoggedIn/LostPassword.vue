<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-lostpasswordform
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

import LostPasswordForm from 'components/form/login/LostPasswordForm'
import UserService from 'services/UserService'

export default {
  name: 'PageLostPassword',
  components: {
    'app-transition': AppTransition,
    'app-lostpasswordform': LostPasswordForm
  },
  methods: {
    doSendEmail: function (identity) {
      if (identity.username !== undefined) {
        const service = new UserService()
        const response = service.forgotPasswordUsername(identity.username)
        console.log(response)
      } else if (identity.email !== undefined) {
        const service = new UserService()
        const response = service.forgotPasswordEmail(identity.email)
        console.log(response)
      }
    }
  }
}
</script>
