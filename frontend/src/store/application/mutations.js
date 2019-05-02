import {
  SET_TOKEN, UNSET_TOKEN,
  SET_USER, UNSET_USER,
  SET_REDIRECT_TO, UNSET_REDIRECT_TO,
  SET_SECURITY_CLIENT_PRIVATE_KEY, UNSET_SECURITY_CLIENT_PRIVATE_KEY, SET_SECURITY_CLIENT_PUBLIC_KEY, UNSET_SECURITY_CLIENT_PUBLIC_KEY, SET_SECURITY_CLIENT_KEYS, UNSET_SECURITY_CLIENT_KEYS, SET_SECURITY_SERVER_PUBLIC_KEY, UNSET_SECURITY_SERVER_PUBLIC_KEY
} from './types'

export const mutations = {
  [SET_TOKEN] (state, token) {
    state.token = token
  },
  [UNSET_TOKEN] (state) {
    state.token = null
  },
  [SET_USER] (state, user) {
    state.user = { ...user }
  },
  [UNSET_USER] (state) {
    state.user = null
  },
  [SET_REDIRECT_TO] (state, route) {
    state.redirectTo = route
  },
  [UNSET_REDIRECT_TO] (state) {
    state.redirectTo = null
  },
  [SET_SECURITY_CLIENT_PRIVATE_KEY] (state, key) {
    state.security.clientPrivateKey = key
  },
  [UNSET_SECURITY_CLIENT_PRIVATE_KEY] (state) {
    state.security.clientPrivateKey = null
  },
  [SET_SECURITY_CLIENT_PUBLIC_KEY] (state, key) {
    state.security.clientPublicKey = key
  },
  [UNSET_SECURITY_CLIENT_PUBLIC_KEY] (state) {
    state.security.clientPublicKey = null
  },
  [SET_SECURITY_CLIENT_KEYS] (state, { privateKey, publicKey }) {
    state.security.clientPrivateKey = privateKey
    state.security.clientPublicKey = publicKey
  },
  [UNSET_SECURITY_CLIENT_KEYS] (state) {
    state.security.clientPublicKey = null
  },
  [SET_SECURITY_SERVER_PUBLIC_KEY] (state, key) {
    state.security.serverPublicKey = key
  },
  [UNSET_SECURITY_SERVER_PUBLIC_KEY] (state) {
    state.security.serverPublicKey = null
  }
}
