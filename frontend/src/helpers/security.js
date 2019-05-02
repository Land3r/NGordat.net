import NodeRSA from 'node-rsa'

/**
 * SecurityHelper class.
 * Used to handle application security layer.
 */
export default class SecurityHelper {
  /**
   * Returns a new public/private keypair.
   */
  generatePrivatePublicKeys () {
    const key = new NodeRSA().generateKeyPair(2048, 65537)
    const privateKey = key.exportKey('pkcs1-private')
    const publicKey = key.exportKey('pkcs1-public')

    return { private: privateKey, public: publicKey }
  }
}
