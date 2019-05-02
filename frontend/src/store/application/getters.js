export function isLoggedIn (state) {
  return !!state.token && !!state.user
}

export function shouldBeRedirected (state) {
  return !!state.redirectTo
}
export function isClientSecurityGenerated (state) {
  return !!state.security.clientSecretKey && !!state.security.clientPublicKey
}

export function isServerSecurityReceived (state) {
  return !!state.security.serverPublicKey
}
