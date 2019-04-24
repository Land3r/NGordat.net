<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-loginform
        :onLoginBtnClk="doLogin"
        :onCreateAccountBtnClk="() => { $router.push('/createaccount') }"
        :onLostPasswordBtnClk="() => { $router.push('/lostpassword') }"
      />
    </app-transition>
  </q-page>
</template>

<style>
</style>

<script>
import AppTransition from 'components/common/presentation/Transition'

import LoginForm from 'components/form/login/LoginForm'
import AuthService from 'services/AuthService'

export default {
  name: 'PageLogin',
  components: {
    'app-transition': AppTransition,
    'app-loginform': LoginForm
  },
  methods: {
    doLogin: function (username, password) {
      const service = new AuthService()
      const response = service.doLogin(username, password)
      console.log(response)
    }
  }
}
</script>
