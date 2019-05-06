using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ngordat.net.backend.security
{
  /// <summary>
  /// SecurityProvider class.
  /// </summary>
  public static class SecurityProvider
  {

    #region Signature

    public static string Sign(string data, string privateKey, Encoding encoding, HashAlgorithmName hashAlgorithmName)
    {
      byte[] dataBytes = encoding.GetBytes(data);

      RSA rsa = CreateRsaProviderFromPrivateKey(privateKey);

      var signatureBytes = rsa.SignData(dataBytes, hashAlgorithmName, RSASignaturePadding.Pkcs1);

      return Convert.ToBase64String(signatureBytes);
    }

    public static bool Verify(string data, string sign, string publicKey, Encoding encoding, HashAlgorithmName hashAlgorithmName)
    {
      byte[] dataBytes = encoding.GetBytes(data);
      byte[] signBytes = Convert.FromBase64String(sign);
      RSA rsa = CreateRsaProviderFromPublicKey(publicKey);
      var verify = rsa.VerifyData(dataBytes, signBytes, hashAlgorithmName, RSASignaturePadding.Pkcs1);
      return verify;
    }

    #endregion Signature

    #region Encryption

    public static string Decrypt(string source, string privateKey)
    {
      RSA rsa = CreateRsaProviderFromPrivateKey(privateKey);
      return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(source), RSAEncryptionPadding.Pkcs1));
    }

    public static string Decrypt(byte[] source, byte[] privateKey)
    {
      string sourceString = Encoding.UTF8.GetString(source, 0, source.Length);
      string privateKeyString = Encoding.UTF8.GetString(privateKey, 0, privateKey.Length);
      return Decrypt(sourceString, privateKeyString);
    }


    public static string Encrypt(string text, string publicKey)
    {
      RSA rsa = CreateRsaProviderFromPublicKey(publicKey);
      return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
    }

    public static string Encrypt(byte[] data, byte[] publicKey)
    {
      string dataString = Encoding.UTF8.GetString(data, 0, data.Length);
      string publicKeyString = Encoding.UTF8.GetString(publicKey, 0, publicKey.Length);
      return Encrypt(dataString, publicKeyString);
    }

    #endregion Encryption

    #region Provider

    /// <summary>
    /// Generates a new public/private key pair using RSA
    /// </summary>
    /// <param name="length">(Optional) The length of the key. Defaults to 2048.</param>
    /// <returns>A tuple with the private and public keys as bytes.</returns>
    public static (byte[] privateKey, byte[] publicKey) GeneratePrivatePublicPair(int length = 2048)
    {
      // Lets take a new CSP with a new rsa key pair
      RSACryptoServiceProvider csp = new RSACryptoServiceProvider(length);

      // The private key
      byte[] privKey = ExportPrivateKey(csp);

      // The public key ...
      byte[] pubKey = ExportPublicKey(csp);

      return (privateKey: privKey, publicKey: pubKey);
    }

    public static RSA CreateRsaProviderFromPrivateKey(string privateKey)
    {
      var privateKeyBits = Convert.FromBase64String(privateKey);

      var rsa = RSA.Create();
      var rsaParameters = new RSAParameters();

      using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
      {
        byte bt = 0;
        ushort twobytes = 0;
        twobytes = binr.ReadUInt16();
        if (twobytes == 0x8130)
          binr.ReadByte();
        else if (twobytes == 0x8230)
          binr.ReadInt16();
        else
          throw new Exception("Unexpected value read binr.ReadUInt16()");

        twobytes = binr.ReadUInt16();
        if (twobytes != 0x0102)
          throw new Exception("Unexpected version");

        bt = binr.ReadByte();
        if (bt != 0x00)
          throw new Exception("Unexpected value read binr.ReadByte()");

        rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
        rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
      }

      rsa.ImportParameters(rsaParameters);
      return rsa;
    }

    public static RSA CreateRsaProviderFromPublicKey(string publicKeyString)
    {
      // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
      byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
      byte[] seq = new byte[15];

      var x509Key = Convert.FromBase64String(publicKeyString);

      // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
      using (MemoryStream mem = new MemoryStream(x509Key))
      {
        using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
        {
          byte bt = 0;
          ushort twobytes = 0;

          twobytes = binr.ReadUInt16();
          if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
            binr.ReadByte();    //advance 1 byte
          else if (twobytes == 0x8230)
            binr.ReadInt16();   //advance 2 bytes
          else
            return null;

          seq = binr.ReadBytes(15);       //read the Sequence OID
          if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
            return null;

          twobytes = binr.ReadUInt16();
          if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
            binr.ReadByte();    //advance 1 byte
          else if (twobytes == 0x8203)
            binr.ReadInt16();   //advance 2 bytes
          else
            return null;

          bt = binr.ReadByte();
          if (bt != 0x00)     //expect null byte next
            return null;

          twobytes = binr.ReadUInt16();
          if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
            binr.ReadByte();    //advance 1 byte
          else if (twobytes == 0x8230)
            binr.ReadInt16();   //advance 2 bytes
          else
            return null;

          twobytes = binr.ReadUInt16();
          byte lowbyte = 0x00;
          byte highbyte = 0x00;

          if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
            lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
          else if (twobytes == 0x8202)
          {
            highbyte = binr.ReadByte(); //advance 2 bytes
            lowbyte = binr.ReadByte();
          }
          else
            return null;
          byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
          int modsize = BitConverter.ToInt32(modint, 0);

          int firstbyte = binr.PeekChar();
          if (firstbyte == 0x00)
          {   //if first byte (highest order) of modulus is zero, don't include it
            binr.ReadByte();    //skip this null byte
            modsize -= 1;   //reduce modulus buffer size by 1
          }

          byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

          if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
            return null;
          int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
          byte[] exponent = binr.ReadBytes(expbytes);

          // ------- create RSACryptoServiceProvider instance and initialize with public key -----
          var rsa = RSA.Create();
          RSAParameters rsaKeyInfo = new RSAParameters
          {
            Modulus = modulus,
            Exponent = exponent
          };
          rsa.ImportParameters(rsaKeyInfo);

          return rsa;
        }

      }
    }

    #endregion Provider

    #region Utils

    private static byte[] ExportPrivateKey(RSACryptoServiceProvider csp)
    {
      if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
      var parameters = csp.ExportParameters(true);
      using (var stream = new MemoryStream())
      {
        var writer = new BinaryWriter(stream);
        writer.Write((byte)0x30); // SEQUENCE
        using (var innerStream = new MemoryStream())
        {
          var innerWriter = new BinaryWriter(innerStream);
          EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
          EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
          EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
          EncodeIntegerBigEndian(innerWriter, parameters.D);
          EncodeIntegerBigEndian(innerWriter, parameters.P);
          EncodeIntegerBigEndian(innerWriter, parameters.Q);
          EncodeIntegerBigEndian(innerWriter, parameters.DP);
          EncodeIntegerBigEndian(innerWriter, parameters.DQ);
          EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
          var length = (int)innerStream.Length;
          EncodeLength(writer, length);
          writer.Write(innerStream.GetBuffer(), 0, length);
        }

        byte[] key = stream.ToArray();
        return key;
      }
    }

    private static byte[] ExportPublicKey(RSACryptoServiceProvider csp)
    {
      var parameters = csp.ExportParameters(false);
      using (var stream = new MemoryStream())
      {
        var writer = new BinaryWriter(stream);
        writer.Write((byte)0x30); // SEQUENCE
        using (var innerStream = new MemoryStream())
        {
          var innerWriter = new BinaryWriter(innerStream);
          EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
          EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
          EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
          EncodeIntegerBigEndian(innerWriter, parameters.D);
          EncodeIntegerBigEndian(innerWriter, parameters.P);
          EncodeIntegerBigEndian(innerWriter, parameters.Q);
          EncodeIntegerBigEndian(innerWriter, parameters.DP);
          EncodeIntegerBigEndian(innerWriter, parameters.DQ);
          EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
          var length = (int)innerStream.Length;
          EncodeLength(writer, length);
          writer.Write(innerStream.GetBuffer(), 0, length);
        }

        byte[] key = stream.ToArray();
        return key;
      }
    }

    private static void EncodeLength(BinaryWriter stream, int length)
    {
      if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
      if (length < 0x80)
      {
        // Short form
        stream.Write((byte)length);
      }
      else
      {
        // Long form
        var temp = length;
        var bytesRequired = 0;
        while (temp > 0)
        {
          temp >>= 8;
          bytesRequired++;
        }
        stream.Write((byte)(bytesRequired | 0x80));
        for (var i = bytesRequired - 1; i >= 0; i--)
        {
          stream.Write((byte)(length >> (8 * i) & 0xff));
        }
      }
    }

    private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
    {
      stream.Write((byte)0x02); // INTEGER
      var prefixZeros = 0;
      for (var i = 0; i < value.Length; i++)
      {
        if (value[i] != 0) break;
        prefixZeros++;
      }
      if (value.Length - prefixZeros == 0)
      {
        EncodeLength(stream, 1);
        stream.Write((byte)0);
      }
      else
      {
        if (forceUnsigned && value[prefixZeros] > 0x7f)
        {
          // Add a prefix zero to force unsigned if the MSB is 1
          EncodeLength(stream, value.Length - prefixZeros + 1);
          stream.Write((byte)0);
        }
        else
        {
          EncodeLength(stream, value.Length - prefixZeros);
        }
        for (var i = prefixZeros; i < value.Length; i++)
        {
          stream.Write(value[i]);
        }
      }
    }

    private static int GetIntegerSize(BinaryReader binr)
    {
      byte bt = 0;
      int count = 0;
      bt = binr.ReadByte();
      if (bt != 0x02)
        return 0;
      bt = binr.ReadByte();

      if (bt == 0x81)
        count = binr.ReadByte();
      else
      if (bt == 0x82)
      {
        var highbyte = binr.ReadByte();
        var lowbyte = binr.ReadByte();
        byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
        count = BitConverter.ToInt32(modint, 0);
      }
      else
      {
        count = bt;
      }

      while (binr.ReadByte() == 0x00)
      {
        count -= 1;
      }
      binr.BaseStream.Seek(-1, SeekOrigin.Current);
      return count;
    }

    private static bool CompareBytearrays(byte[] a, byte[] b)
    {
      if (a.Length != b.Length)
        return false;
      int i = 0;
      foreach (byte c in a)
      {
        if (c != b[i])
          return false;
        i++;
      }
      return true;
    }

    #endregion Utils
  }
}