<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-createaccountform
        :onCreateAccountBtnClk="doCreateAccount"
        :onCancelBtnClk="() => { $router.go(-1) }"
      />
    </app-transition>
  </q-page>
</template>

<style>
</style>

<script>
import AppTransition from 'components/common/presentation/Transition'
import { NotifyError, NotifySuccess } from 'helpers/notify'

import CreateAccountForm from 'components/form/login/CreateAccountForm'
import UserService from 'services/UserService'

export default {
  name: 'PageCreateAccount',
  components: {
    'app-transition': AppTransition,
    'app-createaccountform': CreateAccountForm
  },
  methods: {
    doCreateAccount: function (user) {
      const service = new UserService()
      service.create(user).then((response) => {
        // User creation success
        this.$q.notify({
          ...NotifySuccess,
          message: this.$t('createaccountpage.success.createsuccess')
        })
        this.$router.push('/login')
      }).catch((error) => {
        if (error) {
          // User creation failure
          this.$q.notify({
            ...NotifyError,
            message: this.$t('createaccountpage.error.createfailure')
          })
        }
      })
    }
  }
}
</script>
