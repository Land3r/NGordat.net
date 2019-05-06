import NodeRSA from 'node-rsa'
import Crypto from 'crypto'

/**
 * SecurityHelper class.
 * Used to handle application security layer (using RSA pkcs1)
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

  /**
   * Encrypts some data with a RSA public key.
   * @param {string} key The public key to use to encrypt data.
   * @param {string} data The data to encrypt.
   */
  encryptWithPublicKey (key, data) {
    let buffer = Buffer.from(data)
    let encrypted = Crypto.publicEncrypt(key, buffer)
    return encrypted.toString('base64')
  }

  /**
   * Decrypts some data with a RSA private key.
   * @param {string} key The private key to use the decrypt data.
   * @param {base64string} base64data The base64 encoded data to decrypt.
   */
  decryptWithPrivateKey (key, base64data) {
    let buffer = Buffer.from(base64data, 'base64')
    let decrypted = Crypto.privateDecrypt(key, buffer)
    return decrypted.toString('utf8')
  }
}
