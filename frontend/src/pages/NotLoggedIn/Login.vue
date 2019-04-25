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
import { mapActions, mapGetters, mapState } from 'vuex'

import AppTransition from 'components/common/presentation/Transition'
import { NotifyError, NotifySuccess } from 'helpers/notify'

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
      service.doLogin(username, password).then((response) => {
        // Login success
        const user = {
          id: response.id,
          username: response.username,
          email: response.email,
          firstname: response.firstname,
          lastname: response.lastname
        }
        const token = response.token
        this.connectUser({ user: user, token: token })
        this.$q.notify({
          ...NotifySuccess,
          message: this.$t('loginpage.success.loginsuccess')
        })
        if (this.shouldBeRedirected) {
          this.$router.push(this.redirectTo)
          this.unsetRedirectTo()
        } else {
          this.$router.push('/')
        }
      }).catch((error) => {
        if (error) {
          // Login failure
          this.$q.notify({
            ...NotifyError,
            message: this.$t('loginpage.error.loginfailure')
          })
          // TODO: Reset pwd ?
        }
      })
    },
    ...mapActions('application', [
      'connectUser',
      'unsetRedirectTo'
    ])
  },
  computed: {
    ...mapState('application', [
      'redirectTo'
    ]),
    ...mapGetters('application', [
      'shouldBeRedirected'
    ])
  }
}
</script>
