<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-loginform
        :onLoginBtnClk="doLogin"
        :onCreateAccountBtnClk="() => { $router.push('/createaccount') }"
        :onForgotPasswordBtnClk="() => { $router.push('/forgotpassword') }"
      />
    </app-transition>
  </q-page>
</template>

<style>
</style>

<script>
import AppTransition from 'components/common/presentation/Transition'

import LoginForm from 'components/form/login/LoginForm'

export default {
  name: 'PageLogin',
  components: {
    'app-transition': AppTransition,
    'app-loginform': LoginForm
  },
  methods: {
    doLogin: function () {
      // TODO
    }
  }
}
</script>
