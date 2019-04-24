<template>
  <q-card style="min-width: 35vw">
    <q-card-section>
      <div>
        <app-input
          i18n
          label="loginform.lbl.username"
          icon="perm_identity"
          :validation="validations.username"
          errormessage="loginform.error.username"
          v-on:value_changed="(value) => {this.form.username = value}"
          v-on:value_valid="(value) => {this.valid.username = value}"
        />
      </div>
      <div>
        <app-passwordinput
          i18n
          label="loginform.lbl.password"
          :validation="validations.password"
          errormessage="loginform.error.password"
          v-on:value_changed="(value) => {this.form.password = value}"
          v-on:value_valid="(value) => {this.valid.password = value}"
        />
      </div>
    </q-card-section>
    <q-card-actions>
      <div class="col">
        <q-btn
          color="primary"
          class="full-width"
          @click="loginBtnClk"
          :disable="!this.isValid"
        >
          {{$t('loginform.btn.login')}}
        </q-btn>
      </div>
    </q-card-actions>
    <div>
      <app-horizontalrule>
        OR
      </app-horizontalrule>
    </div>
    <q-card-actions>
      <div class="col-6 q-px-xs">
        <q-btn
          color="primary"
          class="full-width"
          @click="createAccountBtnClk"
        >
          {{$t('loginform.btn.createaccount')}}
        </q-btn>
      </div>
      <div class="col-6 q-px-xs">
        <q-btn
          class="full-width"
          @click="forgotPasswordBtnClk"
        >
          {{$t('loginform.btn.forgotpassword')}}
        </q-btn>
      </div>
    </q-card-actions>
  </q-card>
</template>

<script>
import { required } from 'vuelidate/lib/validators'

import AppHorizontalRule from 'components/common/presentation/HorizontalRule'
import AppInput from 'components/common/form/Input'
import AppPasswordInput from 'components/common/form/PasswordInput'

export default {
  name: 'LoginForm',
  props: {
    onLoginBtnClk: {
      type: Function,
      required: true
    },
    onCreateAccountBtnClk: {
      type: Function,
      required: true
    },
    onForgotPasswordBtnClk: {
      type: Function,
      required: true
    }
  },
  components: {
    'app-horizontalrule': AppHorizontalRule,
    'app-input': AppInput,
    'app-passwordinput': AppPasswordInput
  },
  data: function () {
    return {
      form: {
        username: '',
        password: ''
      },
      validations: {
        username: { required },
        password: { required }
      },
      valid: {
        username: false,
        password: false
      }
    }
  },
  methods: {
    loginBtnClk: function () {
      this.onLoginBtnClk(this.form.username, this.form.password)
    },
    createAccountBtnClk: function () {
      this.onCreateAccountBtnClk()
    },
    forgotPasswordBtnClk: function () {
      this.onForgotPasswordBtnClk()
    }
  },
  computed: {
    isValid: function () {
      return this.valid.username && this.valid.password
    }
  }
}
</script>

<style>
</style>
