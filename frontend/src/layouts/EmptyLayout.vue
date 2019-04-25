<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="header">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar square>
            <img src="statics/logo.png">
          </q-avatar>
          {{title}}
        </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script>
import { mapGetters, mapActions, mapState } from 'vuex'

export default {
  name: 'EmptyLayout',
  data () {
    return {
    }
  },
  methods: {
    ...mapActions('application', [
      'unsetRedirectTo'
    ])
  },
  computed: {
    title: function () {
      return process.env.WEBSITE_NAME
    },
    ...mapState('application', [
      'redirectTo'
    ]),
    ...mapGetters('application', [
      'isLoggedIn',
      'shouldBeRedirected'
    ])
  },
  created: function () {
    // If user is already logged in, redirect him.
    if (this.isLoggedIn) {
      if (this.shouldBeRedirected) {
        this.$router.push(this.redirectTo)
        this.unsetRedirectTo()
      } else {
        this.$router.push('/')
      }
    }
  }
}
</script>

<style>
</style>
