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

import CreateAccountForm from 'components/form/login/CreateAccountForm'

export default {
  name: 'PageCreateAccount',
  components: {
    'app-transition': AppTransition,
    'app-createaccountform': CreateAccountForm
  },
  methods: {
    doCreateAccount: function () {
      // TODO
    }
  }
}
</script>
