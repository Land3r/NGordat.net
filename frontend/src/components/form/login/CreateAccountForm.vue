<template>
  <q-card style="min-width: 35vw">
    <q-card-section>
      <img src="statics/icons/icon-128x128.png" class="q-mx-auto block" />
    </q-card-section>
    <q-card-section>
      <div>
        <app-horizontalrule>
          {{$t('createaccountform.section.identity')}}
        </app-horizontalrule>
      </div>
      <div>
        <app-input
          i18n
          label="createaccountform.lbl.username"
          icon="perm_identity"
          :validation="validations.username"
          errormessage="createaccountform.error.username"
          v-on:value_changed="(value) => {this.form.username = value}"
          v-on:value_valid="(value) => {this.valid.username = value}"
        />
      </div>
      <div class="row">
        <div class="col q-pr-xs">
          <app-input
            i18n
            label="createaccountform.lbl.firstname"
            icon="perm_identity"
            :validation="validations.firstname"
            errormessage="createaccountform.error.firstname"
            v-on:value_changed="(value) => {this.form.firstname = value}"
            v-on:value_valid="(value) => {this.valid.firstname = value}"
          />
        </div>
        <div class="col q-pl-xs">
          <app-input
            i18n
            label="createaccountform.lbl.lastname"
            icon="perm_identity"
            :validation="validations.lastname"
            errormessage="createaccountform.error.lastname"
            v-on:value_changed="(value) => {this.form.lastname = value}"
            v-on:value_valid="(value) => {this.valid.lastname = value}"
          />
        </div>
      </div>
      <div>
        <app-input
          i18n
          label="createaccountform.lbl.email"
          icon="alternate_email"
          :validation="validations.email"
          errormessage="createaccountform.error.email"
          v-on:value_changed="(value) => {this.form.email = value}"
          v-on:value_valid="(value) => {this.valid.email = value}"
        />
      </div>
      <div>
        <app-input
          i18n
          label="createaccountform.lbl.email2"
          icon="alternate_email"
          :validation="validations.email"
          errormessage="createaccountform.error.email2"
          v-on:value_changed="(value) => {this.form.email2 = value}"
          v-on:value_valid="(value) => {this.valid.email2 = value}"
        />
      </div>
      <div>
        <app-horizontalrule>
          {{$t('createaccountform.section.password')}}
        </app-horizontalrule>
      </div>
      <div>
        <app-passwordinput
          i18n
          label="createaccountform.lbl.password"
          :validation="validations.password"
          errormessage="createaccountform.error.password"
          v-on:value_changed="(value) => {this.form.password = value}"
          v-on:value_valid="(value) => {this.valid.password = value}"
        />
      </div>
      <div>
        <app-passwordinput
          i18n
          label="createaccountform.lbl.password2"
          :validation="validations.password"
          errormessage="createaccountform.error.password2"
          v-on:value_changed="(value) => {this.form.password2 = value}"
          v-on:value_valid="(value) => {this.valid.password2 = value}"
        />
      </div>
    </q-card-section>
    <q-card-actions>
      <div class="col-6 q-px-xs">
        <q-btn
          color="primary"
          class="full-width"
          @click="createAccountBtnClk"
          :disable="!this.isValid"
        >
          {{$t('createaccountform.btn.createaccount')}}
        </q-btn>
      </div>
      <div class="col-6 q-px-xs">
        <q-btn
          class="full-width"
          @click="cancelBtnClk"
        >
          {{$t('btn.cancel')}}
        </q-btn>
      </div>
    </q-card-actions>
  </q-card>
</template>

<script>
import { required, email } from 'vuelidate/lib/validators'

import AppHorizontalRule from 'components/common/presentation/HorizontalRule'
import AppInput from 'components/common/form/Input'
import AppPasswordInput from 'components/common/form/PasswordInput'

export default {
  name: 'CreateAccountForm',
  props: {
    onCreateAccountBtnClk: {
      type: Function,
      required: true
    },
    onCancelBtnClk: {
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
        firstname: '',
        lastname: '',
        email: '',
        email2: '',
        password: '',
        password2: ''
      },
      validations: {
        username: { required },
        firstname: { required },
        lastname: { required },
        email: { required, email },
        password: { required }
      },
      valid: {
        username: false,
        firstname: false,
        lastname: false,
        email: false,
        email2: false,
        password: false,
        password2: false
      }
    }
  },
  methods: {
    createAccountBtnClk: function () {
      const user = {
        username: this.form.username,
        firstname: this.form.firstname,
        lastname: this.form.lastname,
        email: this.form.email,
        password: this.form.password
      }
      this.onCreateAccountBtnClk(user)
    },
    cancelBtnClk: function () {
      this.onCancelBtnClk()
    }
  },
  computed: {
    isValid: function () {
      return this.valid.username && this.valid.firstname && this.valid.lastname && this.valid.email && this.valid.email2 && this.valid.password && this.valid.password2 &&
        this.form.email === this.form.email2 && this.form.password === this.form.password2
    }
  }
}
</script>

<style>
</style>
