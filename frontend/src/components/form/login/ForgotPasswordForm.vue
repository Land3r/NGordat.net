 <template>
  <q-card style="min-width: 35vw">
    <q-card-section>
      <p>{{$t('forgotpasswordform.fillusernameofemail')}}</p>
    </q-card-section>
    <q-card-section>
      <div>
        <app-input
          i18n
          label="forgotpasswordform.lbl.username"
          icon="perm_identity"
          :validation="validations.username"
          errormessage="forgotpasswordform.error.username"
          v-on:value_changed="(value) => {this.form.username = value}"
          v-on:value_valid="(value) => {this.valid.username = value}"
        />
      </div>
      <div>
        <app-horizontalrule>
          OR
        </app-horizontalrule>
      </div>
      <div>
        <app-input
          i18n
          label="forgotpasswordform.lbl.email"
          icon="alternate_email"
          :validation="validations.email"
          errormessage="forgotpasswordform.error.email"
          v-on:value_changed="(value) => {this.form.email = value}"
          v-on:value_valid="(value) => {this.valid.email = value}"
        />
      </div>
    </q-card-section>
    <q-card-actions>
      <div class="col-6 q-px-xs">
        <q-btn
          color="primary"
          class="full-width"
          @click="sendEmailBtnClk"
          :disable="!this.isValid"
        >
          {{$t('forgotpasswordform.btn.sendemail')}}
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

export default {
  name: 'ForgotPasswordForm',
  props: {
    onSendEmailBtnClk: {
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
    'app-input': AppInput
  },
  data: function () {
    return {
      form: {
        username: '',
        email: ''
      },
      validations: {
        username: { required },
        email: { required, email }
      },
      valid: {
        username: false,
        email: false
      }
    }
  },
  methods: {
    sendEmailBtnClk: function () {
      // TODO: Check if valid
      this.sendEmailBtnClk()
    },
    cancelBtnClk: function () {
      this.onCancelBtnClk()
    }
  },
  computed: {
    isValid: function () {
      return this.valid.username || this.valid.email
    }
  }
}
</script>

<style>
</style>
