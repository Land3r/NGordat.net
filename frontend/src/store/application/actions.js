import {
  SET_TOKEN, UNSET_TOKEN,
  SET_USER, UNSET_USER,
  SET_REDIRECT_TO, UNSET_REDIRECT_TO,
  SET_SECURITY_CLIENT_PRIVATE_KEY, UNSET_SECURITY_CLIENT_PRIVATE_KEY, SET_SECURITY_CLIENT_PUBLIC_KEY, UNSET_SECURITY_CLIENT_PUBLIC_KEY, SET_SECURITY_CLIENT_KEYS, UNSET_SECURITY_CLIENT_KEYS, SET_SECURITY_SERVER_PUBLIC_KEY, UNSET_SECURITY_SERVER_PUBLIC_KEY
} from './types'

export function connectUser (context, { user, token }) {
  context.commit(SET_TOKEN, token)
  context.commit(SET_USER, user)
}

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

export function setRedirectTo (context, route) {
  context.commit(SET_REDIRECT_TO, route)
}

export function unsetRedirectTo (context) {
  context.commit(UNSET_REDIRECT_TO)
}

export function setClientPrivateKey (context, key) {
  context.commit(SET_SECURITY_CLIENT_PRIVATE_KEY, key)
}

export function unsetClientPrivateKey (context) {
  context.commit(UNSET_SECURITY_CLIENT_PRIVATE_KEY)
}

export function setClientPublicKey (context, key) {
  context.commit(SET_SECURITY_CLIENT_PUBLIC_KEY, key)
}

export function unsetClientPublicKey (context) {
  context.commit(UNSET_SECURITY_CLIENT_PUBLIC_KEY)
}

export function setClientKeys (context, keys) {
  context.commit(SET_SECURITY_CLIENT_KEYS, keys)
}

export function unsetClientKeys (context) {
  context.commit(UNSET_SECURITY_CLIENT_KEYS)
}

export function setServerPublicKey (context, key) {
  context.commit(SET_SECURITY_SERVER_PUBLIC_KEY, key)
}

export function unsetServerPublicKey (context) {
  context.commit(UNSET_SECURITY_SERVER_PUBLIC_KEY)
}
