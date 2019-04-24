import {
  SET_TOKEN, UNSET_TOKEN,
  SET_USER, UNSET_USER
} from './types'

export function setToken (context, token) {
  context.commit(SET_TOKEN, token)
}

export function unsetToken (context) {
  context.commit(UNSET_TOKEN)
}

export function setUser (context, user) {
  context.commit(SET_USER, user)
}

export function unsetUser (context) {
  context.commit(UNSET_USER)
}
