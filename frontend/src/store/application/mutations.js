import {
  SET_TOKEN, UNSET_TOKEN,
  SET_USER, UNSET_USER
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
  }
}
