using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace ngordat.net.backend.security
{
  /// <summary>
  /// SecurityProvider class.
  /// </summary>
  public static class SecurityProvider
  {
    /// <summary>
    /// Generates a new public/private key pair using RSA
    /// </summary>
    /// <param name="length">(Optional) The length of the key. Defaults to 2048.</param>
    /// <returns>A tuple with the private and public keys as bytes.</returns>
    public static (RSAParameters privateKey, RSAParameters publicKey) GeneratePrivatePublicPair(int length = 2048)
    {
      // Lets take a new CSP with a new rsa key pair
      RSACryptoServiceProvider csp = new RSACryptoServiceProvider(length);

      // The private key
      RSAParameters privKey = csp.ExportParameters(true);

      // The public key ...
      RSAParameters pubKey = csp.ExportParameters(false);

      return (privateKey: privKey, publicKey: pubKey);
    }

    #region Utils

    /// <summary>
    /// Converts a <see cref="RSAParameters"/> to <see cref="string"/>.
    /// </summary>
    /// <param name="key">The <see cref="RSAParameters"/> to get the string from.</param>
    /// <returns>The key serialized as a string.</returns>
    public static string KeyToString(RSAParameters key)
    {
      // We need some buffer and a serializer.
      StringWriter sw = new StringWriter();
      XmlSerializer xs = new XmlSerializer(typeof(RSAParameters));

      // Serialize the key into the stream.
      xs.Serialize(sw, key);

      // Returns the string from the stream.
      return sw.ToString();
    }

    /// <summary>
    /// Converts a <see cref="string"/> to <see cref="RSAParameters"/>.
    /// </summary>
    /// <param name="key">The string to get the <see cref="RSAParameters"/> from.</param>
    /// <returns>The <see cref="RSAParameters"/> deserialized from a string.</returns>
    public static RSAParameters StringToKey(string keyString)
    {
      // Get a stream from the string, and a serializer.
      StringReader sr = new StringReader(keyString);
      XmlSerializer xs = new XmlSerializer(typeof(RSAParameters));

      // Returns the RSAParameters from the string.
      return (RSAParameters)xs.Deserialize(sr);
    }

#endregion Utils

    #region Encrypt

    /// <summary>
    /// Encrypts data using a key.
    /// </summary>
    /// <param name="encryptKey">The key to use to encrypt data as a string.</param>
    /// <param name="clearData">The data to encrypt.</param>
    /// <returns>The data encrypted, wrapped in base64.</returns>
    public static string Encrypt(string encryptKey, string clearData)
    {
      RSAParameters key = StringToKey(encryptKey);
      return Encrypt(key, clearData);
    }

    /// <summary>
    /// Encrypts data using a key.
    /// </summary>
    /// <param name="encryptKey">The key to use to encrypt data as a <see cref="RSAParameters"/>.</param>
    /// <param name="clearData">The data to encrypt.</param>
    /// <returns>The data encrypted, wrapped in base64.</returns>
    public static string Encrypt(RSAParameters encryptKey, string clearData)
    {
      RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
      csp.ImportParameters(encryptKey);

      // For encryption, always handle bytes...
      byte[] bytesData = System.Text.Encoding.Unicode.GetBytes(clearData);
      
      // Apply pkcs#1.5 padding and encrypt our data 
      byte[] bytesCypheredData = csp.Encrypt(bytesData, true);

      // And convert back bytes into string using base64.
      return Convert.ToBase64String(bytesCypheredData);
    }

    #endregion Encrypt

    #region Decrypt

    /// <summary>
    /// Decrypts data using a key.
    /// </summary>
    /// <param name="decryptKey">The key to use to decrypt data as a string.</param>
    /// <param name="cipheredData">The data to decrypt, wrapped in a base64.</param>
    /// <returns>The data decrypted.</returns>
    public static string Decrypt(string decryptKey, string cipheredData)
    {
      RSAParameters key = StringToKey(decryptKey);
      return Decrypt(key, cipheredData);
    }

    /// <summary>
    /// Decrypts data using a key.
    /// </summary>
    /// <param name="decryptKey">The key to use to decrypt data as a string.</param>
    /// <param name="cipheredData">The data to decrypt, wrapped in a base64.</param>
    /// <returns>The data decrypted.</returns>
    public static string Decrypt(RSAParameters decryptKey, string cipheredData)
    {
      // First, get our bytes back from the base64 string ...
      byte[] bytesCypherText = Convert.FromBase64String(cipheredData);

      // We want to decrypt, therefore we need a csp and load our private key
      RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
      csp.ImportParameters(decryptKey);

      // Decrypt and strip pkcs#1.5 padding
      byte[] bytesPlainTextData = csp.Decrypt(bytesCypherText, true);

      // Get our original plainText back...
      return System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
    }

    #endregion Decrypt
  }
}
